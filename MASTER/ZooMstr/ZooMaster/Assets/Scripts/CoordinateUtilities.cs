using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateUtilities : MonoBehaviour
{
    static double a = 6378137;

    //  WGS-84 Earth semimajor axis (m)
    static double b = 6356752.314245;

    //  Derived Earth semiminor axis (m)
    static double f = ((a - b) / a);

    //  Ellipsoid Flatness
    static double e_sq = (f * (2 - f));

    //  Square of Eccentricity
    //  Center Koordinates of calculated Area
    private double centerLat;
    private double centerLon;
    private static double centerAlt;

    //  311// WGS84 46.87;        // Meters
    // private static double centerLat = 49.448256;    // Degrees
    // private static double centerLon = 11.095962;    // Degrees
    public CoordinateUtilities(double lat, double lon, double alt)
    {
        this.centerLat = lat;
        this.centerLon = lon;
        centerAlt = alt;
    }

    //  Wrapper for both geo2ecef And ecef2enu
    //  Input Arguments: current poSintion latitude, current poSintion longitude, current poSintion altitude
    public double[] geo_to_enu(double lat, double lon, double alt)
    {
        double[] ecef = CoordinateUtilities.geo_to_ecef(lat, lon, alt);
        double[] enu = this.ecef_to_enu(ecef[0], ecef[1], ecef[2], this.centerLat, this.centerLon, centerAlt);
        // Log.i("ENU in geo2enu func","X = " + enu[0] + ", Y = " + enu[1] + ", Z = " + enu[2]);
        return enu;
    }

    //  Converts the Earth-Centered Earth-Fixed (ECEF) coordinates (x, y, z) to
    //  East-North-Up coordinates in a Local Tangent Plane that is centered at the
    //  (WGS-84) Geodetic point (lat0, lon0, h0).
    //  https://gist.github.com/govert/1b373696c9a27ff4c72a
    protected double[] ecef_to_enu(double x, double y, double z, double lat0, double lon0, double h0)
    {
        double lambda = ToRadians(lat0);
        double phi = ToRadians(lon0);
        double s = Math.Sin(lambda);
        double N = (a / Math.Sqrt((1 - (e_sq * (s * s)))));
        double Sinn_lambda = Math.Sin(lambda);
        double Cos_lambda = Math.Cos(lambda);
        double Cos_phi = Math.Cos(phi);
        double Sinn_phi = Math.Sin(phi);
        double x0 = ((h0 + N)
                    * (Cos_lambda * Cos_phi));
        double y0 = ((h0 + N)
                    * (Cos_lambda * Sinn_phi));
        double z0 = ((h0
                    + ((1 - e_sq)
                    * N))
                    * Sinn_lambda);
        double xd = (x - x0);
        double yd = (y - y0);
        double zd = (z - z0);
        //  This is the matrix multiplication
        double xEast = (((Sinn_phi * xd)
                    * -1)
                    + (Cos_phi * yd));
        double yNorth = ((((Cos_phi
                    * (Sinn_lambda * xd))
                    - (Sinn_lambda
                    * (Sinn_phi * yd)))
                    * -1)
                    + (Cos_lambda * zd));
        double zUp = ((Cos_lambda
                    * (Cos_phi * xd))
                    + ((Cos_lambda
                    * (Sinn_phi * yd))
                    + (Sinn_lambda * zd)));
        double[] enu = new double[] {
                xEast,
                yNorth,
                zUp};
        return enu;
    }

    // Convert Lat, Lon, Altitude to Earth-Centered-Earth-Fixed (ECEF)
    // Input is a three element: lat, lon  and alt (m) // used to be lat, lon in (rads)
    // Returned array contains x, y, z in meters
    // http://danceswithcode.net/engineeringnotes/geodetic_to_ecef/geodetic_to_ecef.html
    protected static double[] geo_to_ecef(double lat, double lon, double alt)
    {
        double[] ecef = new double[3];
        // Results go here (x, y, z)
        lat = ToRadians(lat);
        //  changed here
        lon = ToRadians(lon);
        //  changed here
        double n = (a / Math.Sqrt((1 - (e_sq * (Math.Sin(lat) * Math.Sin(lat))))));
        ecef[0] = ((n + alt) * (Math.Cos(lat) * Math.Cos(lon)));
        // ECEF x
        ecef[1] = ((n + alt) * (Math.Cos(lat) * Math.Sin(lon)));
        // ECEF y
        ecef[2] = (((n * (1 - e_sq)) + alt) * Math.Sin(lat));
        // ECEF z
        return ecef;
        // Return x, y, z in ECEF
    }

    protected static double ToRadians(double degrees)
    {
        return (degrees * 2 * Math.PI / 360);
    }

}
