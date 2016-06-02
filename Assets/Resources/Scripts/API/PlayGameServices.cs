using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class PlayGameServices : MonoBehaviour {

    // Use this for initialization

    public string leaderboard = "CgkI2YCTw-odEAIQBg";

    void Start () {
        
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
       // PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

       
    }


    public void Authenticate()
    {
        Social.localUser.Authenticate((bool success) => {
           
        });
    }

    public void ShowLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboard);

    }

    public void AdScore(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboard, (bool success) =>
            {/*
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else
                {
                    Debug.Log("Update Score Fail");
                }*/
            });
        }
    }




    // Update is called once per frame
    void Update () {
	
	}
}
