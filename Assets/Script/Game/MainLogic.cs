using UnityEngine;
using System.Collections;

public class MainLogic : MonoBehaviour {
	static int TILE_SIZE = 7; 
	GameObject touch;
	public enum TILETYPE{ Enemy, Sword, Wand, Potion, Coin};
	Stack pathStack;

	// Use this for initialization
	Tile[,] main_Tile = new Tile[TILE_SIZE,TILE_SIZE];
	bool[,] touch_Check = new bool[TILE_SIZE,TILE_SIZE];

	void Start () {
		pathStack = new Stack();
		touch = GameObject.Find ("Touch");
		int i,j;
		for(i=0;i<TILE_SIZE;i++){
			for(j=0;j<TILE_SIZE;j++){
				main_Tile[i,j] = new Tile();
				main_Tile[i,j].newTile (i,j,this);
				touch_Check[i,j] = false;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.touchCount != 0){
			Vector2 V2 = Input.GetTouch(0).position;
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(V2.x,V2.y,0));
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray, out hit)) {
				foreach(Tile tiles in main_Tile){
					if(tiles.myTile.transform == hit.transform){
						TextMesh mesh = touch.GetComponent<TextMesh>();
						mesh.text = "("+V2.x +","+V2.y+")";
						Add(new Vector2(tiles.myX,tiles.myY));
					}
				}
			}
		}
		else{
			int count = pathStack.Count;
			while(pathStack.Count != 0){
				Vector2 now = (Vector2)pathStack.Pop();
				int nx,ny;
				nx = (int)now.x;
				ny = (int)now.y;
				main_Tile[nx,ny].setScale (2.5f);
				if(count >= 3){
					main_Tile[nx,ny].setHp (-1);
				}
				touch_Check[nx,ny] = false;
			}
			DestroyTile();
		}
	}
	private void DestroyTile(){
		// hP ㄱㅏ 0ㅂㅗㄷㅏ ㅈㅏㄱㅇㅡㄴ ㅌㅏㅇㅣㄹ ㅈㅔㄱㅓ
		int i,j;
		for(i=0;i<TILE_SIZE;i++){
			for(j=0;j<TILE_SIZE;j++){
				if(main_Tile[i,j].myHp <= 0){
					//ㅈㅔㄱㅓ
					iTween.ScaleTo(main_Tile[i,j].myTile, iTween.Hash(
						"x", 0,
						"y", 0,
						"easeType", "easeOutQuad",
						"time", 1,
						"delay", .1));
				}
			}
		}
	}
	private void Add(Vector2 newSelectedTile){
		// Path ㅇㅔ ㅍㅛ ㅅㅣ!
		int nx,ny;
		nx = (int)newSelectedTile.x;
		ny = (int)newSelectedTile.y;
		if(pathStack.Count == 0){
			pathStack.Push(newSelectedTile);
			main_Tile[nx,ny].setScale (1.0f);
			touch_Check[nx,ny] = true;
		}
		else{
			Vector2 nowSelectedTile = (Vector2)pathStack.Peek();
			int nx1,ny1;
			nx1 = (int)nowSelectedTile.x;
			ny1 = (int)nowSelectedTile.y;
			if(!touch_Check[nx,ny]){
				if(Vector2.Distance(newSelectedTile,nowSelectedTile) < 2 &&
				   main_Tile[nx,ny].myType == main_Tile[nx1,ny1].myType){
					// ㄱㅏㄹ ㅅㅜ ㅇㅣㅆ ㅇㅡㄹ ㄸㅐ.
					pathStack.Push(newSelectedTile);
					main_Tile[nx,ny].setScale (1.0f);
					touch_Check[nx,ny] = true;
				}
			}
			else{
				//ㄱㅣㅈㅗㄴㅇㅔ ㅈㅗㄴㅈㅐ ㅎㅏㄴㄷㅏㅁㅕㄴ
				while(!pathStack.Peek().Equals(newSelectedTile)){
					Vector2 delpath = (Vector2)pathStack.Pop();
					int dx,dy;
					dx = (int)delpath.x;
					dy = (int)delpath.y;
					main_Tile[dx,dy].setScale(2.5f);
					touch_Check[dx,dy] = false;
				}
			}
		}
	}
}
