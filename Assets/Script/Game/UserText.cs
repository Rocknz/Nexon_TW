using UnityEngine;

public class UserText : MonoBehaviour {
	GameObject Atk;
	GameObject Int;
	GameObject Def;
	GameObject Hp;
	GameObject Xien;
	GameObject Coin;
	void Start(){
		Atk = GameObject.Find ("Atk Gap");
		Int = GameObject.Find ("Int Gap");
		Def = GameObject.Find ("Def Gap");
		Hp = GameObject.Find ("Hp Gap");
		Xien = GameObject.Find ("Xien Gap");
		Coin = GameObject.Find ("Coin Gap");
		setStat();
	}
	public void setStat(){
		Atk.GetComponent<tk2dTextMesh>().text = UserData.Instance.Atk.ToString();
		Atk.GetComponent<tk2dTextMesh>().Commit();
		Int.GetComponent<tk2dTextMesh>().text = UserData.Instance.Int.ToString();
		Int.GetComponent<tk2dTextMesh>().Commit();
		Def.GetComponent<tk2dTextMesh>().text = UserData.Instance.Def.ToString();
		Def.GetComponent<tk2dTextMesh>().Commit();
		Hp.GetComponent<tk2dTextMesh>().text = UserData.Instance.Hp.ToString()+"/"+UserData.Instance.HpMax.ToString();
		Hp.GetComponent<tk2dTextMesh>().Commit();
		Xien.GetComponent<tk2dTextMesh>().text = UserData.Instance.Xien.ToString()+"/"+UserData.Instance.XienMax.ToString();
		Xien.GetComponent<tk2dTextMesh>().Commit();
		Coin.GetComponent<tk2dTextMesh>().text = UserData.Instance.Coin.ToString();
		Coin.GetComponent<tk2dTextMesh>().Commit();
	}
	void Update(){

	}
}
