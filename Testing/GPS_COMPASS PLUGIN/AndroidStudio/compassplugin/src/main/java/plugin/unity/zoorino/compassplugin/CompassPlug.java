package plugin.unity.zoorino.compassplugin;

import android.app.Activity;
import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;

import static android.content.Context.SENSOR_SERVICE;

public class CompassPlug implements SensorEventListener {

    private static final float ninetyDeg = 90;
    private SensorManager sensorManager;
    private Sensor compass;
    private double currentDegree = 0;

    public CompassPlug(){}

    public String activateCompass(Context unityContext){
        // Initialisation of the compass
        sensorManager = (SensorManager) unityContext.getSystemService(SENSOR_SERVICE);
        compass = sensorManager.getDefaultSensor(Sensor.TYPE_ORIENTATION);
        if (compass != null) {
            sensorManager.registerListener(this, compass, SensorManager.SENSOR_DELAY_GAME);
        } else {
            return "sensorManager = null";
        }
        return "GPS Aktivierung Erfolg";
    }

    // Interface method for actions when Sensor is changed
    @Override
    public void onSensorChanged(SensorEvent event) {
        //double degree = Math.round(event.values[0]) + ninetyDeg; // app only landscape mode, therefore 90 Deg rotation
        //Log.i("COMPASS","degree: " + degree + ", event.values: " + event.values[0]);
        // create a rotation animation (reverse turn degree degrees)
        currentDegree = Math.round(event.values[0]);
        //Log.i("COMPASS","Rotation = " + currentDegree + ", -degree = " + -degree);
        //float rotationDiff = posCalc.rotateDiff(-degree);
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) { }

    public double getDegree(){
        return currentDegree;
    }
}
