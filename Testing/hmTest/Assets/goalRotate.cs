﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalRotate : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (new Vector3(0,Time.deltaTime*100, 0));
    }
}
