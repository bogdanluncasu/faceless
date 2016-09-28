using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
public class Fb : MonoBehaviour {
	public GameObject IsLoggedIn;
	public GameObject NotLoggedIn;
	public GameObject Avatar;
	public GameObject UserName;

	void Awake(){
		print ("I'm awake");
		if (FB.IsLoggedIn) {
			print("logged in");
			FBLoggedIn(true);
		} else
		FB.Init (Initialize,OnHideGame);
	}

	private void Initialize(){
		print ("init");
		if (FB.IsLoggedIn) {
			print("logged in");
			FBLoggedIn(true);
		} else {
			//Login();
			FBLoggedIn(false);
		}
	}

	private void OnHideGame(bool isGameShown){
		print ("hide");
		Time.timeScale=isGameShown?1:0;
	}

	public void Login(){
		FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, Result);
	}

	private void Result(IResult result){
		if (FB.IsLoggedIn) {
			FBLoggedIn(true);
		} else {
			print ("Unsuccessfull");
			FBLoggedIn(false);
		}
	}
	public void FBLoggedIn(bool logged){
		if (logged) {
			FB.API ("/me?fields=id,first_name,last_name,email,gender,locale,picture", HttpMethod.GET, DisplayUsername);
			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
			IsLoggedIn.SetActive (true);
			NotLoggedIn.SetActive (false);

		} else {
			IsLoggedIn.SetActive (false);
			NotLoggedIn.SetActive (true);
		}
	}

	void DisplayUsername(IResult result)
	{
		string url = "http://wegas-lbcswatch.rhcloud.com/Faceless";
		WWWForm form = new WWWForm();
		form.AddField( "facebook_id", result.ResultDictionary ["id"].ToString() );
		form.AddField( "first_name", result.ResultDictionary ["first_name"].ToString() );
		form.AddField( "last_name", result.ResultDictionary ["last_name"].ToString() );
		form.AddField( "email", result.ResultDictionary ["email"].ToString() );
		form.AddField( "picture", result.ResultDictionary ["picture"].ToString() );
		form.AddField( "access-token", AccessToken.CurrentAccessToken.TokenString );
		new WWW(url, form );

		Text TUserName = UserName.GetComponent<Text> ();
		if (result.Error == null) {
			TUserName.text = "Hi there, " + result.ResultDictionary ["first_name"]+" "+result.ResultDictionary ["last_name"];			
		} else {
			Debug.Log (result.Error);
		}
	}
	
	void DisplayProfilePic(IGraphResult result)
	{
	    if (result.Texture != null) {
			Image ProfilePic = Avatar.GetComponent<Image> ();
			ProfilePic.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
		}
	}
}
