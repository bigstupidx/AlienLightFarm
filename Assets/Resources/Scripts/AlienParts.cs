using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlienParts : MonoBehaviour {

    public Image[] images;
    float moveSpeed;
    float speed = 0;
    
    

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
        GetComponent<Animator>().speed = moveSpeed/GameplayConstants.AlienNormalSpeed;
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

    public void UpdateSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;

        if(GetComponent<Animator>().GetInteger("AlienState").Equals(1))
        {
            GetComponent<Animator>().speed = moveSpeed / GameplayConstants.AlienNormalSpeed;
        }
    }

    public void PauseOn()
    {
        this.speed = GetComponent<Animator>().speed;
        GetComponent<Animator>().speed = 0;
    }

    public void PauseOff()
    {
        //if (GetComponent<Animator>().GetInteger("AlienState").Equals(1))
       //     GetComponent<Animator>().speed = moveSpeed / GameplayConstants.AlienNormalSpeed;
      //  else
      if(speed != 0)
            GetComponent<Animator>().speed = this.speed;
    }

}
