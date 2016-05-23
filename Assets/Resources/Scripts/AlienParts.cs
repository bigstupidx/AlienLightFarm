using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlienParts : MonoBehaviour {

    public Image[] images;
    float speed;
    
    public void SetRight()
    {
        if (transform.localScale.x <= 0)
            transform.localScale = new Vector3(transform.localScale.x*(-1) ,transform.localScale.y,transform.localScale.z);
    }

    public void SetLeft()
    {
        if (transform.localScale.x >= 0)
            transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
    }

	public void SetMove()
    {
        GetComponent<Animator>().speed = speed/GameplayConstants.AlienNormalSpeed;
        GetComponent<Animator>().SetInteger("AlienState",1);
    }

    public void SetStay()
    {
        GetComponent<Animator>().speed = 1;
        GetComponent<Animator>().SetInteger("AlienState",0);

    }

    public void SetJumpDown()
    {
        GetComponent<Animator>().speed = 1;
        GetComponent<Animator>().SetInteger("AlienState",2);
    }

    public void SetJumpUp()
    {
        GetComponent<Animator>().speed = 1;
        GetComponent<Animator>().SetInteger("AlienState",3);
    }

    public void UpdateSpeed(float speed)
    {
        this.speed = speed;

        if(GetComponent<Animator>().GetInteger("AlienState").Equals(1))
        {
            GetComponent<Animator>().speed = speed / GameplayConstants.AlienNormalSpeed;
        }
    }
}
