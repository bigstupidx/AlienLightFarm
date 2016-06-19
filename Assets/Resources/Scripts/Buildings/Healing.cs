using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Healing : Building {

    Library library;

    public ParticleSystem ps;

    bool readyToDelete;

    void Awake()
    {
        //  startColor = child.GetComponent<Image>().color;
        //finalColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        library = GameObject.FindObjectOfType<Library>();

    }

    void Start()
    {
        GameObject goPs = Instantiate(ps.gameObject) as GameObject;
        goPs.transform.SetParent(transform, true);
        goPs.transform.position = transform.position;
        goPs.transform.position += new Vector3(0, 0, 100);
        goPs.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0,30);
        goPs.GetComponent<ParticleSystem>().Play();
        StartCoroutine(TimerToDelete());
    }

 

    public void AddAlien(Alien alien)
    {
        alien.Healing();
    }


    public override bool IsDestroyed()
    {
        if (readyToDelete)
        {
            return true;
        }

        return false;
    }

    IEnumerator TimerToDelete()
    {
        yield return new WaitForSeconds(2f);

        readyToDelete = true;
    }
}
