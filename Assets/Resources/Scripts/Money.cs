using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Money : MonoBehaviour {

    public Text moneyText;

    int money = 0;
	// Use this for initialization
	void Start () {
       money =  PreferencesSaver.GetMoney();
	}
	
	public void AddMoney(int val)
    {
        money += val;
        moneyText.text = money + "";
    }


    public void SaveMoney()
    {
        PreferencesSaver.SetMoney(money);
    }

    public int GetMoney()
    {
        return money;
    }
    



}
