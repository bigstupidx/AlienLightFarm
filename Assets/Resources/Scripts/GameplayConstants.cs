using UnityEngine;
using System.Collections;

public class GameplayConstants {

    public const float AlienFullLiveTime = 11f;
    public const float AlienMaxWaitTime = 3f;
    public const float AlienMinWaitTime = 1f;
    public const float AlienNormalSpeed = 30f;
    public const float AlienHungrySpeed = 60f;

    public const int AlienStartBorningCount = 3;


    // Скорость появления алиенов
    public const float AlienBornTime = 2f;
    public const float AlienBorningDelay = 0.9f;



    public static float[] FountainButtonReloadTime = {2f,3f,4f,5f};
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

    public const float AlienCoefRateFountain = 3f;
    // public const float FountainCoefRestitution = 8f;

    public const int MaxAgro = 100;
    //  public static float[] AgroCoef = {60,40,25,20,15};
    public static float AgroCoef = 10f;
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
    public const int PurchasePrice = 0;

    public static int[] OrderOpeningReward = {0, 1 }; 
}
