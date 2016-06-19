using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

    public GameObject tutorialImage;
    public GameObject blockImage;

    Library library;

    bool isShowTutorial1;
    bool isShowTutorial2;


    Clickable tutorial1Clickable;

    void Awake()
    {
        library = GameObject.FindObjectOfType<Library>();
    }
	// Use this for initialization
	void Start () {
        tutorialImage.SetActive(false);
	}

    public void ShowTutorial1(Clickable clickable)
    {
        if (!PreferencesSaver.IsAcceptTutorial1() && !isShowTutorial1)
        {
            tutorial1Clickable = clickable;
            isShowTutorial1 = true;
            tutorialImage.SetActive(true);
            blockImage.SetActive(true);
            Time.timeScale = 0;
            tutorialImage.transform.position = clickable.transform.position;


            library.map.LockAllClickable();
            library.map.UnLockClickable(clickable);
        }
    }

    public void HideTutorial1(Clickable clickable)
    {
        if (isShowTutorial1 && clickable.Equals(tutorial1Clickable))
        {
            tutorialImage.SetActive(false);
            isShowTutorial1 = false;
            blockImage.SetActive(false);
            Time.timeScale = 1;
            library.map.UnLockAllClickable();
            PreferencesSaver.AcceptTutorial1();
        }
    }

    public void ShowTutorial2()
    {
        if (!PreferencesSaver.IsAcceptTutorial2() && !isShowTutorial2)
        {
            isShowTutorial2 = true;
            tutorialImage.SetActive(true);
            blockImage.SetActive(true);
            Time.timeScale = 0;
            tutorialImage.transform.position = library.uiButtonsController.safeCupolButton.transform.position;
            tutorialImage.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 0);
        }
    }

    public void HideTutorial2()
    {
        if (isShowTutorial2)
        {
            tutorialImage.SetActive(false);
            isShowTutorial2 = false;
            blockImage.SetActive(false);
            Time.timeScale = 1;
            PreferencesSaver.AcceptTutorial2();
        }
    }

    public void ToDefault()
    {
        tutorialImage.SetActive(false);
        isShowTutorial1 = false;
        isShowTutorial2 = false;


    }
}
