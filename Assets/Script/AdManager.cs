using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;

public class AdManager : MonoBehaviour
{
	public static AdManager Instance{set; get;}

	public string BannerId;
	public string VideoId;

	private void Start()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);

		#if UNITY_EDITOR
		Debug.Log("Unable to play ads from EDITOR");
		#elif UNITY_ANDROID
		Admob.Instance().initAdmob(BannerId, VideoId);
		Admob.Instance().loadInterstitial();
		#endif
	}

	public void ShowBanner()
	{
		#if UNITY_EDITOR
		Debug.Log("Unable to play ads from EDITOR");
		#elif UNITY_ANDROID
		Admob.Instance().showBannerRelative(AdSize.Banner,AdPosition.BOTTOM_CENTER, 5);
		#endif
	}

	public void ShowVideo()
	{
		if(Admob.Instance().isInterstitialReady())
		{
			Admob.Instance().showInterstitial();
		}
	}
}
