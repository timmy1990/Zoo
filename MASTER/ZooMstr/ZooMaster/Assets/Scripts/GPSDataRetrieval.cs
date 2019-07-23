using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;



public class GPSDataRetrieval : MonoBehaviour
{
    private fakeGPS fakeGpsScript;
    public Text textobject;
    public Text updateGPStext;
    public AndroidJavaObject gpsInstance;
    public AndroidJavaObject contextUnit;
    private Rigidbody rigi;
    private int oncePerSecond = 0;
    private fakeGPS fake = new fakeGPS();

    private float speed = 10.0F;

    // Variables for Coordinatetransformation - Entrance Zoo Nürnberg
    double tmplat = 49.450199;
    double tmplon = 11.139338;
    private CoordinateUtilities coordUtil;
    double tmpDegree = 0;

    // Lerping
    private float lastTime;
    private int index = 0;
    private float strecke = 0;

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

        // initialize lerping
        lastTime = Time.time;
        int index = 0;
        float strecke = 0;
    }

    public void activateGPSData()
    {
        // Create Instance Of Plugin
        Debug.Log("Zoorino: Skript for Plugin Load startet");
        gpsInstance = new AndroidJavaObject("plugin.unity.zoorino.gpsplugin.gpsPlug");
        if (gpsInstance == null)
        {
            Debug.Log("Zoorino: Plugin nicht geladen");
        }
        else
        {
            Debug.Log("Zoorino: Plugin geladen");
        }

        // Get UnityActivity and Context
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

        if (context == null)
        {
            textobject.text = "Zoorino: context ist null";
        }
        else
        {
            textobject.text = "context = nicht null";
        }

        string tmpstr = gpsInstance.Call<string>("prepareGPS", context);

        textobject.text = "Ende von PlugIn: " + tmpstr;
        Debug.Log("Zoorino: prepareGPS: " + tmpstr);

        string mdf = gpsInstance.Call<string>("retStrFrContext", context);
        textobject.text = mdf;
    }

    public void giveGPSData()
    {
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

    //rotation utility
   double calcRotation(double degree) {
        return degree;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Lerp: index=" + index + " strecke=" + strecke);

        // Retrieve fakeGPS Data from dictionary
        float height = 50;
        double[] latlon1 = fake.getGPS(index);
        double[] latlon2 = fake.getGPS(index+1);
        double[] pos1 = coordUtil.geo_to_enu((float)latlon1[0], (float)latlon1[1], height);
        double[] pos2 = coordUtil.geo_to_enu((float)latlon2[0], (float)latlon2[1], height);

        // LERP
        Vector3 v1 = new Vector3((float)pos1[0], (float)pos1[2], (float)pos1[1]);
        Vector3 v2 = new Vector3((float)pos2[0], (float)pos2[2], (float)pos2[1]);
        float delta = Vector3.Distance(v1, v2);
        float w = strecke / delta;
        rigi.transform.position = (1 - w) * v1 + w * v2;

        // update for next lerp
        float dt = Time.time - lastTime;
        lastTime = Time.time;
        strecke += speed * dt;
        if (strecke > delta) {
            index++;
            strecke -= delta;
        }
        int n = fake.getNumPoints();
        if (index >= n-1) {
            index = 0;
            strecke = 0;
        }
    }
}



