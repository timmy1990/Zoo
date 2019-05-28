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

    public String activateCompass(Context unityContext){
        // Initialisation of the compass
        sensorManager = (SensorManager) unityContext.getSystemService(SENSOR_SERVICE);
        compass = sensorManager.getDefaultSensor(Sensor.TYPE_ORIENTATION);
        if (compass != null) {
            Log.i("ZOORINO: COMPASS: ", "Aktivierung erfolgt");
            sensorManager.registerListener(this, compass, SensorManager.SENSOR_DELAY_GAME);
        } else {
            return "sensorManager = null";
        }
        return "Compass Aktivierung Erfolg";
    }

    // Interface method for actions when Sensor is changed
    @Override
    public void onSensorChanged(SensorEvent event) {
        Log.i("ZOORINO: COMPASS: ","Drehung = " + event.values[0]);
        //double degree = Math.round(event.values[0]) + ninetyDeg; // app only landscape mode, therefore 90 Deg rotation
        //Log.i("COMPASS","degree: " + degree + ", event.values: " + event.values[0]);
        // create a rotation animation (reverse turn degree degrees)
        currentDegree = Math.round(event.values[0]);

        Log.i("ZOORINO: COMPASS","Rotation = " + currentDegree);
        //float rotationDiff = posCalc.rotateDiff(-degree);
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) { }

    public double getDegree(){
        return currentDegree;
    }

}
