using UnityEngine;
using System.Collections;

public class MainLogic : MonoBehaviour {
	static int TILE_SIZE = 6; 
	GameObject touch;
	public enum TILETYPE{ Enemy, Sword, Wand, Potion, Coin};

	// Use this for initialization
	Tile[,] Main_Tile = new Tile[TILE_SIZE,TILE_SIZE];
	void Start () {
		touch = GameObject.Find ("Touch");
		int i,j;
		for(i=0;i<TILE_SIZE;i++){
			for(j=0;j<TILE_SIZE;j++){
				Main_Tile[i,j] = new Tile();
				Main_Tile[i,j].newTile (i,j,this);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.touchCount != 0){
			Vector2 V2 = Input.GetTouch (0).position;
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(V2.x,V2.y,0));
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray, out hit)) {
				foreach(Tile tiles in Main_Tile){
					if(tiles.myTile.transform == hit.transform){
						TextMesh mesh = touch.GetComponent<TextMesh>();
						mesh.text = "("+V2.x +","+V2.y+")";
						tiles.setScale (1.0f);
					}
				}
			}
		}
		else{
			int i,j;
			for(i=0;i<TILE_SIZE;i++){
				for(j=0;j<TILE_SIZE;j++){
					Main_Tile[i,j].setScale(2.0f);
				}
			}
		}
	}
}
