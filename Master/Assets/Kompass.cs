using UnityEngine;

public class Kompass : MonoBehaviour
{
   void Start()
    {
        Input.location.Start();
    }

    void Update()
    {
        float rotation = Input.compass.trueHeading;
        print("true heading: " + rotation);
    }

}
