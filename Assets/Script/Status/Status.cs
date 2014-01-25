using UnityEngine;
using System.Collections;
using LitJson;

/*
 * json's keys
 * t = ['limit_lv','limit_stat','name','durability','kyungdo','stab_damage',
     'hack_damage','def','int_damage','magic_def','accuracy',
     'dex','agility','critical','delay','intensity','slot']
*/
public class Status : MonoBehaviour {
	int money = 1<<20;
	GameObject Money;
	GameObject[] ViewTable;
	/*
	 * 0 : ㄷㅐㄱㅓㅁ
	 * 1 : ㅌㅐㄷㅗ
	 * 2 : ㅍㅕㅇㄷㅗ
	 * 3 : ㅌㅜㄱㅜ
	 * 4 : ㄱㅏㅂㅇㅗㅅ
	 * 5 : ㄴㅓㅋㅡㄹ
	 * 6 : ㅇㅏㅁㄹㅣㅅ
	 * 7 : ㅁㅓㄹㅣ
	 * 8 : ㅁㅗㅁ
	 * 9 : ㅅㅗㄴ
	 */
	//					0			1			2			3		4		5		6	7		8	9
	string[] ItemType={"DeaSword","TaeSword","B_M_Sword","Helmet","L_Armor","NCL","BU","hea","bod","hand"};
	int [] ItemLevel={0,0,0,0,0,0,0,0,0,0};
	LitJson.JsonData [] Items = new LitJson.JsonData[10];
    
	// Use this for initialization
	void Start () {
		Money = GameObject.Find ("Money");
		TextMesh textMesh = Money.GetComponent<TextMesh> ();
		textMesh.text = "Money : " + money.ToString ();

		ViewTable = new GameObject[4];
		MeshRenderer[] renderer = new MeshRenderer[4];
		for ( int i = 0 ; i < 4 ; i++ ) {
			ViewTable[i] = GameObject.Find ("ViewTable"+i.ToString());
	/*		renderer[i] = ViewTable[i].GetComponent<MeshRenderer>();
			renderer[i].material.color = new Color(renderer[i].material.color.r,
			                                       renderer[i].material.color.g,
			                                       renderer[i].material.color.b,
			                                       1.0f);
	*/	}


		for (int i = 0; i < 10; i++) {
			string text = System.IO.File.ReadAllText(""+ItemType[i]+".json");
			Items[i] = LitJson.JsonMapper.ToObject (text);
			Debug.Log(i);
		}

		for ( int i = 0 ; i < Items[0].Count ; i++ ) 
			Debug.Log (Items[0][i]["name"]);
		//JsonData jsonBU = JsonMapper.ToObject (JsonData);
		//Debug.Log (jsonBU ["name"].Count);
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetButtonDown ("Fire1") ) {
			if ( money - (1<<10) < 0 ) return ;
			money -= 1<<10;
			//Debug.Log(money);

			TextMesh textMesh = Money.GetComponent<TextMesh>();
			textMesh.text = "Money : "+money.ToString ();
		}
		//Debug.Log ("asdf");
	}
}
