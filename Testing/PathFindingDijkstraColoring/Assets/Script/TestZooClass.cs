﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZooClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigi = GetComponent<Rigidbody>();

        ZooPathCreator zpc = new ZooPathCreator();
        zpc.createPathFromCurrentPositionToFirstNode(rigi.transform.position);
        zpc.scanScene();
        zpc.getShortestPath("B");
        zpc.markShortestPathInScene();
    }

}
