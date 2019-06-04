using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyroTest : MonoBehaviour
{
    GameObject gyroView;
    // Start is called before the first frame update
    void Start()
    {
        gyroView = new GameObject("gyroTest");
        gyroView.transform.position = this.transform.position;
        this.transform.parent = gyroView.transform;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        gyroView.transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
        this.transform.Rotate(-Input.gyro.rotationRateUnbiased.x, 0, 0);
    }
}
