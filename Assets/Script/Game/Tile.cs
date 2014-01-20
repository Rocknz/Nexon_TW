using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public GameObject myTile;
	public MainLogic.TILETYPE myType;
	private static tk2dSpriteCollectionData datas = (tk2dSpriteCollectionData)Resources.Load("Tiles Data/Tiles",typeof(tk2dSpriteCollectionData));
	public void newTile(int x,int y,MainLogic l){
		myTile = new GameObject("Tile ("+x+","+y+")");
		myTile.AddComponent<tk2dSprite>();
		myTile.gameObject.transform.parent = l.transform;
		//myTile.transform.localScale = new Vector3(2,2,1);
		setScale(2.0f);
		setType();
		setTile(x,y);
	}
	public void setScale(float x){
		myTile.transform.localScale = new Vector3(x,x,1);
	}
	public void setType(){
		int t;
		do{
			t = (int)(Random.value * 5.0f);
		}while(t == 5);
		switch(t){
		case 0: myType = MainLogic.TILETYPE.Enemy; break;
		case 1: myType = MainLogic.TILETYPE.Sword; break;
		case 2: myType = MainLogic.TILETYPE.Wand; break;
		case 3: myType = MainLogic.TILETYPE.Coin; break;
		case 4: myType = MainLogic.TILETYPE.Potion; break;
		}
	}
	public void setTile(int x,int y){
		myTile.transform.localPosition = new Vector3(x*10-27,y*10-27,100);
		setImage();
	}
	private void setImage(){
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
}
