using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Lee : MonoBehaviour
{

    const int W = 10;         // ширина рабочего поля
    const int H = 7;         // высота рабочего поля
    public const int WALL = -1;         // непроходимая ячейка
    public const int BLANK = -2;         // свободная непомеченная ячейка
    public const int AIR = -3;         // свободная непомеченная ячейка

    //  int px[W * H], py[W * H];      // координаты ячеек, входящих в путь
    public static int[,] startGrid = {
        {BLANK,BLANK,BLANK,BLANK,BLANK,BLANK,BLANK,BLANK,BLANK,BLANK},
        {AIR,  WALL, WALL, WALL, WALL, WALL, WALL, WALL, WALL, AIR },
        {AIR,  BLANK,BLANK,BLANK,BLANK,BLANK,BLANK,BLANK,BLANK,AIR},
        {WALL, WALL, WALL, WALL, AIR,  AIR,  WALL, WALL, WALL, WALL },
        {BLANK,BLANK,BLANK,BLANK,AIR,  AIR,  BLANK,BLANK,BLANK,BLANK},
        {AIR,  WALL, WALL, WALL, WALL, WALL, WALL, WALL, WALL, AIR },
        {AIR,  BLANK,BLANK,BLANK,BLANK,BLANK,BLANK,BLANK,BLANK,AIR},
       };  
                  // рабочее поле

    static int[] dx = { 0,0,1, -1, };  
    static int[] dy = { 1, -1,0,0 };

    // Перед вызовом lee() массив grid заполнен значениями WALL и BLANK

    public static Vector2[] GetRecoveryWay(int ax, int ay, int bx, int by)   // поиск пути из ячейки (ax, ay) в ячейку (bx, by)
    {
        int[,] grid;

        WavePropogation(ax, ay/*, bx, by*/, out grid);
        int len;
        /*
        for (int j = grid.GetLength(0) - 1; j >= 0; j--)
        {
            string str = "";
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                string qwe = " ";
                if (grid[j,i] >=  0 && grid[j,i] < 10)
                {
                    qwe = "  ";
                }
                str += grid[j, i] + qwe;
            }
            Debug.Log(str);
        }
        Debug.Log(" ");*/
        //  if (grid[by,bx] == BLANK) return false;  // путь не найден

        // восстановление пути

        // ЗНАЧЕНИЕ grid[by,bx] было равно -1 . А должно быть >= 0
        len = grid[by,bx];            // длина кратчайшего пути из (ax, ay) в (bx, by)

        if (len < 0)
        {
            len = 0;
        }
        Vector2[] vectArr = RecoveryWay(ax, ay, bx, by,len,grid);
        return vectArr;
        
    }

    public static Vector2[] GetPositionInArea(int ax, int ay, int len)
    {
        int[,] grid;

        WavePropogation(ax, ay, out grid);

        List<Vector2> vectList = new List<Vector2>();
      //  Debug.Log(startGrid.GetLength(0));
        for(int j = 0; j < grid.GetLength(0); j++)
            for(int i = 0; i<grid.GetLength(1); i++)
            {
                if (grid[j, i] > 0 && grid[j, i] <= len/* && !Has2AirBetween(j,i,ay,ax)/* && и между ними нет 2 air*/)
                    vectList.Add(new Vector2(i,j));
            }


        
        return vectList.ToArray();
    }

    static bool Has2AirBetween(int j1, int i1, int j2, int i2)
    {
        if (j1 != j2)
            return false;

        int firstX;
        int secondX;


        if (i1 > i2)
        {
            firstX = i2;
            secondX = i1;
        }
        else
        {
            firstX = i1;
            secondX = i2;
        }


     //   if()
        
        return true;
    }

    static Vector2[] RecoveryWay(int ax, int ay, int bx, int by, int len, int[,] grid)
    {
        int d, x, y, k;
        Vector2[]  vectArr = new Vector2[len+1];

        x = bx;
        y = by;
        d = len;
        while (d > 0)
        {
            vectArr[d] = new Vector2(x, y);                   // записываем ячейку (x, y) в путь
            d--;
            for (k = 0; k < 4; ++k)
            {
               int iy = y + dy[k];
               int ix;
         //      if (startGrid[y, x] != AIR)
                    ix = x + dx[k];
         //      else
         //           ix = x;

                if (iy >= 0 && iy < H && ix >= 0 && ix < W &&
                     grid[iy, ix] == d)
                {
                    
             /*       if (startGrid[y, x] == AIR)
                    {
                        y = y + dy[k];
                        break;// переходим в ячейку, которая на 1 ближе к старту
                    }
                    else
                    {*/
                        x = x + dx[k];
                        y = y + dy[k];           // переходим в ячейку, которая на 1 ближе к старту
                        break;
                   // }
                }
            }
        }
        vectArr[0] = new Vector2(ax, ay);

        
        return vectArr;
    }

    static void WavePropogation(int ax, int ay,/*, int bx, int by, */out int[,] grid)
    {
        grid = (int[,])startGrid.Clone();

        int d, x, y, k;
        bool stop;

        // if (grid[ay,ax] == WALL || grid[by,bx] == WALL) return false;  // ячейка (ax, ay) или (bx, by) - стена

        // распространение волны
        d = 0;
        grid[ay, ax] = 0;            // стартовая ячейка помечена 0
        do
        {
            stop = true;               // предполагаем, что все свободные клетки уже помечены
            for (y = 0; y < H; ++y)
                for (x = 0; x < W; ++x)
                    if (grid[y, x] == d)                         // ячейка (x, y) помечена числом d
                    {
                        for (k = 0; k < 4; ++k)                    // проходим по всем непомеченным соседям
                        {
                            /*    int iy = y + dy[k];
                                int ix;// = x + dx[k];

                                if (startGrid[y, x] == AIR)
                                {
                                    ix = x;
                                }
                                else
                                    ix = x + dx[k];
                                    */


                            int iy = y + dy[k], ix = x + dx[k];

                           
                            
                            if (iy >= 0 && iy < H && ix >= 0 && ix < W &&
                                 (grid[iy, ix] == BLANK || grid[iy,ix] == AIR))
                            {

                                if (startGrid[y, x] == AIR && startGrid[iy, ix] == AIR)
                                {
                                    if (k == 0 || k == 1)
                                    {
                                        stop = false;
                                        grid[iy, ix] = d + 1;
                                        break;
                                    }
                                    if (k == 2 || k == 3) // если боковые
                                    {
                                        continue;
                                    }
                                }
                                
                               

                                    stop = false;              // найдены непомеченные клетки
                                    grid[iy, ix] = d + 1;      // распространяем волну
                                
                            }
                        }
                    }
            d++;
        } while (!stop /*&& grid[by, bx] == BLANK*/);
    }

    public static int GetLength(int ax, int ay, int bx, int by)
    {
        int[,] grid;

        WavePropogation(ax, ay,/*, bx, by, */out grid);

        return grid[by, bx];
    }

    /*
    public static int GetLenToPos(int currentClickableNumStart, int floorStart, int currentClickableNumFinal, int floorFinal)
    {

        Vector2 aVector = GetPosInMatrix(currentClickableNumStart, floorStart);
        Vector2 bVector = GetPosInMatrix(currentClickableNumFinal, floorFinal);
       

        return GetLength((int) aVector.x, (int)aVector.y, (int)bVector.x, (int) bVector.y);
    }*/

    public static Vector2 GetPosInMatrix(int currentClickableNum, int floor)
    {
        int x;
        int y;
        switch (floor)
        {
            case 0:
                y = 0;
                x = currentClickableNum;
                break;

            case 1:
                y = 2;
                x = currentClickableNum % 10 + 1;
                break;

            case 2:
                y = 4;

                if (currentClickableNum - 18 < 4)
                    x = currentClickableNum - 18;
                else
                    x = currentClickableNum - 18 + 2;
                break;

            case 3:
                y = 6;
                x = currentClickableNum - 26 + 1;
                break;

            default: x = 0; y = 0; break;
        }

        return new Vector2(x, y);
    }

    /*
    public static Queue<Clickable> GetWayToClickable(Clickable currentClickable, Clickable finalClickable)
    {
        Vector2 currentPosInMatrix = GetPosInMatrix(currentClickable.num, currentClickable.GetFloor());
        Vector2 finalPosInMatrix = GetPosInMatrix(finalClickable.num, finalClickable.GetFloor());

        Vector2[] vectArr = GetRecoveryWay((int) currentPosInMatrix.x, (int) currentPosInMatrix.y, (int) finalPosInMatrix.x, (int)finalPosInMatrix.y);

        Queue<Clickable> queueClickable = new Queue<Clickable>();

        //ЗАКОНЧИЛ ТУТ
       // foreach (Vector2 vect in vectArr)
   //         queueClickable.Enqueue(GetClickableByPosInMatrix(vect));


        return new Queue<Clickable>();
    }*/
    

}
