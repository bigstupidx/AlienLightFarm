using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButton : MonoBehaviour {

    public Image reloadImage;
    public Image border;
    public Image block;
    float currentTime;
    // Use this for initialization
    void Start () {
        ToDefault();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetReload(float time)
    {
        StartCoroutine(ReloadCoroutine(time));

   //     GetComponent<Button>().interactable = false;

        RectTransform rt = reloadImage.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 0);
    }



    public void SetChecked()
    {
        border.gameObject.SetActive(true);
    }

    public void SetUnchecked()
    {
        border.gameObject.SetActive(false);
    }

    public void SetActive()
    {
        GetComponent<Button>().interactable = true;
        block.gameObject.SetActive(false);
    }

    public void SetDeactive()
    {
        GetComponent<Button>().interactable = false;
        block.gameObject.SetActive(true);
    }

    IEnumerator ReloadCoroutine(float time)
    {
        currentTime = time;

        RectTransform rt = reloadImage.GetComponent<RectTransform>();
        float maxHeight = rt.rect.height;

        while(currentTime > 0)
        {
            currentTime =  currentTime - Time.deltaTime;

            float currentPosY = (1-(currentTime/time)) * maxHeight;
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, currentPosY);

            if (currentTime < 0)
                currentTime = 0;
            yield return null;
        }


        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.rect.height);
 //       GetComponent<Button>().interactable = true;
    }

    public bool IsReload()
    {
        if (currentTime > 0)
            return true;
        else return false;
    }

    void ToDefault()
    {
        RectTransform rt = reloadImage.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.rect.height);

    }

}
