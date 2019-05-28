package plugin.unity.zoorino.gpsplugin;

import android.Manifest;
import android.content.Context;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.util.Log;
import static android.content.Context.LOCATION_SERVICE;

public class gpsPlug {

    //public String s1 = "nice";
    //public double d1 = 49.94;

    public LocationManager locationManager;
    public LocationListener locationListener;
    public double locationLat;
    public double locationLon;
    public Context unityContext;

    public gpsPlug() {}

    // public String returnString() { return s1; }
    // public double giveAttributeDouble() { return d1; }

    // public String retStrFrContext(Context unityContext) {
    //    return unityContext.getPackageName();
    //}

    // Activation of GPS Unit with passed Context from Unity
    public String prepareGPS(Context unityContext){
        this.unityContext = unityContext;
        locationManager = (LocationManager) unityContext.getSystemService(LOCATION_SERVICE);
        locationListener = new LocationListener() {
            @Override
            public void onLocationChanged(Location location) {
                // Actions when location changed
                locationLon = location.getLongitude();
                locationLat = location.getLatitude();
                Log.i("ZOORINO PLUGIN: ", "Lon: " + locationLon + ", Lat: " + locationLat);
            }

            @Override
            public void onStatusChanged(String provider, int status, Bundle extras) { }

            @Override
            public void onProviderEnabled(String provider) { }

            @Override
            public void onProviderDisabled(String provider) { }
        };

        if (unityContext.checkSelfPermission(Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && unityContext.checkSelfPermission(Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {

            try {
                locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 1000, 1, locationListener);
            } catch (Exception e){
                Log.e("ZOORINO GPSAKTIVIERUNGSFEHLER", "" + e);
            }
        }
        return "end of prepareGPS";
    }

    // Stop GPS Updates, necessary when quitting the app
    public String stopGPS(){
        if (unityContext == null) {
            return "Erst GPS aktivieren";
        } else {
            locationManager.removeUpdates(locationListener);
            return "GPS wurde deaktiviert";
        }
    }

    // Returns Lat/Lon when called
    public double[] giveGPSData() {
        double[] gps = {locationLat, locationLon};
        return gps;
    }

    public void deavtivateGPS(){

    }

}
