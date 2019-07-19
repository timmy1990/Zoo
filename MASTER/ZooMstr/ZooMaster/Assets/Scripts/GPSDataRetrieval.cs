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
    private Transform startPos;
    private Transform endPos;
    public float speed = 1.0F;
    public float smooth = 5.0F;
    private float journeyLength;
    private float startTime; 
    string currentStartPoint;



    // Variables for Coordinatetransformation - Entrance Zoo Nürnberg
    double tmplat = 49.450199;
    double tmplon = 11.139338;
    private CoordinateUtilities coordUtil;
    double tmpDegree = 0;

    Dictionary<string, double[]> coordinates = new Dictionary<string, double[]>(){
       {"0", new[] { 49.4502176, 11.1393087}},
       {"1", new[] { 49.4501723, 11.1392416 } },
       {"2", new[] { 49.4500502, 11.1393597 } },
       {"3", new[] { 49.4499142, 11.1393382 } },
       {"4", new[] { 49.4497015, 11.1392604 } },
       {"5", new[] { 49.4493161, 11.139196 } },
       {"6", new[] { 49.4492028, 11.1392095 } },
       {"7", new[] { 49.4489552, 11.1392765 } },
       {"8", new[] { 49.4487895, 11.1392148 } },
       {"9", new[] { 49.4486849, 11.1392416 } },
       {"10", new[] { 49.4485279, 11.1393006 } },
       {"11", new[] { 49.4484791, 11.1393892 } },
       {"12", new[] { 49.4484251,11.1396708 } },
       {"13", new[] { 49.4483448, 11.139821 } },
       {"14", new[] { 49.4484041, 11.1397003 } },
       {"15", new[] { 49.4481112, 11.1396171 } },
       {"16", new[] { 49.4480058, 11.1396091 } },
       {"17", new[] { 49.4478785, 11.1397218 } },
       {"18", new[] { 49.4477634, 11.1400785 } },
       {"19", new[] { 49.4477162, 11.140469 } },
       {"20", new[] { 49.4477581, 11.1408364 } } };

     // Start is called before the first frame update
    void Start()
    {
        /*currentStartPoint = "0";
        SetPoints();
        Dictionary.Add("0", new double[2]{ 49.450199, 11.139338 });
        Dictionary.Add("1", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("2", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("3", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("4", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("5", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("6", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("7", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("8", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("9", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("10", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("11", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("12", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("13", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("14", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("15", new double[2] {49.450199, 11.139338 });
        Dictionary.Add("16", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("17", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("18", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("19", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("20", new double[2] { 49.450199, 11.139338 });
        Dictionary.Add("21", new double[2] { 49.450199, 11.139338 });
       
      */  // get reference to player Instance for transformations

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

    /*void SetPoints()
    {
        startPos = coordinates[currentStartPoint];
        endPos = coordinates[currentStartPoint + 1];
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos.position, endPos.position);
    }
    */
    public void generateGPSNodes()
    {

    }
    public void lerp()
    {

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
        return degree;
    }
    // Update is called once per frame
    void Update()  {
        /*
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startPos.position, endPos.position, fracJourney);
        if (fracJourney >= 1f && currentStartPoint + 1 < coordinates.Length)
        {
            currentStartPoint++;
            SetPoints();
        }*/
    }
}
