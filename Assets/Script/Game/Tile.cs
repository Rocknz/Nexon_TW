using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public GameObject myTile;
	public TileStatus myStatus;
	private static tk2dSpriteCollectionData datas = (tk2dSpriteCollectionData)Resources.Load("Tiles Data/Tiles",typeof(tk2dSpriteCollectionData));
	public static Vector3 Position(int y,int x){
		return (new Vector3((float)(x*3.5-10.5f),(float)(y*3.5-10.5f),(float)(100.0f)));
	}

	public Tile(int y,int x,MainLogic l){
		myTile = new GameObject("Tile ("+y+","+x+")");
		myTile.AddComponent<tk2dSprite>();
		myTile.gameObject.transform.parent = l.transform;
		myStatus = new TileStatus(y,x);
		SetTileByStatus();
	}

	public void SetTileByStatus(){
		SetPosition (myStatus.myY,myStatus.myX);
		SetImage (myStatus.myType);
		SetScale (0.9f);
	}
	private void SetImage(MainLogic.TILETYPE myType){
		tk2dSprite sprite = myTile.GetComponent<tk2dSprite>();
		switch(myType){
		case MainLogic.TILETYPE.Enemy:
			sprite.SetSprite(datas,"Enemy");
			break;
		case MainLogic.TILETYPE.Sword:
			sprite.SetSprite(datas,"Sword");
			break;
		case MainLogic.TILETYPE.Wand:
			sprite.SetSprite(datas,"Wand");
			break;
		case MainLogic.TILETYPE.Coin:
			sprite.SetSprite(datas,"Coin");
			break;
		case MainLogic.TILETYPE.Potion:
			sprite.SetSprite(datas,"Potion");
			break;
		}
	}
	public void SetScale(float x){
		myTile.transform.localScale = new Vector3(x,x,1);
	}
	public void SetPosition(int y,int x){
		myTile.transform.localPosition = Position (y,x);
	}
}
