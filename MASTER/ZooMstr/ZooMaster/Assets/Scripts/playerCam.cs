
using UnityEngine;

public class playerCam : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Vector2 touchPos;
    private float swipeResistance;

   private void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(0))
        {
            touchPos = Input.mousePosition;

        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(0))
        {
            float swipeForce = touchPos.x - Input.mousePosition.x;
            if (Mathf.Abs(swipeForce) > swipeResistance)
            {
                if (swipeForce < 0)
                    SlideCamera(true);
                else
                    SlideCamera(false);
            }
        }
    }

     void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;


            transform.LookAt(target);
    }
    
    public void SlideCamera (bool left)
    {
        if (left)
            // Rotate vector and slide left --else--swipe right
            offset = Quaternion.Euler(0, 90, 0) * offset;
        else
            offset = Quaternion.Euler(0, -90, 0) * offset;
    }
    


}
