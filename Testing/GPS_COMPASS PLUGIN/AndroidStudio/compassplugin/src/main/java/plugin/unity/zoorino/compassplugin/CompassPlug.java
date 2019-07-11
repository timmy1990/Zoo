package plugin.unity.zoorino.compassplugin;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.util.Log;

import static android.content.Context.SENSOR_SERVICE;

public class CompassPlug implements SensorEventListener {

    private static final float ninetyDeg = 90;
    private SensorManager sensorManager;
    private Sensor compass;
    private double currentDegree = 0;

    public CompassPlug() {}

    // Method to activate the compass sensor by passing
    // -> arguments: context of active activity
    // -> return: String with report of activation status
    // Might be a solution as well
    // https://stackoverflow.com/questions/10192057/android-getorientation-method-returns-bad-results
    public String activateCompass(Context unityContext){
        // Initialisation of the compass
        sensorManager = (SensorManager) unityContext.getSystemService(SENSOR_SERVICE);
        compass = sensorManager.getDefaultSensor(Sensor.TYPE_ORIENTATION);
        if (compass != null) {
            sensorManager.registerListener(this, compass, SensorManager.SENSOR_DELAY_GAME);
        } else {
            return "ZOORINO COMPASS ACTIVATION FAILED - sensorManager == null";
        }
        return "ZOORINO COMPASS ACTIVATION SUCCESS";
    }

    // Interface method for actions when Sensor is changed
    @Override
    public void onSensorChanged(SensorEvent event) {
        Log.i("ZOORINO: COMPASS: ","Drehung = " + event.values[0]);
        //double degree = Math.round(event.values[0]) + ninetyDeg; // app only landscape mode, therefore 90 Deg rotation
        //Log.i("COMPASS","degree: " + degree + ", event.values: " + event.values[0]);
        // create a rotation animation (reverse turn degree degrees)
        currentDegree = Math.round(event.values[0]);

        //Log.i("ZOORINO: COMPASS","Rotation = " + currentDegree);
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) { }

    // Method for deactivating the compass if it is not used anymore or application finishes
    public String deactivateCompass(){
        sensorManager.unregisterListener(this, compass);
        return "ZOORINO: COMPASS DEACTIVATED";
    }

    // Method for returning the current Degree of the compass.
    public double getDegree(){
        return currentDegree;
    }

}
