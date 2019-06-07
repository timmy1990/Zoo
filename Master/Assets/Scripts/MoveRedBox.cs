using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRedBox : MonoBehaviour
{

    private Vector3 startMarker;
    private Vector3 endMarker;

    private float speed = 80.0f; // Movement speed in units/sec.
    private float startTime; // Time when the movement started.
    private float journeyLength;  // Total distance between the markers.
    private bool movementON = false; // Indicates if movement is active
    private int journeyStation = 0;
    private bool onTheWay = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (movementON == true)
        {
            if (onTheWay == false)
            {
                if (journeyStation == 0)
                {
                    startMarker = this.transform.position;
                    endMarker = new Vector3(125f, 15f, -80f); // 1. Destination: Right Lower Corner
                    journeyLength = Vector3.Distance(startMarker, endMarker);  // Calculate the journey length.
                    startTime = Time.time; // set current Time
                    onTheWay = true;
                }
                else if (journeyStation == 1)
                {
                    startMarker = this.transform.position;
                    endMarker = new Vector3(-125f, 15f, -80f); // 2. Destination: Left Lower Corner
                    journeyLength = Vector3.Distance(startMarker, endMarker);  // Calculate the journey length.
                    startTime = Time.time; // set current Time
                    onTheWay = true;
                }
                else if (journeyStation == 2)
                {
                    startMarker = this.transform.position;
                    endMarker = new Vector3(-125f, 15f, 80f); // 3. Destination: Right Upper Corner
                    journeyLength = Vector3.Distance(startMarker, endMarker);  // Calculate the journey length.
                    startTime = Time.time; // set current Time
                    onTheWay = true;
                }
                else if (journeyStation == 3)
                {
                    startMarker = this.transform.position; // 4. Destination: Right Upper Corner
                    endMarker = new Vector3(125f, 15f, 80f);
                    journeyLength = Vector3.Distance(startMarker, endMarker);  // Calculate the journey length.
                    startTime = Time.time; // set current Time
                    onTheWay = true;
                }
            }

            float distCovered = (Time.time - startTime) * speed;
            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;
            //print("fracJourney" + fracJourney);
            // Set our position as a fraction of the distance between the markers.
            this.transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
            if (fracJourney > 1)
            {
                journeyStation = (journeyStation + 1) % 4;
                //print("JourneyStation:" + journeyStation);
                onTheWay = false;
            }
        }
    }

    // Switch for Box-Movement
    public void setMovementON()
    {
        if (movementON == false)
        {
            movementON = true;
        }
        else
        {
            movementON = false;
        }
    }
}