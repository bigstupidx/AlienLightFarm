using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    Library library;
    float currentVal;
	// Use this for initialization
	void Start () {
        library = GameObject.FindObjectOfType<Library>();
	}
	
	// Update is called once per frame
	void Update () {

        if (currentVal == GameplayConstants.MaxAgro)
            GameOver();

        currentVal = Mathf.Max(0, currentVal - Time.deltaTime * GameplayConstants.RecoveryAgroCoef);

        //library.bgController.UpdateColor(currentVal/ GameplayConstants.MaxAgro);

        library.agroLineController.UpdateLength(currentVal/100f);

               
	}


    public void DeathAlien()
    {
        currentVal = Mathf.Min(GameplayConstants.MaxAgro, currentVal+ GameplayConstants.MaxAgro *GameplayConstants.AgroCoef/ library.aliens.GetComponent<AlienController>().GetAlienCount());
    }

    void GameOver()
    {
       // Time.timeScale = 0;
    }
}
