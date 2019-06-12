using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class compassDataRetrieval : MonoBehaviour
{
    public AndroidJavaObject compassInstance;
    public AndroidJavaObject contextUnit;
    public Text textobject;

    // Start is called before the first frame update
    void Start()
    {
        compassInstance = new AndroidJavaObject("plugin.unity.zoorino.compassplugin.CompassPlug");
        
        // Get UnityActivity and Context
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

        
        if (context == null){
            Debug.Log("Zoorino: context ist null");
        } else {
            contextUnit = context;
            string tmpActivateCompStr = compassInstance.Call<string>("activateCompass", context);
            Debug.Log("ZOORINO: COMPASSAKTIVIERUNG: " + tmpActivateCompStr);
        }
    }

    public void updateCompassData(){
        compassInstance = new AndroidJavaObject("plugin.unity.zoorino.compassplugin.CompassPlug");
        if (contextUnit == null){
            Debug.Log("Zoorino: context ist null");
        } else {
            string tmpActivateCompStr = compassInstance.Call<string>("activateCompass", contextUnit);
            Debug.Log("ZOORINO: COMPASSAKTIVIERUNG: " + tmpActivateCompStr);
        }
    }

    // Update is called once per frame
    void Update()
    {
        textobject.text = "ZOORINO: COMPASS = " + compassInstance.Call<double>("getDegree");
    }
}
