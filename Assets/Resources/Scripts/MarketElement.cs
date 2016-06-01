using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MarketElement : MonoBehaviour {

    Color startColor;
    public Image image;
    public Image currentIcon;
    bool isOpen;
	// Use this for initialization
	void Awake () {
        startColor = image.color;
	}
	


    public void Close()
    {
        image.color = Color.black;
        isOpen = false;
    }

    public void Open()
    {
        image.color = startColor;
        isOpen = true;

    }

    public void SetCurrent()
    {
        currentIcon.gameObject.SetActive(true);
    }

    public void SetNotCurrent()
    {
        currentIcon.gameObject.SetActive(false);
    }


    public bool IsOpen()
    {
        return isOpen;
    }

}
