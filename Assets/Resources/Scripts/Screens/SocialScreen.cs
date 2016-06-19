using UnityEngine;
using System.Collections;

public class SocialScreen : MonoBehaviour {

    public GameObject enterVkButton;
    public GameObject postVkButton;


    Library library;

    void Awake()
    {
        library = GameObject.FindObjectOfType<Library>();
    }



    public void OnOpenScreen()
    {
        enterVkButton.SetActive(false);
        postVkButton.SetActive(false);

        bool isShowGroup = library.vkController.IsShowVkGroupButton();
        bool isShowPost = library.vkController.IsShowVkPostButton();

        RectTransform enterVkRt = enterVkButton.GetComponent<RectTransform>();
        RectTransform postVkRt = postVkButton.GetComponent<RectTransform>();



        if (isShowGroup && isShowPost)
        {
            enterVkButton.SetActive(true);
            postVkButton.SetActive(true);
            enterVkRt.anchoredPosition = new Vector2(200, enterVkRt.anchoredPosition.y);
            postVkRt.anchoredPosition = new Vector2(-200, postVkRt.anchoredPosition.y);
        }
        else if(isShowGroup)
        {
            enterVkButton.SetActive(true);
            enterVkRt.anchoredPosition = new Vector2(0, enterVkRt.anchoredPosition.y);
        }
        else if (isShowPost)
        {
            postVkButton.SetActive(true);
            postVkRt.anchoredPosition = new Vector2(0, postVkRt.anchoredPosition.y);
        }


    }

    public void HideEnterVkButton()
    {
        enterVkButton.gameObject.SetActive(false);
    }

    public void HidePostVkButton()
    {
        postVkButton.gameObject.SetActive(false);
    }
}
