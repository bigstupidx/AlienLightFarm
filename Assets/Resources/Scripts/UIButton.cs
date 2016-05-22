using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButton : MonoBehaviour {

    public Image reloadImage;

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

    IEnumerator ReloadCoroutine(float time)
    {
        float currentTime = time;

        RectTransform rt = reloadImage.GetComponent<RectTransform>();
        float maxHeight = rt.rect.height;

        while(currentTime > 0)
        {
            currentTime =  currentTime - Time.deltaTime;

            float currentPosY = (1-(currentTime/time)) * maxHeight;
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, currentPosY);

            yield return null;
        }


        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.rect.height);
 //       GetComponent<Button>().interactable = true;
    }

    void ToDefault()
    {
        RectTransform rt = reloadImage.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.rect.height);

    }

}
