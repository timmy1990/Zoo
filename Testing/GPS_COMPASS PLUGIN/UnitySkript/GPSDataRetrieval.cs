using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GPSDataRetrieval : MonoBehaviour
{
public Text textobject;
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

        // Create Instance Of Plugin
        Debug.Log("Zoorino: Skript for Plugin Load startet");
        gpsInstance = new AndroidJavaObject("plugin.unity.zoorino.gpsplugin.gpsPlug");
        if (gpsInstance == null) {
            Debug.Log("Zoorino: Plugin nicht geladen");
        }
        Debug.Log("Zoorino: Plugin geladen");
        

        double pdouble = gpsInstance.Call<double>("giveAttributeDouble");
        Debug.Log("Zoorino: giveAttributeDouble" + pdouble);
        string s1 = gpsInstance.Call<string>("returnString");
        Debug.Log("Zoorino: returnString" + s1);

        // Get UnityActivity and Context
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
        contextUnit = context;
        if (context == null){
            Debug.Log("Zoorino: context ist null");
        }
        
        string resultStr = gpsInstance.Call<string>("retStrFrContext", context);
        Debug.Log("Zoorino: retStrFrContext= " + resultStr);
        textobject.text = "1. " + s1 + " 2. " + pdouble + " 3. " + resultStr;

        // GPS functions

        //string resultFromPermission = gpsInstance.Call<string>("checkPermission", activity);
        //Debug.Log("Zoorino: checkPermission: " + resultFromPermission);
        // string tmpstr = gpsInstance.Call<string>("prepareGPS", context);
        // Debug.Log("Zoorino: prepareGPS: " + tmpstr);
 
    }

    public void activateGPSData(){
        string tmpstr = gpsInstance.Call<string>("prepareGPS", contextUnit);
        textobject.text = "" + tmpstr;
        Debug.Log("Zoorino: prepareGPS: " + tmpstr);
       
    }

    public void giveGPSData(){
        double[] tmpDoubArr = gpsInstance.Call<double[]>("giveGPSData");
        textobject.text = "GPS: Lat: " + tmpDoubArr[0] + ", Lon: " + tmpDoubArr[1];
    }

    // Update is called once per frame
    void Update()  {
        
    }
}
