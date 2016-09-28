using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Facebook.Unity;
using SimpleJSON;

public class CreateLevels : MonoBehaviour {
	public int levels;
	public GameObject levelBox;
	int height,width,i=0;
	string id;
	void Start () {
		height = Screen.height;
		width = Screen.width;
		FB.API ("/me?fields=id", HttpMethod.GET, SetId);
	}
	void SetId(IResult result){
		id = result.ResultDictionary ["id"].ToString();
		StartCoroutine( GetLevels ());
	}

	IEnumerator GetLevels(){
		string url = "http://wegas-lbcswatch.rhcloud.com/Faceless/getLevels";
		WWWForm form = new WWWForm();
		form.AddField( "get_levels", 1 );
		form.AddField( "facebook_id", id);
		form.AddField( "access-token", AccessToken.CurrentAccessToken.TokenString );
		WWW www=new WWW(url, form );
		yield return www;
		print (www.text);
		JSONNode json = JSON.Parse (www.text);
		JSONArray array = json.AsArray;
		foreach (JSONNode node in array) {
			if(node["done"]!=null){
				GameObject newObj = GameObject.Instantiate(levelBox) as GameObject;
				//transform.Find("ScrollLevel").transform.Find("Viewport").transform.Find("Content")
				newObj.transform.parent = GameObject.Find("Levels").transform.Find("ContentBox").transform; 
				newObj.GetComponent<RectTransform>().offsetMax=-new Vector2((width-204)-(width*138*i)/817, (height*41)/339);
				newObj.GetComponent<RectTransform>().offsetMin=new Vector2(69+(width*138*i)/817, (height*87)/339);
				FillPanels(newObj,node);
				DragComponents d=newObj.GetComponent<DragComponents>();
				d.SetLevel(++i);
			}
		}
	}
	

	void FillPanels (GameObject panel,JSONNode node)
	{
		GameObject level=panel.transform.Find ("Level").gameObject as GameObject;
		GameObject goal = panel.transform.Find ("GoalObjective").gameObject as GameObject;
		GameObject play = panel.transform.Find ("Play").gameObject as GameObject;

		level.GetComponent<Text> ().text = node ["level"];
		goal.GetComponent<Text> ().text = node ["goal"];
		if (node ["done"].AsInt == -1) {
			play.SetActive(false);
		}
	}
}
