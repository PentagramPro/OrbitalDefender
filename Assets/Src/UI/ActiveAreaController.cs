using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ActiveAreaController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

	enum Modes {Idle, Targeting};

	public RectTransform TargetingArea;
	public GunController Gun;
	public DirectionIndicatorController DirIndicator;


	public float TargetingR=10, ActiveR=1;
	//float FireStrength = 0;
	Vector2 FireDirection;
	RectTransform rt;
	Modes state = Modes.Idle;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
		TargetingArea.gameObject.SetActive(false);
		DirIndicator.Visible = false;
	}
	
	// Update is called once per frame
	void Update () {

		rt.position = Camera.main.WorldToScreenPoint(Gun.transform.position);
	}

	void OnDrawGizmos()
	{
		if(TargetingArea!=null)
		{
			RectTransform rect = GetComponent<RectTransform>();
			Vector3 pos = rect.position;
			Gizmos.DrawWireSphere(pos,TargetingR);
			Gizmos.DrawWireSphere(pos,ActiveR);
		}
	}
	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if(state == Modes.Targeting)
		{
			Vector2 pt2 = eventData.position;

			// pointer is limited to lower part of circle
			if(eventData.position.y>rt.position.y)
				pt2.y = rt.position.y;

			Vector2 rad = pt2 - (Vector2)rt.position;

			if(rad.magnitude>TargetingR)
			{
				rad = Vector2.Lerp(rt.position,pt2,TargetingR/rad.magnitude)- (Vector2)rt.position;
				pt2 = (Vector2)rt.position+rad;
			}

			if(rad.magnitude>ActiveR)
			{
				DirIndicator.Visible = true;
				FireDirection = -rad/TargetingR;
				DirIndicator.SetOrientation(rt.position,pt2);
				Gun.OnMoveBarrel(FireDirection);

			}
			else
			{
				DirIndicator.Visible = false;
				FireDirection = Vector2.zero;
			}
		}
	}

	#endregion

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		Vector2 rad = eventData.position - (Vector2)rt.position;
		if(rad.magnitude<ActiveR)
		{
			TargetingArea.gameObject.SetActive(true);
			DirIndicator.Visible = true;
			state = Modes.Targeting;
		}
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if(state == Modes.Targeting)
		{
			TargetingArea.gameObject.SetActive(false);
			DirIndicator.Visible = false;
			state = Modes.Idle;
			if(FireDirection.magnitude>0)
			{
				Gun.OnFire(FireDirection);
			}
		}
	}

	#endregion
}
