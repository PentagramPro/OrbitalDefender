using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;


public class AdController : MonoBehaviour {
	public static string PrefShowBanner = "ShowBanner";

	public UIController UI;
	enum Modes{Count,Wait}
	Modes state = Modes.Count;


	CountTime counter;
	public float RefreshRate = 60;

	BannerView bannerView;
	private void RequestBanner()
	{		 
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	

		state = Modes.Wait;
	}

	void OnAdLoaded(object sender, System.EventArgs args)
	{
		Debug.Log("Ad loaded by BannerView");

		state = Modes.Count;
		UI.ShiftTopBar(true);

	}

	void OnDestroy()
	{
		bannerView.Destroy();
	}
	// Use this for initialization
	void Start () {

		if(PlayerPrefs.HasKey(PrefShowBanner) && PlayerPrefs.GetInt(PrefShowBanner)==0)
		{
			enabled = false;
			return;
		}

		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-9593729489650617/6232962689";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
		bannerView.AdLoaded+=OnAdLoaded;

		counter = new CountTime(RefreshRate);
	}
	
	// Update is called once per frame
	void Update () {
		if(state==Modes.Count)
		{

			if(counter.Count(RefreshRate))
			{
				counter.Reset();
				RequestBanner();
			}
		}
	}
}
