using UnityEngine;
using System.Collections;

public class AdController : MonoBehaviour {

    int iterator = 0;

    Library library;

	// Use this for initialization
	void Awake () {
        library = GameObject.FindObjectOfType<Library>();
	}
	

    public void AddIterator()
    {
        iterator++;
    }

    public bool CanShowVideoAd()
    {
        if (iterator % 4 == 2)
            return true;
        else
            return false;
    }

    public bool CanShowStaticAd()
    {
        if (iterator % 4 == 3)
            return true;
        else
            return false;
    }

    public void ShowVideoAd()
    {

    }

    public void ShowStaticAd()
    {

    }

    public void OnCompleteVideoAd()
    {
        library.money.AddMoney(GameplayConstants.AdMoneyReward);
    }
}
