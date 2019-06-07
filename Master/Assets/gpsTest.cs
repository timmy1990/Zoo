using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gpsTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        double lat = 49;
        double lon = 11;
        double alt = 0;

        CoordinateUtilities kompass_test = new CoordinateUtilities(lat, lon, alt);
        double[] enu = kompass_test.geo_to_enu(lat, lon, alt);
        print("enu = " + enu[0] + "," + enu[1]);
    }

}
