using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Facebook.Unity;
public class ShowBestScore : MonoBehaviour {
	int fscore,highestScore;
	string id="";
	Text score;

	void Start(){
		score = gameObject.GetComponent<Text> ();
		fscore = ((int)GameObject.Find ("Score").GetComponent<Score> ().GetScore ());
		FB.API ("/me?fields=id", HttpMethod.GET, SetId);
	}
	void SetId(IResult result){
		id = result.ResultDictionary ["id"].ToString();
		print (id);
		StartCoroutine( GetScore ());
	}
	IEnumerator GetScore(){
		string url = "http://wegas-lbcswatch.rhcloud.com/Faceless/getScore/"+id;
		WWW www = new WWW(url);
		yield return www;
		print ("HScore: "+www.text);
		highestScore=int.Parse(www.text);
		if (fscore <= highestScore) {
			fscore = highestScore;
		} else {
			StartCoroutine( SetScore ());
		}
		score.text = fscore.ToString ();
		Destroy (GameObject.Find ("Score"));
	}
	IEnumerator SetScore(){
		string url = "http://wegas-lbcswatch.rhcloud.com/Faceless";
		WWWForm form = new WWWForm();
		form.AddField( "set_score", fscore );
		form.AddField( "facebook_id", id );
		form.AddField( "access-token", AccessToken.CurrentAccessToken.TokenString );
		WWW www=new WWW(url, form );
		yield return www;
		print (www.text);
	}
}
