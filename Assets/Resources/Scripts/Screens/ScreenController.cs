using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenController : MonoBehaviour {

    Library library;

    public GameObject gameScreen;
    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject marketScreen;
    public GameObject rewardScreen;

    GameObject currentScreen;

	// Use this for initialization

    void Awake()
    {
        library = GameObject.FindObjectOfType<Library>();

    }

    void Start () {

        HideAllScreens();
        ShowStartScreen();
	}


    void HideAllScreens()
    {
        gameScreen.SetActive(false);
        marketScreen.SetActive(false);
        startScreen.SetActive(false);
        endScreen.SetActive(false);
        rewardScreen.SetActive(false);
    }

    public void ShowStartScreen()
    {

        if (currentScreen == null)
        {
            startScreen.SetActive(true);
            library.playGameServices.Authenticate();
        }
        else
        {
            startScreen.SetActive(true);

            currentScreen = startScreen;

            iTween.ValueTo(gameObject,
            iTween.Hash("from", 1,
             "to", 0,
             "time", 0.5f,
             "onupdate", (System.Action<object>)(newVal =>
             {
                 endScreen.GetComponent<CanvasGroup>().alpha = (float)newVal;
             }
             ),
             "oncomplete", "OnCompleteShowStartScreen",
             "oncompletetarget", gameObject
             ));
        }

    }

    public void ShowMarketScreen()
    {
        marketScreen.SetActive(true);
        marketScreen.GetComponent<MarketScreen>().OnOpenScreen();
    }

    public void HideMarketScreen()
    {
        marketScreen.SetActive(false);
    }

    public void ShowRewardScreen()
    {
        rewardScreen.SetActive(true);
        rewardScreen.GetComponent<RewardScreen>().ShowReward();
    }

    public void HideRewardScreen()
    {
        rewardScreen.SetActive(false);
    }


    void OnCompleteShowStartScreen()
    {
        endScreen.SetActive(false);
    }

    public void ShowGameScreen()
    {
        gameScreen.SetActive(true);


        library.money.gameObject.SetActive(false);

        gameScreen.GetComponent<GameController>().UpdateBackground();

        currentScreen = gameScreen;

        iTween.ValueTo(gameObject,
              iTween.Hash("from", 1,
               "to", 0,
               "time", 0.5f,
               "onupdate", (System.Action<object>)(newVal =>
               {
                   startScreen.GetComponent<CanvasGroup>().alpha = (float)newVal;

               }
               ),
               "oncomplete", "OnCompleteHideStartScreen",
               "oncompletetarget", gameObject
               ));
    }

    void OnCompleteHideStartScreen()
    {
        startScreen.GetComponent<CanvasGroup>().alpha = 1;

        startScreen.SetActive(false);

        library.gameController.StartGame();
    }

    public void ShowEndScreen()
    {
        endScreen.SetActive(true);
        library.money.gameObject.SetActive(true);
        endScreen.GetComponent<EndScreen>().UpdateAll();
           


        currentScreen = endScreen;
        endScreen.GetComponent<CanvasGroup>().alpha = (float)0;


        iTween.ValueTo(gameObject,
              iTween.Hash("from", 0,
               "to", 1,
               "time", 0.5f,
               "onupdate", (System.Action<object>)(newVal =>
               {
                   endScreen.GetComponent<CanvasGroup>().alpha = (float)newVal;

               }
               ),
               "oncomplete", "OnCompleteShowEndScreen",
               "oncompletetarget", gameObject
               ));
    }

    void OnCompleteShowEndScreen()
    {
        library.gameController.ToDefault();
        gameScreen.SetActive(false);
    }

}
