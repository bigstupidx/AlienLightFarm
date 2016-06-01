using UnityEngine;
using System.Collections;

public class GameplayConstants {

    public const float AlienFullLiveTime = 12f;
    public const float AlienMaxWaitTime = 3f;
    public const float AlienMinWaitTime = 1f;
    public const float AlienNormalSpeed = 30f;
    public const float AlienHungrySpeed = 60f;

    public static int[] AlienClampDistance = { 5, 10, 20, 34 };
    public const int AlienMinDistance = 2;

    public const float AlienHungryCoef = 0.5f;


    public const int AlienStartBorningCount = 3;


    // Скорость появления алиенов
    public const float AlienBornTime = 2f;
    public const float AlienBorningDelay = 1.4f;



    public static float[] FountainButtonReloadTime = {2.5f,3.5f,4.5f,5.5f};
    public const float SafeCupolButtonReloadTime = 3f;
    public static float HealingButtonReloadTime = 10f;
    //public static float PusherButtonReloadTime = 5f;
    public static float BlackHoleButtonReloadTime = 5f;



    //    public const float WallButtonReloadTime = 1f;



    public const float FountainMaxLife = 210f;
    public const float FountainMinLife = 10f;

    public const float FountainGrowCoef = 2f;
    public const float FountainRecoveryCoef = 4f;

    public const float WallLifeTime = 7f;
    public const float SafeCupolLifeTime = 5f;

    public const float BlackHoleClosedSpeed = 1f;

    public const float AlienCoefRateFountain = 7f;
    // public const float FountainCoefRestitution = 8f;

    public const int MaxAgro = 100;
    //  public static float[] AgroCoef = {60,40,25,20,15};
    public static float AgroCoef = 20f;
  //  public static int[] AgroCoefAlienCountTreshold = { 0, 10, 15, 20, 25 };
    public const float RecoveryAgroCoef = 7f;

    /*
    public const int FountainTreshold = 0;
    public const int WallTreshold = 1000000;
    public const int SafeCupolTreshold = 10;
    public const int BlackHoleTreshold = 1000000;
    public const int PusherTreshold = 20;
    public const int HealingTreshold = 30;
    */
    public static int[] AliensTresholds = { 0, 10, 20, 30 };

    public const int PusherRange = 3;
    public const float PusherExpansionSpeed = 5f;

    public const float ExpulsionAlienSpeed = 7f;

    public const float LentaReloadTime = 2f;

    public const int AdMoneyReward = 20;
    public const int PurchasePrice = 200;

    public static int[] OrderOpeningReward = {0, 1 };

    public const int MoneyDevider = 5;
}
