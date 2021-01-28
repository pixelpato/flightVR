﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControll : MonoBehaviour
{

    public Transform topOfJoystick;
    public Transform AnchorPoint;

    
    [SerializeField] public float forwardBackwardTilt = 0;
    [SerializeField] public float sideToSideTilt = 0;
    public float maxTilt = 40; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        forwardBackwardTilt = topOfJoystick.rotation.eulerAngles.x;
        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            forwardBackwardTilt = forwardBackwardTilt - 360;
        }
        else if (forwardBackwardTilt > 5 && forwardBackwardTilt < 74)
        {
        }
        
        
        sideToSideTilt = topOfJoystick.rotation.eulerAngles.z;
        if (sideToSideTilt < 355 && sideToSideTilt > 290)
        {
            sideToSideTilt = sideToSideTilt - 360;
        }
        else if (sideToSideTilt > 5 && sideToSideTilt < 74)
        {
        }


        if (forwardBackwardTilt > maxTilt)
            forwardBackwardTilt = maxTilt;
        if (sideToSideTilt > maxTilt)
            sideToSideTilt = maxTilt;
            
            */

        forwardBackwardTilt =(topOfJoystick.position.z - AnchorPoint.position.z)*10;
        sideToSideTilt = (topOfJoystick.position.x - AnchorPoint.position.x) *10;

    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(("PlayerHand")))
            transform.LookAt(other.transform.position,transform.up);
    }
}
