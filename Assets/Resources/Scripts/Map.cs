using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Map : MonoBehaviour {

    public Clickable[] cells;
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

        List<Clickable> tempArrCl = GetClickableInArea(clickable, GameplayConstants.AlienMinDistance, GameplayConstants.AlienClampDistance[attempt]);
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

        if (cell == null && attempt < GameplayConstants.AlienClampDistance.Length - 1)
            GetRandomFreeCellToMove(clickable, listClickable, ++attempt);

        return cell;
    }

    /*
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

    }*/

    public bool IsJumping(Clickable temp)
    {
        if (temp == null)
            return false;

        if (temp.num == 3 || temp.num == 4 || temp.num == 8 || temp.num == 10 || temp.num == 11 || temp.num == 13 || temp.num == 14 || temp.num == 19)
            return true;
        else
            return false;
    }

    
    public bool IsRightJump(int num)
    {

        if (num == 4 || num == 8 ||  num == 10 || num == 19)
            return true;
        else
            return false;
    }

    public bool IsLeftJump(int num)
    {

        if (num == 3 || num == 11 || num == 13 || num == 14)
            return true;
        else
            return false;
    }

    /*
public bool IsExtremeForWallToRight(Clickable temp)
{
    if (temp == null)
        return false;

    if (temp.num == 0 || temp.num == 10 || temp.num == 18 ||   temp.num == 22 || temp.num == 26 )
        return true;
    else
        return false;
}*/

    public Alien.JumpDirection GetJumpDirection(Clickable temp)
    {
        if (temp.num == 4 || temp.num == 8)
            return Alien.JumpDirection.RightUp;

        if (temp.num == 3 || temp.num == 13)
            return Alien.JumpDirection.LeftUp;

        if (temp.num == 11 || temp.num == 14)
            return Alien.JumpDirection.LeftDown;

        if (temp.num == 10 || temp.num == 19)
            return Alien.JumpDirection.RightDown;

        return Alien.JumpDirection.None;
    }

    public Clickable GetClickableAfterJump(Clickable temp)
    {
        if (temp.num == 3)
            return cells[10];

        if (temp.num == 10)
            return cells[3];

        if (temp.num == 4)
            return cells[11];

        if (temp.num == 11)
            return cells[4];



        if (temp.num == 8)
            return cells[14];

        if (temp.num == 14)
            return cells[8];

        if (temp.num == 13)
            return cells[19];

        if (temp.num == 19)
            return cells[13];



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

    public Clickable GetCurrentClickable(Vector2 pos)
    {
        Clickable clickable = null;

        foreach (Clickable cell in cells)
        {
            bool trueX = false;
            bool trueY = false;

            RectTransform rt = cell.GetComponent<RectTransform>();

            if (rt.anchoredPosition.x + rt.rect.width / 2f  > pos.x
                && rt.anchoredPosition.x - rt.rect.width / 2f  < pos.x)
            {

                trueX = true;
            }
            else
            {
                continue;
            }


            if (rt.anchoredPosition.y + rt.rect.height / 2f  > pos.y
              && rt.anchoredPosition.y - rt.rect.height / 2f  < pos.y)
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

            if (IsJumping(GetClickableByPosInMatrix(vectArr[i])) && GetClickableByPosInMatrix(vectArr[i + 1]) == null)
            {
                tempQueue.Enqueue(GetClickableByPosInMatrix(vectArr[i]));
                i += 2;
            }

        }
        //Debug.Log(" ");

        return tempQueue;

    }

    public List<Clickable> GetClickableInArea(Clickable currentClickable, int min, int length)
    {
        Vector2 currentPosInMatrix = Lee.GetPosInMatrix(currentClickable.num, currentClickable.GetFloor());
        Vector2[] vectArr = Lee.GetPositionInArea((int)currentPosInMatrix.x, (int)currentPosInMatrix.y, min, length);

        List<Clickable> listClickable = new List<Clickable>();

        foreach (Vector2 vect in vectArr)
        {
            Clickable clickable = GetClickableByPosInMatrix(vect);

            if (clickable != null)
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
                if (posInMatrix.x == 3 || posInMatrix.x == 4)
                {
                    x = -1;
                    break;
                }

                if (posInMatrix.x < 3)
                    x = (int)posInMatrix.x + 8;
                else
                    x = (int)posInMatrix.x + 8 - 2;
                break;



            case 4:

                if (posInMatrix.x == 0 || posInMatrix.x == 7)
                {
                    x = -1;
                    break;
                }


                x = (int)posInMatrix.x + 14 - 1;
                break;
            /*
        case 6:

            if (posInMatrix.x == 0 || posInMatrix.x == 9)
            {
                x = -1;
                break;
            }
            x = (int)posInMatrix.x + 26 - 1;


            break;
            */
            default: x = -1; break;
        }

        if (x == -1)
            return null;

        return GetCell(x);

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


    public int GetFloorByClickableNum(int num)
    {

        int floor = 0;

        if (num< 8)
            floor = 0;
        else if (num< 14)
            floor = 1;
        else if (num< 20)
            floor = 2;

        return floor;
    }
}
