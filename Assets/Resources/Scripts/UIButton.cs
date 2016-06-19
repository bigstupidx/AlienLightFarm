using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButton : MonoBehaviour {

    public Image reloadImage;
    public Image border;
    public Image block;
    public Text reloadTime;
    float currentTime;
    // Use this for initialization
    void Start () {
        ToDefault();
	}
	
	

    public void SetReload(float time)
    {
        StartCoroutine(ReloadCoroutine(time));

   //     GetComponent<Button>().interactable = false;

        //RectTransform rt = reloadImage.GetComponent<RectTransform>();
        //rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 0);
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

        // RectTransform rt = reloadImage.GetComponent<RectTransform>();
        //float maxHeight = rt.rect.height;
        reloadImage.gameObject.SetActive(true);
        reloadTime.gameObject.SetActive(true);
        
        while(currentTime > 0)
        {
            currentTime =  currentTime - Time.deltaTime;

            reloadTime.text = ((int) Mathf.Floor(currentTime)) + "";
            //float currentPosY = (1-(currentTime/time)) * maxHeight;
          //  rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, currentPosY);

            if (currentTime < 0)
                currentTime = 0;
            yield return null;
        }
        reloadImage.gameObject.SetActive(false);
        reloadTime.gameObject.SetActive(false);
      //  rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.rect.height);
 //       GetComponent<Button>().interactable = true;
    }

    public bool IsReload()
    {
        if (currentTime > 0)
            return true;
        else return false;
    }

    public void ToDefault()
    {
        StopAllCoroutines();
        currentTime = 0;
        reloadImage.gameObject.SetActive(false);
      ///  RectTransform rt = reloadImage.GetComponent<RectTransform>();
       // rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.rect.height);
        reloadTime.gameObject.SetActive(false);
    }

}
