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
    public Button enterVkButton;
    public Button postVkButton;

    public Button socialMenuButton;
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

        library.playGameServices.AdScore(aliensCount);

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

    public void UpdateAll()
    {
        UpdateScore();
        ShowAd();
       // ShowGetRewardButton();
        //ShowEnterInVk();
        //ShowPostVk();
        ShowSocialMenuButton();
    }

    public void ShowEnterInVk()
    {
        if (library.vkController.IsShowVkGroupButton())
            enterVkButton.gameObject.SetActive(true);
        else
            enterVkButton.gameObject.SetActive(false);
    }

    public void ShowPostVk()
    {
        if (library.vkController.IsShowVkPostButton())
            postVkButton.gameObject.SetActive(true);
        else
            postVkButton.gameObject.SetActive(false);
    }

    public void HideRewardButton()
    {
        getRewardButton.gameObject.SetActive(false);
    }

    public void HideAdButton()
    {
        videoAdButton.gameObject.SetActive(false);
    }

 
    public void ShowSocialMenuButton()
    {
        if (library.vkController.IsShowVkGroupButton() || library.vkController.IsShowVkPostButton())
            socialMenuButton.gameObject.SetActive(true);
        else
            socialMenuButton.gameObject.SetActive(false);
    }


    public void HideSocialMenuButton()
    {
        socialMenuButton.gameObject.SetActive(false);
    }

}
