using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RewardScreen : MonoBehaviour {

    public GameObject img;
    public GameObject closeButton;
    Library library;

    void Awake()
    {
        library = GameObject.FindObjectOfType<Library>();
    }

	public void ShowReward()
    {
        closeButton.SetActive(false);

        PreferencesSaver.AddRewardNum();


        int numReward = PreferencesSaver.GetNumReward();


        if (numReward < GameplayConstants.OrderOpeningReward.Length)
        {
            img.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Backgrounds/" + GameplayConstants.OrderOpeningReward[numReward]);
        }

        iTween.ValueTo(gameObject,
             iTween.Hash("from",  img.GetComponent<RectTransform>().rect.height/2f,
              "to", -library.canvas.GetComponent<RectTransform>().rect.height/2f,
              "delay", 0.5f,
              "time", 2f,
              "onupdate", (System.Action<object>)(newVal =>
              {
                  //endScreen.GetComponent<CanvasGroup>().alpha = (float)newVal;
                  img.GetComponent<RectTransform>().anchoredPosition = new Vector2(img.GetComponent<RectTransform>().anchoredPosition.x, (float) newVal);
              }
              ),
              "oncomplete", "OnCompleteShow",
              "easetype", iTween.EaseType.easeOutElastic,
              "oncompletetarget", gameObject
              ));


        PreferencesSaver.SetCurrentElementInMarket(GameplayConstants.OrderOpeningReward[numReward]);
        PreferencesSaver.SetOpenElementInMarket(GameplayConstants.OrderOpeningReward[numReward]);

        library.money.AddMoney(-GameplayConstants.PurchasePrice);
        library.money.SaveMoney();
        library.screenController.endScreen.GetComponent<EndScreen>().HideRewardButton();
    }

    void OnCompleteShow()
    {
        closeButton.SetActive(true);
    }

}
