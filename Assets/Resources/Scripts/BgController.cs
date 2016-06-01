using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BgController : MonoBehaviour {

    //Color startColor;
    //Color finalColor = new Color32(255, 47, 47, 255);
	// Use this for initialization
	void Start () {
       // startColor = GetComponent<Image>().color;
	}

    public void SetBackground(int num)
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Backgrounds/" + num);
    }
	
	// Update is called once per frame
    /*
	public void UpdateColor(float val)
    {
        GetComponent<Image>().color = Color.Lerp(startColor, finalColor, val);
    }*/
}
