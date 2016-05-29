using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Map : MonoBehaviour {

    public Clickable[] cells = new Clickable[34];
    Library library;

    bool lightIsOn;

    void Awake()
    {
        library = GameObject.FindObjectOfType<Library>();
    }


    public Clickable GetRandomFreeCellForBorn(Clickable excetions)
    {
        Clickable cell = null;

        List<int> tempArr = new List<int>();
        for (int i = 0; i < cells.Length; i++)
        {
            /*
            bool isBusy = false;
            for (int j = 0; j < excetions.Length; j++)
            {
                if (excetions[j].num == i)
                    isBusy = true;
            }

            if(!isBusy)*/
            if (excetions == null || (excetions != null && excetions.num != i))
                tempArr.Add(i);
        }

        do
        {
            if (tempArr.Count == 0)
                return cells[0];
                

            int randomNum = Random.Range(0, tempArr.Count - 1);
            int num = tempArr[randomNum];

            cell = cells[num];

            tempArr.RemoveAt(randomNum);

        } while (!cell.IsFree()/*!cell.IsFreeForBorn()*/);

        return cell;
    }

    public Clickable GetRandomFreeCellForBorn()
    {
        return GetRandomFreeCellForBorn(null);
    }

    public Clickable GetRandomFreeCellToMove(Clickable clickable, List<Clickable> listClickable)
    {
        return GetRandomFreeCellToMove(clickable, listClickable, 0);
    }

    public Clickable GetRandomFreeCellToMove(Clickable clickable, List<Clickable> listClickable, int attempt)
    {
        Clickable cell = null;

        //   int maxCell = Mathf.Clamp(clickable.num + Alien.ClampDistance[attempt], 0, cells.Length - 1);
        //      int minCell = Mathf.Clamp(clickable.num - Alien.ClampDistance[attempt], 0, cells.Length - 1);



        //     List<int> tempArr = new List<int>();

        /*
                if (listClickable != null)
                {
                    foreach (Clickable inListClickable in listClickable)
                    {
                        if (clickable.num != inListClickable.num && inListClickable.num >= minCell && inListClickable.num <= maxCell)
                        {
                            tempArr.Add(inListClickable.num);
                        }
                    }
                }

                else
                {*/

         List<Clickable> tempArrCl = GetClickableInArea(clickable, Alien.ClampDistance[attempt]);
       // Debug.Log(tempArrCl.Count);

        /* for (int i = minCell; i <= maxCell; i++)
         {
             if (clickable.num != i)
                 tempArr.Add(i);
         }*/
        //  }/*
        do
        {
            //   if (tempArr.Count == 0)
            //        break;

            if (tempArrCl.Count == 0)
               break;

            cell = tempArrCl[Random.Range(0, tempArrCl.Count)];

            tempArrCl.Remove(cell);
           /* int randomNum = Random.Range(0, tempArr.Count);
            int num = tempArr[randomNum];

            cell = cells[num];
            
            tempArr.RemoveAt(randomNum);
            */
        } while (!cell.IsFreeForMove());

        if (cell == null && attempt < Alien.ClampDistance.Length-1)
            GetRandomFreeCellToMove(clickable, listClickable, ++attempt);

        return cell;
    }

    public Clickable GetClickableToNextFloor(int currentClickable, int currentFloor, int finalFloor)
    {
        switch (currentFloor)
        {
            case 0: return (currentClickable <= 4) ? cells[0] : cells[9];
            case 1:
                if (finalFloor > currentFloor)
                    return (currentClickable <= 13) ? cells[13] : cells[14];
                else
                    return (currentClickable <= 13) ? cells[10] : cells[17];

            case 2:
                if (finalFloor > currentFloor)
                    return (currentClickable <= 21) ? cells[18] : cells[25];
                else
                    return (currentClickable <= 21) ? cells[21] : cells[22];

            case 3: return (currentClickable <= 29) ? cells[26] : cells[33];
            default: return null;
        }

    }

    public bool IsJumping(Clickable temp)
    {
        if (temp == null)
            return false;

        if (temp.num == 0 || temp.num == 9 || temp.num == 13 || temp.num == 14 || temp.num == 10 || temp.num == 17 || temp.num == 18 || temp.num == 25 || temp.num == 21 || temp.num == 22 || temp.num == 26 || temp.num == 33)
            return true;
        else
            return false;
    }

    public bool IsExtremeForWallToLeft(Clickable temp)
    {
        if (temp == null)
            return false;

        if (temp.num == 9 || temp.num == 17 ||  temp.num == 25 || temp.num == 21 || temp.num == 33)
            return true;
        else
            return false;
    }

    public bool IsExtremeForWallToRight(Clickable temp)
    {
        if (temp == null)
            return false;

        if (temp.num == 0 || temp.num == 10 || temp.num == 18 ||   temp.num == 22 || temp.num == 26 )
            return true;
        else
            return false;
    }

    public Alien.JumpDirection GetJumpDirection(Clickable temp)
    {
        if (temp.num == 0 || temp.num == 14 || temp.num == 18)
            return Alien.JumpDirection.RightUp;

        if (temp.num == 9 || temp.num == 13 || temp.num == 25)
            return Alien.JumpDirection.LeftUp;

        if (temp.num == 26 || temp.num == 22 || temp.num == 10)
            return Alien.JumpDirection.LeftDown;

        if (temp.num == 33 || temp.num == 21 || temp.num == 17)
            return Alien.JumpDirection.RightDown;

        return Alien.JumpDirection.None;
    }

    public Clickable GetClickableAfterJump(Clickable temp)
    {
        if (temp.num == 0)
            return cells[10];

        if (temp.num == 9)
            return cells[17];

        if (temp.num == 17)
            return cells[9];

        if (temp.num == 10 )
            return cells[0];



        if (temp.num == 13)
            return cells[21];

        if (temp.num == 14)
            return cells[22];

        if (temp.num == 21)
            return cells[13];

        if (temp.num == 22)
            return cells[14];


        if (temp.num == 18)
            return cells[26];

        if (temp.num == 25)
            return cells[33];

        if (temp.num == 26)
            return cells[18];

        if (temp.num == 33)
            return cells[25];


        Debug.Log("LOL " + temp.num);
        return null;
    }

    public Clickable FindNearestFountain(Alien alien, List<Clickable> listClickable)
    {
        Clickable currentTarget = alien.GetCurrentClickable();
        Clickable clickable = null;

        List<Clickable> temp;

        if (listClickable == null)
        {
            temp = new List<Clickable>(cells);
        }
        else
        {
            temp = listClickable;
        }

      /*  Clickable ignoreFountain = alien.GetIgnoreFountainClickable();

        if (ignoreFountain != null && temp.Contains(ignoreFountain))
            temp.Remove(ignoreFountain);
            */

        foreach (Clickable cell in temp)
        {
            if (cell.IsAvailableFountain())
            {
                // найти ближайший фонтан

                if (clickable == null || GetLenToPos(currentTarget, cell) < GetLenToPos(currentTarget, clickable) /*Mathf.Abs(currentTarget.num - cell.num) < Mathf.Abs(clickable.num - currentTarget.num)*/)
                    clickable = cell;
            }
        }

    //    if(clickable!= null)
     //   Debug.Log(clickable.num);


        return clickable;
    }

    int GetLenToPos(Clickable firstClickable, Clickable secondClickable)
    {

        Vector2 aVector = Lee.GetPosInMatrix(firstClickable.num, firstClickable.GetFloor());
        Vector2 bVector = Lee.GetPosInMatrix(secondClickable.num, secondClickable.GetFloor());
        return Lee.GetLength((int)aVector.x, (int)aVector.y, (int)bVector.x, (int)bVector.y);
    }

    public Clickable GetCurrentClickable(Vector3 pos)
    {
        Clickable clickable = null;

        foreach(Clickable cell in cells)
        {
            bool trueX = false;
            bool trueY = false;

            RectTransform rt = cell.GetComponent<RectTransform>();

            if (rt.position.x + rt.rect.width/2f*library.canvas.scaleFactor > pos.x 
                && rt.position.x - rt.rect.width / 2f * library.canvas.scaleFactor < pos.x)
            {
              
                trueX = true;
            }
            else
            {
                continue;
            }


            if (rt.position.y + rt.rect.height / 2f * library.canvas.scaleFactor > pos.y
              && rt.position.y - rt.rect.height / 2f * library.canvas.scaleFactor < pos.y)
            {
                trueY = true;
            }
            else
            {
                continue;
            }

            if (trueX && trueY)
            {

              clickable = cell;
             break;
            }
        }

        return clickable;
    }
    /*
    public List<Clickable> GetAcceptableClickedWithWall(Alien alien)
    {
        List<Clickable> tempList = new List<Clickable>();

        if (Clickable.BuildingType.Wall.Equals(alien.GetCurrentClickable().GetBuildingType()))
        {

            if (alien.GetCurrentClickable().num == 8 || alien.GetCurrentClickable().num == 24)
            {
                if (alien.transform.position.x < alien.GetCurrentClickable().transform.position.x)
                {
                    tempList.Add(cells[alien.GetCurrentClickable().num + 1]);
                }
                else
                {
                    for (int i = 0; i < alien.GetCurrentClickable().num; i++)
                        tempList.Add(cells[i]);
                    for (int i = alien.GetCurrentClickable().num + 2; i < cells.Length; i++)
                        tempList.Add(cells[i]);
                }
            }
            else if (alien.GetCurrentClickable().num == 16)
            {
                if (alien.transform.position.x > alien.GetCurrentClickable().transform.position.x)
                {
                    tempList.Add(cells[alien.GetCurrentClickable().num + 1]);
                }
                else
                {
                    for (int i = 0; i < alien.GetCurrentClickable().num; i++)
                        tempList.Add(cells[i]);
                    for (int i = alien.GetCurrentClickable().num + 2; i < cells.Length; i++)
                        tempList.Add(cells[i]);
                }
            }
            else
            {
                if ((alien.transform.position.x < alien.GetCurrentClickable().transform.position.x && (alien.GetCurrentFloor() == 0 || alien.GetCurrentFloor() == 2))
                    || (alien.transform.position.x > alien.GetCurrentClickable().transform.position.x && (alien.GetCurrentFloor() == 1 || alien.GetCurrentFloor() == 3)))
                {
                    if (alien.GetCurrentClickable().num == 9 || alien.GetCurrentClickable().num == 25 || alien.GetCurrentClickable().num == 17 || alien.GetCurrentClickable().num == 33 || alien.GetCurrentClickable().num == 0)
                    {
                        
                      
                    }
                    else
                    {
                        for (int i = alien.GetCurrentClickable().num + 1; i < cells.Length; i++)
                            tempList.Add(cells[i]);
                    }
                }
                else
                {
                    if (alien.GetCurrentClickable().num == 9 || alien.GetCurrentClickable().num == 25 || alien.GetCurrentClickable().num == 17 || alien.GetCurrentClickable().num == 33 || alien.GetCurrentClickable().num == 0)
                    {
                        for (int i = 0; i < alien.GetCurrentClickable().num; i++)
                            tempList.Add(cells[i]);
                        for (int i = alien.GetCurrentClickable().num + 1; i < cells.Length; i++)
                            tempList.Add(cells[i]);
                    }
                    else
                    {
                        for (int i = 0; i < alien.GetCurrentClickable().num; i++)
                            tempList.Add(cells[i]);
                    }
                }
            }


        }


        return tempList;
    }*/

    public Clickable GetCell(int num)
    {
        return cells[num];
    }

    public Queue<Clickable> GetWayToClickable(Clickable currentClickable, Clickable finalClickable)
    {
        Vector2 currentPosInMatrix = Lee.GetPosInMatrix(currentClickable.num, currentClickable.GetFloor());
        Vector2 finalPosInMatrix = Lee.GetPosInMatrix(finalClickable.num, finalClickable.GetFloor());
        Vector2[] vectArr = Lee.GetRecoveryWay((int)currentPosInMatrix.x, (int)currentPosInMatrix.y, (int)finalPosInMatrix.x, (int)finalPosInMatrix.y);

        Queue<Clickable> tempQueue = new Queue<Clickable>();
        for (int i = 0; i < vectArr.Length; i++)
        {
           // Debug.Log(vectArr[i]);
                if (i == vectArr.Length - 1)
                {
                    tempQueue.Enqueue(GetClickableByPosInMatrix(vectArr[i]));
                        break;
                }

                if(IsJumping(GetClickableByPosInMatrix(vectArr[i])) && GetClickableByPosInMatrix(vectArr[i+1]) == null)
                {
                    tempQueue.Enqueue(GetClickableByPosInMatrix(vectArr[i]));
                    i += 2;
                }
               
        }
        //Debug.Log(" ");
        
        return tempQueue;
  
    }

    public List<Clickable> GetClickableInArea(Clickable currentClickable, int length)
    {
        Vector2 currentPosInMatrix = Lee.GetPosInMatrix(currentClickable.num, currentClickable.GetFloor());
        Vector2[] vectArr = Lee.GetPositionInArea((int)currentPosInMatrix.x, (int)currentPosInMatrix.y, length);

       // Debug.Log(vectArr.Length);

        List<Clickable> listClickable = new List<Clickable>();

        foreach (Vector2 vect in vectArr)
        {
            Clickable clickable = GetClickableByPosInMatrix(vect);
            /*
            if (clickable != null)
            Debug.Log(vect+" "+clickable.num);
            else
                Debug.Log(vect + " " + "null");
                */

            if (clickable!= null)
                listClickable.Add(clickable);
        }

        return listClickable;

    }


    public Clickable GetClickableByPosInMatrix(Vector2 posInMatrix)
    {
        int x;
        switch ((int)posInMatrix.y)
        {
            case 0:
                x = (int)posInMatrix.x;
                break;

            case 2:
                if (posInMatrix.x == 0 || posInMatrix.x == 9)
                {
                    x = -1;
                    break;
                }

                
                x = (int)posInMatrix.x+ 10 - 1;
                break;

            case 4:

                if (posInMatrix.x == 5 || posInMatrix.x == 4)
                {
                    x = -1;
                    break;
                }

              

                if (posInMatrix.x < 4)
                    x = (int) posInMatrix.x + 18;
                else
                    x = (int) posInMatrix.x + 18 - 2;
                break;

            case 6:

                if (posInMatrix.x == 0 || posInMatrix.x == 9)
                {
                    x = -1;
                    break;
                }
                x = (int)posInMatrix.x + 26 - 1;


                break;

            default: x = -1;  break;
        }

        if (x == -1)
            return null;

        return GetCell(x);

    }

    public Vector2 GetExpulsionTargetPosition(Alien alien, Clickable clickable)
    {
        if(IsExtremeForWallToLeft(clickable))
        {
                return new Vector2(clickable.transform.position.x - library.buildings.wall.rect.width / 2f *2.5f * library.canvas.scaleFactor, alien.transform.position.y);
        }
        else if(IsExtremeForWallToRight(clickable))
        {
            return new Vector2(clickable.transform.position.x+ library.buildings.wall.rect.width / 2f * 2.5f * library.canvas.scaleFactor , alien.transform.position.y);

        }

        else
        {
            if (alien.transform.position.x >= clickable.transform.position.x)
            {
                return new Vector2(clickable.transform.position.x  + library.buildings.wall.rect.width / 2f * library.canvas.scaleFactor * 1.5f, alien.transform.position.y);
            }
            else
                return new Vector2(clickable.transform.position.x  - library.buildings.wall.rect.width / 2f * library.canvas.scaleFactor * 1.5f, alien.transform.position.y);
        }
    }

    public Vector2 GetPusherExpulsionTargetPosition(Alien alien, Clickable clickable)
    {
        Clickable tempClickable = GetRandomClickableOnFloorForExpulsion(alien,clickable);
        float pos = clickable.GetComponent<RectTransform>().sizeDelta.x / 2f * Random.Range(0f, 0.9f);

        if (Random.Range(1, 3) == 2)
            pos *= (-1);

        return new Vector2(tempClickable.transform.position.x + pos* library.canvas.scaleFactor, alien.transform.position.y);
    }

    Clickable GetRandomClickableOnFloorForExpulsion(Alien alien, Clickable currentClickable)
    {
        List<Clickable> tempList = new List<Clickable>();

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].GetFloor() == currentClickable.GetFloor() && cells[i] != currentClickable && Mathf.Abs(cells[i].num - currentClickable.num) <= GameplayConstants.PusherRange)
            {

                if ((IsExtremeForWallToLeft(currentClickable) || IsExtremeForWallToRight(currentClickable))
                    || (alien.transform.position.x <= currentClickable.transform.position.x && cells[i].num < currentClickable.num)
                    || (alien.transform.position.x > currentClickable.transform.position.x && cells[i].num > currentClickable.num))
                {
                    if (cells[i].GetFloor() == 2
                            && ((cells[i].num >= 18 && cells[i].num <= 21 && currentClickable.num >= 22 && currentClickable.num <= 25) ||
                                (cells[i].num >= 22 && cells[i].num <= 25 && currentClickable.num >= 18 && currentClickable.num <= 21)))
                        continue;

                    tempList.Add(cells[i]);

                }
            }
        }

        return tempList[Random.Range(0, tempList.Count)];

        
    }



    public void OnHighlightAllActiveClickable()
    {
        Clickable.BuildingType buildingType = library.uiButtonsController.GetCurrentElementType();

        lightIsOn = true;

        foreach (Clickable cl in cells)
                cl.OnHighlight(buildingType);
                   
    }

    public void OffHighlightAllActiveClickable()
    {
        lightIsOn = false;
        foreach (Clickable cl in cells)
                cl.OffHighlight();
           
    }

    public void LandWasChanged()
    {
        if (lightIsOn)
            OnHighlightAllActiveClickable();
    }

    public void ToDefault()
    {
        lightIsOn = false;
        foreach (Clickable cell in cells)
            cell.ToDefault();
    }

}
