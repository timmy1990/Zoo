using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class path : MonoBehaviour
{
    public PathCreator pathCreator;
        public float speed;
    public float distanceTravelled;


    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
    }
}
