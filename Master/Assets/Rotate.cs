using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public KeyCode pressUp;
    public KeyCode pressDown;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pressUp))
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        if (Input.GetKeyDown(pressDown))
            GetComponent<Transform>().eulerAngles = new Vector3(-90, 0, 0);


    }

}
