using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class NewPlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float SpeedMultiplier = 20f;
    private float OldSpeed;
    
    public float RotationSpeed = 10;
    public Rigidbody spaceShipRb;
    public JoystickControll JoystickControll;
    
    
    public GameObject SpeedTrigger;
    public GameObject SpeedTriggerStartPoint;
    private XRGrabInteractable speedTriggerInteract; 

    public RotationButton RotationButtonLeft;
    public RotationButton RotationButtonRight;

    // Start is called before the first frame update
    void Start()
    {
        OldSpeed = 0;
        speedTriggerInteract = SpeedTrigger.GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.Log("rb is null");
    }


    void Update()
    {
        MoveSpaceship();
        RotateSpaceship();
        UiManager.Instance.updateSpeedText(Mathf.FloorToInt(GetSpeed()));
    }

    private void RotateSpaceship()
    {
        if (RotationButtonLeft.ButtonIsPressed && !RotationButtonRight.ButtonIsPressed)
            transform.Rotate((new Vector3(0, -RotationSpeed, 0)) * Time.deltaTime);
        else if (!RotationButtonLeft.ButtonIsPressed && RotationButtonRight.ButtonIsPressed)
            transform.Rotate((new Vector3(0, RotationSpeed, 0)) * Time.deltaTime);
    }


    private void MoveSpaceship()
    {
        float speed = GetSpeed();
        Vector3 direction = new Vector3(JoystickControll.sideToSideTilt * -1, spaceShipRb.velocity.y, JoystickControll.forwardBackwardTilt);
        direction = Vector3.Normalize(direction);
        Vector3 newMovement = direction * speed * Time.deltaTime;
        Debug.Log("new movement is " + newMovement);
        transform.Translate(newMovement * -1);
    }

    private float GetSpeed() //smth between -6 and 6 i guess
    {
        if (!speedTriggerInteract.isSelected)
        {
            float distance =SpeedTrigger.transform.position.z -  SpeedTriggerStartPoint.transform.position.z;

            float newSpeed = distance * SpeedMultiplier; 
            
            
            Debug.Log("new speed is " + newSpeed);
            //double zPos = SpeedTrigger.transform.localPosition.z;
            //zPos = 1 - (zPos / 10);
            //double newSpeed = zPos * SpeedMultiplier;
            OldSpeed = (float) Math.Abs(newSpeed);
            return (float) Math.Abs(newSpeed);
        }
        return OldSpeed;
    }
}
