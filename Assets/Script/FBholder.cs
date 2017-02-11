using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FBholder : MonoBehaviour
{
    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    public GameObject DialogProfilePic;

    public Text DialogUserName;

    void Awake()
    {
        FB.Init (SetInit, OnHideUnity);
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log ("FB is logged in");
        }
        else
        {
            Debug.Log ("FB is not logged in");
        }

        DealWithFBMenus (FB.IsLoggedIn);
    }

    void OnHideUnity(bool isGameShown)
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

    public void FBlogin()
    {
        List<string> permissions = new List<string> ();
        permissions.Add ("public_profile");

        FB.LogInWithReadPermissions (permissions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {
        if (result.Error != null)
        {
            Debug.Log (result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log ("FB is logged in");
            }
            else
            {
                Debug.Log ("FB is not logged in");
            }

            DealWithFBMenus (FB.IsLoggedIn);
        }
    }

    void DealWithFBMenus (bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            DialogLoggedIn.SetActive (true);
            DialogLoggedOut.SetActive (false);

            FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        }
        else
        {
            DialogLoggedIn.SetActive (false);
            DialogLoggedOut.SetActive (true);
        }
    }

    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
            DialogUserName.text = "Hi there, " + result.ResultDictionary ["first_name"];
        } 
        else
        {
            Debug.Log (result.Error);
        }
    }

    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
        {
            Image ProfilePic = DialogProfilePic.GetComponent<Image> ();

            ProfilePic.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
        }
    }

    public void Share()
    {
        FB.ShareLink (
            contentTitle: "Hextris High Score",
            contentURL: new System.Uri ("http://cafe.naver.com/snugdc"),
            contentDescription: "Here is a link to my Circle.",
            callback: OnShare);
    }

    private void OnShare(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty (result.Error))
        {
            Debug.Log ("Share Link Error : " + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log (result.PostId);
        }
        else
        {
            Debug.Log ("Share Succeed");
        }
    }
}