using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BlackHole : Building
{
    //float lifeTime = GameplayConstants.;

    //Color startColor;
    //Color finalColor;

    public GameObject child;
    List<Alien> aliens = new List<Alien>();
    // Use this for initialization
    Library library;
    void Start()
    {
        //  startColor = child.GetComponent<Image>().color;
        //finalColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        library = GameObject.FindObjectOfType<Library>();
        
    }


    // Update is called once per frame
    void Update()
    {
        if (child.transform.localScale.x > 0)
        {
            child.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * GameplayConstants.BlackHoleClosedSpeed;

            if(child.transform.localScale.x <= 0)
            {
                RemoveAliens();

                child.transform.localScale = Vector3.zero;
            }
        }  
       // lifeTime = Mathf.Max(lifeTime - Time.deltaTime, 0);
        //child.GetComponent<Image>().color = Color.Lerp(finalColor, startColor, lifeTime / GameplayConstants.WallLifeTime);// new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, startAlpha + lifeTime/FullLifeTime);
    }

    public void AddAlien(Alien alien)
    {
        aliens.Add(alien);
        alien.transform.SetParent(child.transform, true);
        alien.SetBlackHole();
    }

    void RemoveAliens()
    {
        foreach(Alien alien in aliens)
        {
            library.aliens.GetComponent<AlienController>().RemoveFreeAlien(alien);
        }
    }

    public override bool IsDestroyed()
    {
        if (child.transform.localScale == Vector3.zero)
            return true;

        return false;
    }
}

