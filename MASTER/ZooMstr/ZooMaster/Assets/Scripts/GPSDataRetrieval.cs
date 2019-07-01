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

// Variables for Coordinatetransformation - Entrance Zoo Nürnberg
double tmplat = 49.450199;
double tmplon = 11.139338;
private CoordinateUtilities coordUtil;
double tmpDegree = 0;

    // Start is called before the first frame update
    void Start()
    {
        // get reference to player Instance for transformations
        rigi = GetComponent<Rigidbody>();

        // Initialize with Startpoint
        coordUtil = new CoordinateUtilities(tmplat, tmplon, 0.00);

        // Testing if it works
        double[] enu2 = coordUtil.geo_to_enu(tmplat, tmplon, 0.00);
        Debug.Log("ENU: X: " + enu2[0] + ", Y: " + enu2[1]);

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
        // Call Plugin for GPS Data
        double[] tmpDoubArr = gpsInstance.Call<double[]>("giveGPSData");
        updateGPStext.text = "GPS: Lat: " + tmpDoubArr[0] + ", Lon: " + tmpDoubArr[1];
        // Convert GPS Data into local coordinates
        double[] enu = coordUtil.geo_to_enu(tmpDoubArr[0], tmpDoubArr[1], 0.00);
        updateGPStext.text = "ENU: X: " + enu[0] + ", Y: " + enu[1];

        // Set Position of Player Instance
        // Alternate: rigi.position for non-interpolatet movement between positions
        Vector3 movement = new Vector3((float)tmpDoubArr[0], 0, (float)tmpDoubArr[1]);
        rigi.MovePosition(transform.position + movement);
    }

    // rotation utility
    double calcRotation(double degree) {
<<<<<<< HEAD
        
=======

>>>>>>> 0dd976a6183a834163311a6196c6bc9af6690d95
        return degree;
    }
    // Update is called once per frame
    void Update()  {
    }
}
