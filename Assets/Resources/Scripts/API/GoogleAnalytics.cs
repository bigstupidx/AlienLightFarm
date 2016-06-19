using UnityEngine;
using System.Collections;

public class GoogleAnalytics : MonoBehaviour {


        public GoogleAnalyticsV4 componentAnalitycs;

        void Awake()
        {
            componentAnalitycs.StartSession();
            componentAnalitycs.LogEvent("Default Category", "Default Action", "Default Description", 0L);
        }

        void OnApplicationQuit()
        {
            componentAnalitycs.StopSession();

        }
    
}
