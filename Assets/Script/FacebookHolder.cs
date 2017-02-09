using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookHolder : MonoBehaviour
{
    void Awake()
    {
        FB.Init (OnInitComplete, OnHideUnity);
    }

    private void OnInitComplete()
    {
        if (FB.IsInitialized)
            FB.ActivateApp ();
        else
            Debug.Log ("FB Init Fail");

        if (AccessToken.CurrentAccessToken != null)
        {
            Debug.Log (AccessToken.CurrentAccessToken.ToString ());
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ShareLink()
    {
        if (!FB.IsLoggedIn)
        {
            FBlogin ();
        }
        else
        {
            FB.FeedShare (
                toId: "",
                link: null,
                linkName: "Hextris",
                linkCaption: "헥트리스 잼써요",
                linkDescription: "우헤헤 it's fun",
                picture: null,
                mediaSource: "",
                callback: null);
        }
    }

    public void FBlogin()
    {
        FB.LogInWithReadPermissions (new List<string> () {
            "email",
            "public_profile",
            "user_friends"
        }, null);
    }
}
