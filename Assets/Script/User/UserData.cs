using UnityEngine;
using System.Collections;

public class UserData : MonoBehaviour {
	private int mHp;
	private int mAtk;
	private int mDef;
	private int mInt;
	private int mCoin;

	// use instance -> UserData.Instance.Hp 

	private static UserData ins;
	public static UserData Instance {
		get { 
			return ins; 
		}
	}
	public int Hp{
		get { return ins.mHp; }
		set { mHp = value; }
	}
	public int Atk{
		get { return mAtk; }
		set { mAtk = value; }
	}
	public int Def{
		get { return mDef; }
		set { mDef = value; }
	}
	public int Int{
		get { return mInt; }
		set { mInt = value; }
	}
	public int Coin{
		get { return mCoin; }
		set { mCoin = value; }
	}
	void Start () {
		// DataLoad
		ins = new UserData();

		//PlayerPrefs -> DataLoad and DataSave

		ins.Hp = PlayerPrefs.GetInt ("Hp");
		ins.Atk = PlayerPrefs.GetInt ("Atk");
		ins.Def = PlayerPrefs.GetInt ("Def");
		ins.Int = PlayerPrefs.GetInt ("Int");
		ins.Coin = PlayerPrefs.GetInt ("Coin");
		
		PlayerPrefs.SetInt("Hp",20);
		PlayerPrefs.SetInt("Atk",10);
		PlayerPrefs.SetInt("Def",10);
		PlayerPrefs.SetInt("Int",10);
		PlayerPrefs.SetInt("Coin",10);

		DontDestroyOnLoad(this);	
		Application.LoadLevel(1);
	}

	void Update () {
	
	}
}
