using UnityEngine;
using System.Collections.Generic;

public class Buildings : MonoBehaviour {

    public RectTransform fountain;
    public RectTransform wall;
    public RectTransform safeCupol;

    public Transform fountains;
    public Transform walls;
    public Transform safeCupols;
    public Transform blackHoles;
    public Transform pushers;
    public Transform healings;
    public Transform particles;

    public int GetFountainCount()
    {
        return fountains.childCount;
    }

    public int GetWallCount()
    {
        return walls.childCount;
    }

    public int GetSafeCupolCount()
    {
        return safeCupols.childCount;
    }

    public int GetBlackHolesCount()
    {
        return blackHoles.childCount;
    }

    public void ClearParticles()
    {
        List<GameObject> childrens = new List<GameObject>();
        foreach (Transform child in particles)
            childrens.Add(child.gameObject);

        foreach (GameObject child in childrens)
                Destroy(child);

    }
}
