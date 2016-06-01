using UnityEngine;
using System.Collections.Generic;

public class Pusher : MonoBehaviour// Building
{
    //float lifeTime = GameplayConstants.;

    //Color startColor;
    //Color finalColor;
    /*
    bool isUsed;
    public GameObject child;
    List<Alien> aliens = new List<Alien>();
    // Use this for initialization
    Library library;


    void Awake()
    {
        child.transform.localScale = Vector3.zero;
        library = GameObject.FindObjectOfType<Library>();

    }




    // Update is called once per frame
    void Update()
    {
        
        if(!isUsed)
        {
            ExpulsionAll();
            isUsed = true;
        }


        if (child.transform.localScale.x <= 1)
        {
            
            child.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * GameplayConstants.PusherExpansionSpeed;

            if (child.transform.localScale.x > 1)
                child.transform.localScale = Vector3.one;
        }
        // lifeTime = Mathf.Max(lifeTime - Time.deltaTime, 0);
        //child.GetComponent<Image>().color = Color.Lerp(finalColor, startColor, lifeTime / GameplayConstants.WallLifeTime);// new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, startAlpha + lifeTime/FullLifeTime);
    }

    public void AddAlien(Alien alien)
    {
        aliens.Add(alien);
     //   alien.transform.SetParent(child.transform, true);
        //alien.SetBlackHole();
    }

    /*
    public void ExpulsionAll()
    {
        foreach(Alien alien in aliens)
        {
            alien.PusherExpulsionAlien();
        }
    }

    

    public override bool IsDestroyed()
    {
        if (child.transform.localScale == Vector3.one)
            return true;

        return false;
    }*/
}