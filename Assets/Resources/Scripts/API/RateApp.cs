using UnityEngine;
using System.Collections;

public class RateApp : MonoBehaviour {


    public void RateApplication()
    {
        Application.OpenURL(GameplayConstants.marketURL);

    }
}
