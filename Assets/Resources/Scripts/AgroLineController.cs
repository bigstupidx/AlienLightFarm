using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AgroLineController : MonoBehaviour {

 //   Color startColor;
   // Color finalColor = new Color32(255, 47, 47, 255);
    Library library;
    // Use this for initialization
    float val;
    void Start()
    {
        library = GameObject.FindObjectOfType<Library>();
    //    startColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    public void UpdateLength(float val)
    {
        this.val = val;
        //GetComponent<Image>().color = Color.Lerp(startColor, finalColor, val);
   /*     RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(val * library.canvas.GetComponent<RectTransform>().sizeDelta.x, rt.sizeDelta.y);
        rt.anchoredPosition = new Vector2(-library.canvas.GetComponent<RectTransform>().sizeDelta.x/2f + rt.sizeDelta.x/2f, rt.anchoredPosition.y);*/
    }

    void Update()
    {
        RectTransform rt = GetComponent<RectTransform>();

        float xVal = MathTools.ULerp(rt.sizeDelta.x,
            val * library.canvas.GetComponent<RectTransform>().sizeDelta.x,
            5f * Time.unscaledDeltaTime);
        
        rt.sizeDelta = new Vector2 (xVal, rt.sizeDelta.y);

        rt.anchoredPosition = new Vector2(-library.canvas.GetComponent<RectTransform>().sizeDelta.x / 2f + rt.sizeDelta.x / 2f, rt.anchoredPosition.y);

    }
}
