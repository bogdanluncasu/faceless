using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DragComponents : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {
	Vector3 startPosition;
	GameObject container;
	static List<GameObject> levels;
	Vector3 initialObjectMove;

	public void SetLevel(int l){
		transform.Find("Play").gameObject.GetComponent<Button>().onClick.AddListener(()=>{	
			Application.LoadLevel("level"+l);
		});
	}
	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		print ("drop");
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{

		Vector3 position = Input.mousePosition;
		if (position.x < startPosition.x&&gameObject.transform.position.x>50) {
			MoveToLeft (15);
		} else if(gameObject.transform.position.x<650)
			MoveToRight (15);
		print ("drag");
	}

	#endregion
	void MoveToLeft(float dist){
		foreach (GameObject l in levels) {
			l.transform.position=
				new Vector3(l.transform.position.x-dist,l.transform.position.y,0);
		}
	}

	void MoveToRight(float dist){
		foreach (GameObject l in levels) {
			l.transform.position=
				new Vector3(l.transform.position.x+dist,l.transform.position.y,l.transform.position.z);
		}
	}
	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		startPosition = gameObject.transform.position;
		print ("begin");
	}
	#endregion


	void Start () {
		if(levels==null)levels=new List<GameObject>();
		container = GameObject.Find ("Levels").transform.Find ("ContentBox").gameObject as GameObject;
		levels.Add (gameObject);
	}


}
