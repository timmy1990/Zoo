using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GPSDataRetrieval : MonoBehaviour {
public Text textobject;
public Text updateGPStext;
public AndroidJavaObject gpsInstance;
public AndroidJavaObject contextUnit;
private Rigidbody rigi;

// Variables for Coordinatetransformation - coordiantes tafelfeldstraße 69
double tmplat = 49.448380;
double tmplon = 11.096157;
private CoordinateUtilities coordUtil;
double tmpDegree = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        Abcd tmpabcd = new Abcd();
        Debug.Log("TEST VON ABCD: " + tmpabcd.returnNbr(23.00));
        Debug.Log("TEST VON ABCD: " + tmpabcd.sds + " " + tmpabcd.pi);

        coordUtil = new CoordinateUtilities(tmplat, tmplon, 50.00);
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
        double[] enu = coordUtil.geo_to_enu(tmpDoubArr[0], tmpDoubArr[1], 50.00);
        updateGPStext.text = "ENU: X: " + tmpDoubArr[0] + ", Y: " + tmpDoubArr[1];

        Vector3 movement = new Vector3(tmpDoubArr[0], 0, tmpDoubArr[1]);
        rigi.MovePosition(transform.position + movement);
    }

    // rotation utility
    double calcRotation(double degree) {

        return degree;
    }
    // Update is called once per frame
    void Update()  {
    }
}
