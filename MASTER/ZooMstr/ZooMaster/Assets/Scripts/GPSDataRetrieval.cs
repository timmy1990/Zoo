using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GPSDataRetrieval : MonoBehaviour
{
public Text textobject;
public Text updateGPStext;
public AndroidJavaObject gpsInstance;
public AndroidJavaObject contextUnit;

    // Start is called before the first frame update
    void Start()
    {
        // Call for Location Permission
        if (!Permission.HasUserAuthorizedPermission("android.permission.ACCESS_FINE_LOCATION"))
        {
            Permission.RequestUserPermission("android.permission.ACCESS_FINE_LOCATION");
            Permission.RequestUserPermission("android.permission.ACCESS_COARSE_LOCATION");
            Permission.RequestUserPermission("android.permission.TYPE_ORIENTATION");
        }
    }

    public void activateGPSData(){
        // Create Instance Of Plugin
        Debug.Log("Zoorino: Skript for Plugin Load startet");
        gpsInstance = new AndroidJavaObject("plugin.unity.zoorino.gpsplugin.gpsPlug");
        if (gpsInstance == null) {
            Debug.Log("Zoorino: Plugin nicht geladen");
        } else {
            Debug.Log("Zoorino: Plugin geladen"); 
        } 
        

        // Get UnityActivity and Context
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
        
        //contextUnit = context;
        if (context == null){
            textobject.text = "Zoorino: context ist null";
        } else {
            textobject.text = "context = nicht null";
        }

        string tmpstr = gpsInstance.Call<string>("prepareGPS", context);

        textobject.text = "Ende von PlugIn: " + tmpstr;
        Debug.Log("Zoorino: prepareGPS: " + tmpstr);

        string mdf = gpsInstance.Call<string>("retStrFrContext", context);
        textobject.text = mdf;
    }

    public void giveGPSData(){
        double[] tmpDoubArr = gpsInstance.Call<double[]>("giveGPSData");
        updateGPStext.text = "GPS: Lat: " + tmpDoubArr[0] + ", Lon: " + tmpDoubArr[1];
    }

    // Update is called once per frame
    void Update()  {
        
    }
}
