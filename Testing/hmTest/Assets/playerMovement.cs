using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement: MonoBehaviour
{

    public float speed = 20.0f;

    private Rigidbody rigi;
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xAxis, 0, yAxis);

        rigi.MovePosition(transform.position + movement);
    }
}




        