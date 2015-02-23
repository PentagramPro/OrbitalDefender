using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpBarController : MonoBehaviour {

	enum Modes {Idle,DmgDelay, DmgContract}
	public RectTransform HpIndicator, DamageIndicator;
	RectTransform barBase;

	[StoreThis]
	Modes state = Modes.Idle;

	[StoreThis]
	float hp;

	[StoreThis]
	float maxHp = 0;

	[StoreThis]
	float counter = 0;

	[StoreThis]
	float delayedHp = 0;


	float timeDelay = 0.3f;
	float speedContract = 20;

	public float HP {
		get
		{
			return hp;
		}
		set
		{
			//Debug.Log("new hp: "+value);
			if(value<hp)
			{
				state = Modes.DmgDelay;
				if(!DamageIndicator.gameObject.activeSelf)
					DamageIndicator.gameObject.SetActive(true);
				SetBar(DamageIndicator,value,delayedHp);
				counter = 0;
			}
			else
			{		
				if(value>delayedHp)
				{
					state = Modes.Idle;
					DamageIndicator.gameObject.SetActive(false);
					delayedHp = value;
				}
				else
				{

				}
			}
			SetBar(HpIndicator,0,value);
			hp = value;
		}
	}

	void Awake()
	{
		barBase = GetComponent<RectTransform>();
		DamageIndicator.gameObject.SetActive(false);
	}

	// Use this for initialization
	void Start () {
		maxHp = hp;
		delayedHp = maxHp;
		SetBar(HpIndicator,0,hp);
	}

	[ExecuteAfterLoad]
	void OnDeserialized()
	{
		Debug.Log("Hp bar is loaded. hp="+hp+", maxhp="+maxHp+", delayedHp="+delayedHp);
		SetBar(HpIndicator,0,hp);

		DamageIndicator.gameObject.SetActive(state!=Modes.Idle);

	}

	// Update is called once per frame
	void FixedUpdate () {
		switch(state)
		{
		case Modes.DmgDelay:
			counter+=Time.fixedDeltaTime;
			if(counter>timeDelay)
			{
				state = Modes.DmgContract;
			}
			break;
		case Modes.DmgContract:
			if(delayedHp>hp)
			{
				delayedHp-=speedContract*Time.fixedDeltaTime;
				if(delayedHp<hp)
					delayedHp = hp;
				SetBar(DamageIndicator,hp,delayedHp);
			}
			else
			{
				state = Modes.Idle;
				DamageIndicator.gameObject.SetActive(false);
			}
			break;
		}
	}

	void SetBar(RectTransform bar, float min, float max)
	{
		if(maxHp==0)
			return;

		if(min<0)
			min=0;
		if(max>maxHp)
			max = maxHp;

		float x1 = barBase.rect.width*min/maxHp;
		float x2 = barBase.rect.width*max/maxHp;

		bar.localPosition = new Vector2( (x2+x1-barBase.rect.width)/2,0);

		bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,x2-x1);
	}

}
