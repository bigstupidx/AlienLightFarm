using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreen : MonoBehaviour {

    Library library;

    public Text scoreText;
    public Text bestScoreText;
    public Text currentMoneyText;

    public Button videoAdButton;
    public Button getRewardButton;
    // Use this for initialization
	void Awake () {
        library = GameObject.FindObjectOfType<Library>();
	}


    public void UpdateScore()
    {
        int aliensCount = library.aliens.GetComponent<AlienController>().GetAlienCount();

        scoreText.text = aliensCount + "";

        int bestScore = PreferencesSaver.GetBestScore();

        if (aliensCount > bestScore)
        {
            bestScore = aliensCount;
            PreferencesSaver.SetBestScore(bestScore);
        }

        bestScoreText.text = bestScore + "";

        int currentMoney = (int) Mathf.Floor(aliensCount / GameplayConstants.MoneyDevider);

        currentMoneyText.text = currentMoney +"";

        library.money.AddMoney(currentMoney);

        library.money.SaveMoney();

    }

    public void ShowAd()
    {
        if (library.adController.CanShowVideoAd())
            videoAdButton.gameObject.SetActive(true);
        else
            videoAdButton.gameObject.SetActive(false);


        if (library.adController.CanShowStaticAd())
            library.adController.ShowStaticAd();

        library.adController.AddIterator();
    }

    public void ShowGetRewardButton()
    {
        if (library.money.GetMoney() >= GameplayConstants.PurchasePrice && (PreferencesSaver.GetNumReward() + 1)< GameplayConstants.OrderOpeningReward.Length)
            getRewardButton.gameObject.SetActive(true);
        else
            getRewardButton.gameObject.SetActive(false);
    }


    public void HideRewardButton()
    {
        getRewardButton.gameObject.SetActive(false);
    }

}
