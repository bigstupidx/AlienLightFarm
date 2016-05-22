using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlienCount : MonoBehaviour {

    Text text; 

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	public void SetCount(int val)
    {
        text.text = val + "";
    }
}
