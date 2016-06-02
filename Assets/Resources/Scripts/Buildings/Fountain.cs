using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Fountain : Building
{
    //float fountainLife = GameplayConstants.FountainFullLife;

    // float minusFountainLifeDelta = 0.4f;
    // float plusFountainLifeDelta = 0.2f;

    int wasUsed;

    Color startColor;

    public RawImage health;
    public RawImage bg;

    const float maxWidth = 95f;
    float fountainLife = GameplayConstants.FountainMinLife;
    float fountainCurrentMaxLife = GameplayConstants.FountainMinLife;
    float deltaHealth = maxWidth / GameplayConstants.FountainMaxLife;
    void Update()
    {
        UpdateColor();
        startColor = Color.red;

        if (wasUsed == 0)
        {
            if (fountainLife <= GameplayConstants.FountainMaxLife && fountainLife != 0)
            {
                if(fountainLife < fountainCurrentMaxLife)
                {
                    fountainLife = Mathf.Min(fountainLife + Time.deltaTime * GameplayConstants.FountainRecoveryCoef, fountainCurrentMaxLife);
                }
                else
                {
                    fountainLife = Mathf.Min(fountainLife + Time.deltaTime * GameplayConstants.FountainGrowCoef, GameplayConstants.FountainMaxLife);
                    fountainCurrentMaxLife = fountainLife;
                }
            }
        }
        else
        {
            fountainLife = Mathf.Max(fountainLife - wasUsed * Time.deltaTime * GameplayConstants.AlienCoefRateFountain, 0);
            wasUsed = 0;
        }
    }

    void UpdateColor()
    {
        //GetComponent<Image>().color = Color.Lerp(startColor, Color.white, 1 - (fountainLife / (GameplayConstants.FountainFullLife)));
        RectTransform rth = health.GetComponent<RectTransform>();
        rth.sizeDelta = new Vector2(fountainLife*deltaHealth, rth.sizeDelta.y);

        RectTransform rtbg = bg.GetComponent<RectTransform>();
        rtbg.sizeDelta = new Vector2(fountainCurrentMaxLife * deltaHealth, rtbg.sizeDelta.y);


    }

    public void Use()
    {
        wasUsed++;
    }

    public override bool IsDestroyed()
    {
        if (fountainLife == 0)
            return true;
        return false;
    }
}
