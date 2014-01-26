using UnityEngine;
using System.Collections;
public class TW_Start : MonoBehaviour {
	GameObject start;
	void Start () {
		start = GameObject.Find("Start");	
	}
	void Update () {
		if(Input.GetButtonDown ("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray, out hit)) {
				if(start.transform == hit.transform){
					Application.LoadLevel(3);
					TextMesh mesh = start.GetComponent<TextMesh>();
					mesh.text = Screen.height.ToString() +","+Screen.width.ToString();
					Debug.Log ("OK");
				}
			}
		}
	}
}