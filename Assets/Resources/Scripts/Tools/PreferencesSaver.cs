using UnityEngine;
using System.Collections;
using System;

public class PreferencesSaver : MonoBehaviour
{

    public static string pass = "HeyKey";

    public static void SetBestScore(int score)
    {
      //  if (GetBestScore() < score)
            SavePref("BestScore", score+"");
    }

    public static int GetBestScore()
    {
        string val = GetPref("BestScore", 0 + "");
        return int.Parse(val);
    }

    public static void SetMoney(int money)
    {
        SavePref("Money", money + "");
    }

    public static int GetMoney()
    {
        string val = GetPref("Money", 0 + "");
        return int.Parse(val);
    }

    public static void SetCurrentElementInMarket(int num)
    {
        SavePref("CurrentElementInMarket",  num+ "");
    }


    public static int GetCurrentElementInMarket()
    {
        string val = GetPref("CurrentElementInMarket", 0 + "");
        return int.Parse(val);
    }

    public static void SetOpenElementInMarket(int num)
    {
        SavePref("IsOpenElementInMarket_"+num, true + "");
    }

    public static bool IsOpenElementInMarket(int num)
    {
        string val = GetPref("IsOpenElementInMarket_"+num, false + "");
        return bool.Parse(val);
    }


    public static void SetEnterInVkGroup()
    {
        SavePref("EnterInVkGround", true + "");
    }

    public static bool IsEnterInVkGroup()
    {
        string val = GetPref("EnterInVkGround", false + "");
        return bool.Parse(val);
    }

    public static void SetDatePostVk(DateTime dateTime)
    {
        SavePref("DatePostVk", dateTime + "");
    }

    public static DateTime GetDatePostVk()
    {
        try
        {
            return System.DateTime.Parse(GetPref("DatePostVk", System.DateTime.MinValue.ToString()));
        }
        catch (FormatException)
        {
            return System.DateTime.MinValue;
        }
    }


    public static int GetNumReward()
    {
        string val = GetPref("NumReward", 0 + "");
        return int.Parse(val);
    }

    public static void AddRewardNum()
    {
        SavePref("NumReward", (GetNumReward() + 1)+"");
    }

    private static void SavePref(string key, string val)
    {
        SecurePlayerPrefs.SetString(key, val, pass);
        PlayerPrefs.Save();
    }

    private static string GetPref(string key, string defaultValue)
    {
        return SecurePlayerPrefs.GetString(key, defaultValue, pass);
    }


}
