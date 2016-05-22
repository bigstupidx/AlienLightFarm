using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SafeCupol : Building
{


    float lifeTime = GameplayConstants.SafeCupolLifeTime;

    Color startColor;
    Color finalColor;

    public GameObject child;
    // Use this for initialization

    void Start()
    {
        startColor = child.GetComponent<Image>().color;
        finalColor = new Color(startColor.r, startColor.g, startColor.b, 0);
    }


    // Update is called once per frame
    void Update()
    {
        lifeTime = Mathf.Max(lifeTime - Time.deltaTime, 0);

        child.GetComponent<Image>().color = Color.Lerp(finalColor, startColor, lifeTime / GameplayConstants.SafeCupolLifeTime);// new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, startAlpha + lifeTime/FullLifeTime);
    }

    public override bool IsDestroyed()
    {
        if (lifeTime == 0)
            return true;

        return false;
    }
}
