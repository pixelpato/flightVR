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
    public float SpeedMultiplier = 250f;
    private float OldSpeed;

    public int heightSpeed = 5;
    public Rigidbody spaceShipRb;
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
        else if (!UpButton.ButtonIsPressed && DownButton.ButtonIsPressed)
            return -heightSpeed;

        return 0;
    }


    private void MoveSpaceship()
    {
        float speed = GetSpeed();
        Vector3 direction = new Vector3(JoystickControll.sideToSideTilt * -1, GetHeightSpeed(), JoystickControll.forwardBackwardTilt);
        Debug.Log("direction is  " + direction);
        direction = Vector3.Normalize(direction);
        Debug.Log("direction normalized  " + direction);
        Vector3 newMovement = direction * speed * Time.deltaTime *-1;
        Debug.Log("new movement  " + newMovement);

        
        //        transform.Translate(newMovement * -1);


      //  float x = JoystickControll.sideToSideTilt * -1 * speed * Time.deltaTime;
     //   float y = GetHeightSpeed();
       // float z = JoystickControll.forwardBackwardTilt * speed * Time.deltaTime;
        //invert values
        rb.AddForce(direction);
        
        Debug.Log("current velocity on rb is " + rb.velocity);
    }

    private float GetSpeed() //smth between -6 and 6 i guess
    {
        if (!speedTriggerInteract.isSelected)
        {
            float distance =SpeedTrigger.transform.position.z -  SpeedTriggerStartPoint.transform.position.z;

            float newSpeed = distance * SpeedMultiplier; 
            
            
            Debug.Log("new speed is " + newSpeed);
            OldSpeed = (float) Math.Abs(newSpeed);
            return (float) Math.Abs(newSpeed);
        }
        return OldSpeed;
    }
}
