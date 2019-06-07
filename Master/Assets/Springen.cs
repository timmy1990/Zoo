using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springen : MonoBehaviour {

    public float speed = 10f;
    public float jumpspeed = 10f;
    public float disToGround = 0.5f;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log(isGrounded());

        if (Input.GetKey(KeyCode.Space)/*&&isGrounded()*/)
        {
            Vector3 jumpVelocity = new Vector3(0f, jumpspeed, 0f);
            rb.velocity = rb.velocity + jumpVelocity;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
        rb.MovePosition(transform.position + movement);

	}
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, disToGround);
    }
}
