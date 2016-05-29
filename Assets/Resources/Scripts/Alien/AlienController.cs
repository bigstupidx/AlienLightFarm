using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlienController : MonoBehaviour {

    List<Alien> borningAliens = new List<Alien>();
    List<Alien> aliens = new List<Alien>();


    float bornDelay;
    Library library;
    int io = 0;
    bool startBorningWasUsed = false;

    int iterator;
    // Use this for initialization
	void Awake () {
        library = GameObject.FindObjectOfType<Library>();


	}

    // Update is called once per frame
    void Update()
    {
        BornAlien();
        
        int currentIterator = 0;
        int startNum = iterator;
        while(currentIterator <= 20)
        {
            if (aliens.Count == 0)
                break;


            aliens[iterator].UpdateVal();

            if (aliens[iterator].GetLiveState().Equals(Alien.AlienLiveState.Die))
            {
                RemoveAlien(aliens[iterator]);
                return;
            }
            
            bool foutainIsFind = false;
            if (aliens[iterator].GetLiveState().Equals(Alien.AlienLiveState.Alive)
                &&
                (aliens[iterator].GetMovementState().Equals(Alien.AlienMovementState.Free) 
                || aliens[iterator].GetMovementState().Equals(Alien.AlienMovementState.MoveToJump) 
                || aliens[iterator].GetMovementState().Equals(Alien.AlienMovementState.MoveToPoint) 
                || aliens[iterator].GetMovementState().Equals(Alien.AlienMovementState.Wait)))
            {
                if (aliens[iterator].IsHungry())
                {
                    if (aliens[iterator].GetCurrentFountainTarget() == null)
                    {
                        foutainIsFind = AlienFindFountain(aliens[iterator]);
                    }
                    else
                    {
                        foutainIsFind = true;
                    }
                }

                if (!foutainIsFind && aliens[iterator].GetMovementState().Equals(Alien.AlienMovementState.Free))
                {
                    AlienMoveTo(aliens[iterator]);
                }
            }

            iterator++;
            currentIterator++;

            if (iterator >= aliens.Count)
                iterator = 0;
            if (iterator == startNum)
                break;


        }
        
       
    }

    void BornAlien()
    {
        
        if (!startBorningWasUsed)
        {
            startBorningWasUsed = true;

            for (int i = 0; i < GameplayConstants.AlienStartBorningCount; i++)
                CreateAlien();
            
            SetBornDelay();
        }
        else if (borningAliens.Count < 1 && IsBornDelay())
        {
       /*     
              if(io == 0)
              {*/
            CreateAlien();
            // foreach(Clickable cl in library.map.GetWayToClickable(library.map.GetCell(9), library.map.GetCell(28)))
            //     {
            //        Debug.Log(cl.num);
            //   }
            io++;
            SetBornDelay();
        }

        UpdateBorningDelay();
    }

    void UpdateBorningDelay()
    {
        bornDelay = Mathf.Max(0, bornDelay - Time.deltaTime);
    }

    void SetBornDelay()
    {
        bornDelay = GameplayConstants.AlienBorningDelay + GameplayConstants.AlienBornTime;
    }

    bool IsBornDelay()
    {
        if (bornDelay > 0)
            return false;
        else return true;
    }

    void CreateAlien()
    {
        Clickable clickable = /*library.map.GetCell(14);*/library.map.GetRandomFreeCellForBorn();

        if(clickable != null)
        {
            Alien alien = clickable.CreateAlien();
            AddBorningAlien(alien);
        }
    }

    void AlienMoveTo(Alien alien)
    {
        List<Clickable> tempClickable = null;
        /*
        if (alien.GetCurrentClickable().GetBuildingType().Equals(Clickable.BuildingType.Wall))
        {            
            tempClickable = library.map.GetAcceptableClickedWithWall(alien);
        }*/
        // на основе алгоритма ли надо найти ближайшие точки

        //        library.map.GetClickableInArea();

        /*Изменить grid в lee*/
        Vector2 pos = new Vector2(-1,-1);
        if (alien.GetIgnoreWallClickable() != null)
        {
            pos = Lee.GetPosInMatrix(alien.GetIgnoreWallClickable().num, alien.GetIgnoreWallClickable().GetFloor());
            Lee.startGrid[(int)pos.y, (int)pos.x] = Lee.WALL;
        }
        /*
        for (int j = Lee.startGrid.GetLength(0) - 1; j >= 0; j--)
        {
            string str = "";
            for (int i = 0; i < Lee.startGrid.GetLength(1); i++)
            {
                string qwe = " ";
                if (Lee.startGrid[j, i] >= 0 && Lee.startGrid[j, i] < 10)
                {
                    qwe = "  ";
                }
                str += Lee.startGrid[j, i] + qwe;
            }
            Debug.Log(str);
        }
        Debug.Log(" ");*/
        Clickable clickable = library.map.GetRandomFreeCellToMove(alien.GetCurrentClickable(),tempClickable);

  //      if (alien.GetIgnoreWallClickable() != null)
 //       {
 //           Debug.Log(clickable.num);
   //     }

            Queue<Clickable> wayToClickable = library.map.GetWayToClickable(alien.GetCurrentClickable(),clickable);


        if(pos != new Vector2(-1,-1))
            Lee.startGrid[(int)pos.y, (int)pos.x] = Lee.BLANK;
        /*Вернуть обратно*/

        //  foreach (Clickable cl in wayToClickable)
        //  Debug.Log(cl.num);
        //alien.SetTarget(clickable);

        alien.SetWayQueue(wayToClickable);
    }

    public bool AlienFindFountain(Alien alien)
    {
        
        List<Clickable> tempClickable = null;
        /*if (alien.GetCurrentClickable().GetBuildingType().Equals(Clickable.BuildingType.Wall))
        {
            tempClickable = library.map.GetAcceptableClickedWithWall(alien);
        }*/
        Vector2 pos = new Vector2(-1, -1);
        if (alien.GetIgnoreWallClickable() != null)
        {
            pos = Lee.GetPosInMatrix(alien.GetIgnoreWallClickable().num, alien.GetIgnoreWallClickable().GetFloor());
            Lee.startGrid[(int)pos.y, (int)pos.x] = Lee.WALL;
        }
        Clickable clickable = library.map.FindNearestFountain(alien, tempClickable);

       // Debug.Log("Clickable num "+ clickable.num);

        if (clickable != null /*&& !alien.IsFountainTimeOut()*/)
        {
            Queue<Clickable> wayToClickable = library.map.GetWayToClickable(alien.GetCurrentClickable(), clickable);

            // foreach(Clickable cl in wayToClickable)
            //    Debug.Log(cl.num);

            if (pos != new Vector2(-1, -1))
                Lee.startGrid[(int)pos.y, (int)pos.x] = Lee.BLANK;

            alien.SetFountainWayQueue(wayToClickable);
          //  alien.SetTarget(library.map.FindNearestFountain(alien.GetCurrentClickable()));
           // alien.SetFoundFountain(true);
            return true;
        }
        else
        {
            if (pos != new Vector2(-1, -1))
                Lee.startGrid[(int)pos.y, (int)pos.x] = Lee.BLANK;
            return false;
        }
    }

    void AddBorningAlien(Alien alien)
    {
        borningAliens.Add(alien);
    }

    public void AddAlien(Alien alien)
    {
        borningAliens.Remove(alien);
        aliens.Add(alien);
        library.alienCount.SetCount(aliens.Count);
    }
    
    public void RemoveAlien(Alien alien)
    {
        RemoveFreeAlien(alien);
        library.gameController.DeathAlien();
    }

    public void RemoveFreeAlien(Alien alien)
    {
        borningAliens.Remove(alien);
        aliens.Remove(alien);
        Destroy(alien.gameObject);
        library.alienCount.SetCount(aliens.Count);
    }

    public void MoveAllAlienInClickable(Clickable clickable)
    {
        foreach(Alien alien in aliens)
        {
            if(alien.GetCurrentClickable().num == clickable.num)
            {
                alien.MoveAlienByWall();
            }
        }
    }

    public void LandWasChanged()
    {
        foreach (Alien alien in aliens)
        {
            alien.LandWasChanged();
        }

    }

    public int GetAlienCount()
    {
        return aliens.Count;
    }


    public List<Alien> GetAliensInCell(Clickable clickable)
    {
        List<Alien> tempAliens = new List<Alien>();
        foreach(Alien alien in aliens)
        {

            if(alien.GetCurrentClickable() != null && alien.GetCurrentClickable() == clickable && !alien.GetMovementState().Equals(Alien.AlienMovementState.Jump))
            {
                tempAliens.Add(alien);
            }
        }

        foreach (Alien alien in borningAliens)
        {

            if (alien.GetCurrentClickable() != null && alien.GetCurrentClickable() == clickable)
            {
                tempAliens.Add(alien);
            }
        }

        return tempAliens;
    }


    public void ToDefault()
    {
        foreach(Alien alien in aliens)
            Destroy(alien.gameObject);
        
        aliens.Clear();

        foreach (Alien alien in borningAliens)
            Destroy(alien.gameObject);

        borningAliens.Clear();
        library.alienCount.SetCount(aliens.Count);
        bornDelay = 0;
        startBorningWasUsed = false;
    }

}
