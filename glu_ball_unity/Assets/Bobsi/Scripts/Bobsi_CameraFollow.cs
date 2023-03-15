using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobsi_CameraFollow : MonoBehaviour
{
    //Naming, etc.. the usual.
    //Good that he chooses to use reference a transform immediatly
    public Transform target;

    //Nice that he uses the default offset and caches it. Way more designer/editor friendly
    private Vector3 offset;

    private void Start()
    {
        //No null checks!
        offset = transform.position - target.position;
    }

    private void Update()
    {
        //No null checks again
        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
    }
}
