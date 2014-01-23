﻿using UnityEngine;
using System.Collections;

public class TileStatus : MonoBehaviour {
	public MainLogic.TILETYPE myType;
	public int myX,myY;
	public int myHp;
	public TileStatus(int y,int x){
		// ㅅㅐㄹㅗ ㅅㅓㄴ ㅇㅓㄴ 
		myY = y;
		myX = x;

		myHp = 0;
		SetHp (1);
		
		NewType ();
	}
	public void SetHp(int addHp){
		myHp += addHp;
	}
	public void NewType(){
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
	public static bool EqualType(MainLogic.TILETYPE fType,MainLogic.TILETYPE sType){
		if(fType == sType){
			return true;
		}
		else if(fType == MainLogic.TILETYPE.Sword && sType == MainLogic.TILETYPE.Enemy){
			return true;
		}
		else if (fType == MainLogic.TILETYPE.Enemy && sType == MainLogic.TILETYPE.Sword){
			return true;
		}
		return false;
	}
}
