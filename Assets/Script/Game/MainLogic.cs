using UnityEngine;
using System.Collections;

public class MainLogic : MonoBehaviour {
	public enum TILETYPE{ Enemy, Sword, Wand, Potion, Coin};
	public static int TILE_SIZE = 7; 

	GameObject touch;
	Stack pathStack;
	bool nowBreaking;
	int fallingCount;
	// Use this for initialization
	Tile[,] main_Tile = new Tile[TILE_SIZE,TILE_SIZE];
	bool[,] touch_Check = new bool[TILE_SIZE,TILE_SIZE];

	void Start () {
		pathStack = new Stack();
		touch = GameObject.Find ("Touch");
		int i,j;
		for(i=0;i<TILE_SIZE;i++){
			for(j=0;j<TILE_SIZE;j++){
				main_Tile[i,j] = new Tile(i,j,this);
				touch_Check[i,j] = false;
			}
		}
		nowBreaking = false;
	}

	// Update is called once per frame
	void Update () {
		if(!nowBreaking){
//			if(Input.touchCount != 0){
//				Vector2 V2 = Input.GetTouch(0).position;
//				Ray ray = Camera.main.ScreenPointToRay(new Vector3(V2.x,V2.y,0));
			if(Input.GetButtonDown("Fire1")){
				Vector2 V2 = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit = new RaycastHit();
				if(Physics.Raycast(ray, out hit)) {
					foreach(Tile tiles in main_Tile){
						if(tiles.myTile.transform == hit.transform){
							TextMesh mesh = touch.GetComponent<TextMesh>();
							mesh.text = "("+V2.y +","+V2.x+")";
							Add(new Vector2(tiles.myStatus.myX,tiles.myStatus.myY));
						}
					}
				}
			}
			else if(Input.GetButtonDown("Fire2")){
				int count = pathStack.Count;
				while(pathStack.Count != 0){
					Vector2 now = (Vector2)pathStack.Pop();
					int nx,ny;
					nx = (int)now.x;
					ny = (int)now.y;
					main_Tile[ny,nx].SetScale (0.9f);
					if(count >= 3){
						main_Tile[ny,nx].myStatus.SetHp (-1);
					}
					touch_Check[ny,nx] = false;
				}
				DestroyTile();
			}
		}
	}
	private void DestroyTile(){
		// hP ㄱㅏ 0ㅂㅗㄷㅏ ㅈㅏㄱㅇㅡㄴ ㅌㅏㅇㅣㄹ ㅈㅔㄱㅓ
		int i,j,cnt=0;
		for(i=0;i<TILE_SIZE;i++){
			for(j=0;j<TILE_SIZE;j++){
				if(main_Tile[i,j].myStatus.myHp <= 0){
					cnt ++;
				}
			}
		}
		if(cnt != 0){
			nowBreaking = true;
			for(i=0;i<TILE_SIZE;i++){
				for(j=0;j<TILE_SIZE;j++){
					if(main_Tile[i,j].myStatus.myHp <= 0){
						//ㅈㅔㄱㅓ
						cnt --;
						if(cnt != 0){
							iTween.ScaleTo(main_Tile[i,j].myTile, iTween.Hash(
								"x", 0,
								"y", 0,
								"easeType", "easeOutQuad",
								"time", 1,
								"delay", .1));
						}
						else{
							iTween.ScaleTo(main_Tile[i,j].myTile, iTween.Hash(
								"x", 0,
								"y", 0,
								"easeType", "easeOutQuad",
								"time", 1,
								"delay", .1,
								"oncomplete","FallingTile",
								"oncompletetarget",gameObject,
								"oncompleteparams",main_Tile[i,j]));
						}
					}
				}
			}
		}
	}
	private void FallingTile(){
		int i,j,y;
		for(j=0;j<TILE_SIZE;j++){
			y = 0;
			for(i=0;i<TILE_SIZE;i++){
				while(true){
					if(y >= TILE_SIZE){
						main_Tile[i,j].myStatus = new TileStatus(y,j);
						y ++;
						break;
					}
					else{
						if(main_Tile[y,j].myStatus.myHp <= 0){
							y++;
						}
						else{
							main_Tile[i,j].myStatus = main_Tile[y,j].myStatus;
							y++;
							break;
						}
					}
				}
				main_Tile[i,j].SetTileByStatus();
			}
		}
		fallingCount = 0;
		for(i=0;i<TILE_SIZE;i++){
			for(j=0;j<TILE_SIZE;j++){
				if(main_Tile[i,j].myStatus.myY != i){
					main_Tile[i,j].myStatus.myY = i;
					fallingCount ++;
					Vector3 v3 = Tile.Position(i,j);
					iTween.MoveTo(main_Tile[i,j].myTile, iTween.Hash(
						"x", v3.x,
						"y", v3.y,
						"easeType", "easeOutQuad",
						"speed", 10,
						"oncomplete","FallingEnd",
						"oncompletetarget",gameObject,
						"oncompleteparams",main_Tile[i,j]));
				}
			}
		}
	}
	public void FallingEnd(Tile tile){
		fallingCount --;
		tile.SetTileByStatus();
		if(fallingCount == 0){
			nowBreaking = false;
		}
	}
	private void Add(Vector2 newSelectedTile){
		// Path ㅇㅔ ㅍㅛ ㅅㅣ!
		int nx,ny;
		ny = (int)newSelectedTile.y;
		nx = (int)newSelectedTile.x;
		if(pathStack.Count == 0){
			pathStack.Push(newSelectedTile);
			main_Tile[ny,nx].SetScale (0.4f);
			touch_Check[ny,nx] = true;
		}
		else{
			Vector2 nowSelectedTile = (Vector2)pathStack.Peek();
			int nx1,ny1;
			nx1 = (int)nowSelectedTile.x;
			ny1 = (int)nowSelectedTile.y;
			if(!touch_Check[ny,nx]){
				if(Vector2.Distance(newSelectedTile,nowSelectedTile) < 2 &&
				   TileStatus.EqualType(main_Tile[ny,nx].myStatus.myType,
				                        main_Tile[ny1,nx1].myStatus.myType)){

					// ㄱㅏㄹ ㅅㅜ ㅇㅣㅆ ㅇㅡㄹ ㄸㅐ.
					pathStack.Push(newSelectedTile);
					main_Tile[ny,nx].SetScale (0.4f);
					touch_Check[ny,nx] = true;
				}
			}
			else{
				//ㄱㅣㅈㅗㄴㅇㅔ ㅈㅗㄴㅈㅐ ㅎㅏㄴㄷㅏㅁㅕㄴ
				while(!pathStack.Peek().Equals(newSelectedTile)){
					Vector2 delpath = (Vector2)pathStack.Pop();
					int dx,dy;
					dx = (int)delpath.x;
					dy = (int)delpath.y;
					main_Tile[dy,dx].SetScale(0.9f);
					touch_Check[dy,dx] = false;
				}
			}
		}
	}
}
