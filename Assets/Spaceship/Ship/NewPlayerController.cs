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
    public float SpeedMultiplier = 350f;
    private float OldSpeed;
    private Vector3 OldMovement = new Vector3(0,0,0);
    private Vector3 MaxSpeed = new Vector3(15, 15, 15);

    public int heightSpeed = 5;
    public JoystickControll JoystickControll;
    
    
    public GameObject SpeedTrigger;
    public GameObject SpeedTriggerStartPoint;
    private XRGrabInteractable speedTriggerInteract; 

    public RotationButton UpButton;
    public RotationButton DownButton;

    // Start is called before the first frame update
    void Start()
    {
        OldSpeed = 0;
        speedTriggerInteract = SpeedTrigger.GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.Log("rb is null");
    }


    void FixedUpdate()
    {
        MoveSpaceship();
        UiManager.Instance.updateSpeedText(Mathf.FloorToInt(GetSpeed()));
    }

    private int  GetHeightSpeed()
    {
        if (UpButton.ButtonIsPressed && !DownButton.ButtonIsPressed)
            return heightSpeed;
        if (!UpButton.ButtonIsPressed && DownButton.ButtonIsPressed)
            return -heightSpeed;

        return 0;
    }


    private void MoveSpaceship()
    {
        float speed = GetSpeed();
        Vector3 direction = new Vector3(JoystickControll.sideToSideTilt , GetHeightSpeed(), JoystickControll.forwardBackwardTilt *-1);
        direction = Vector3.Normalize(direction);
        
        Vector3 newMovement = direction * speed;
        newMovement = MultiplyForNewDirection(newMovement);
        newMovement *= Time.deltaTime;
        Debug.Log("new movement  " + newMovement);
        
        rb.AddForce(direction);
        Debug.Log("current velocity on rb is " + rb.velocity);
    }

    private Vector3 MultiplyForNewDirection(Vector3 newMovement)
    {
        if (OldMovement.x < 0 && newMovement.x > 0 || OldMovement.x > 0 && newMovement.x < 0 ||
            OldMovement.z < 0 && newMovement.z > 0 || OldMovement.z > 0 && newMovement.z < 0)
            newMovement *= 4;
        else
            OldMovement = newMovement;

        return newMovement;
    }

    private float GetSpeed() 
    {
        if (!speedTriggerInteract.isSelected)
        {
            float distance =SpeedTrigger.transform.position.z -  SpeedTriggerStartPoint.transform.position.z;
            float currentSpeedMultiplier = GetSpeedMultiplier();
            float newSpeed = distance * currentSpeedMultiplier; 
            
            Debug.Log("new speed is " + newSpeed);
            OldSpeed = Math.Abs(newSpeed);
            
            
            return Math.Abs(newSpeed);
        }
        return OldSpeed;
    }


    private float GetSpeedMultiplier()
    {
        if (rb.velocity.x < MaxSpeed.x || rb.velocity.z < MaxSpeed.z)
        {
            return SpeedMultiplier + 150;
        }
        return SpeedMultiplier;
    }
}
