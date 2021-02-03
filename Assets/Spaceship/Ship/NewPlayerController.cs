using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class NewPlayerController : MonoBehaviour
{

    public float MaxHP = 100;
    public float CurrentHP = 100;
    public float AsteroidDamage = 10;
    
    
    
    //movement data
    private Rigidbody rb;
    public float SpeedMultiplier = 1;
    private float OldSpeed;
    public float Speed = 350;
    private Vector3 OldMovement = new Vector3(0,0,0);
    private Vector3 NewMovement = new Vector3(0,0,0);
    private Vector3 MaxVelocity = new Vector3(15, 1, 15);
    public int heightSpeed = 10;
    
    //player interaction    
    public JoystickControll JoystickControll;
    
    public GameObject SpeedTrigger;
    public GameObject SpeedTriggerStartPoint;
    private ShipSounds shipSFX;
    private XRGrabInteractable speedTriggerInteract; 

    public UpDownButton UpButton;
    public UpDownButton DownButton;

    [FormerlySerializedAs("TextMeshPro")] public TextMeshProUGUI debugTextmesh;
    
    // Start is called before the first frame update
    void Start()
    {
        OldSpeed = 0;
        speedTriggerInteract = SpeedTrigger.GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.Log("rb is null");
        GameObject shipObj = GameObject.Find("Spaceship");

        try
        {
            shipSFX = shipObj.GetComponent<ShipSounds>();
            shipSFX.volume(0.25f);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    void FixedUpdate()
    {
        SetMovement();
        SetSpeedMultiplier();
        MoveSpaceship();
        UiManager.Instance.updateSpeedText(Mathf.FloorToInt(rb.velocity.magnitude));
        
    }
    private void SetMovement()
    {
        NewMovement = new Vector3(JoystickControll.sideToSideTilt *-1 , GetHeightSpeed(), JoystickControll.forwardBackwardTilt *-1);
        NewMovement = Vector3.Normalize(NewMovement);
    }
    private void SetSpeedMultiplier()
    {
        AddSpeedUpMultiplier();
        MultiplyForNewDirection();
    }
    private void MultiplyForNewDirection()
    {
         if (OldMovement.x < 0 && NewMovement.x > 0 || OldMovement.x > 0 && NewMovement.x < 0 ||
            OldMovement.z < 0 && NewMovement.z > 0 || OldMovement.z > 0 && NewMovement.z < 0)
        {
            if(Math.Abs(rb.velocity.x) > 6 ||Math.Abs(rb.velocity.z) > 6)
                SpeedMultiplier += 12;
            else if(Math.Abs(rb.velocity.x) > 3 ||Math.Abs(rb.velocity.z) > 3)
                SpeedMultiplier += 6;
            else
                SpeedMultiplier += 3;
        }
        else
            OldMovement = NewMovement;
    }
    private void AddSpeedUpMultiplier()
    {
            if (Math.Abs(rb.velocity.x) < 3 || Math.Abs(rb.velocity.z) < 3)
                SpeedMultiplier =5;
            if (Math.Abs(rb.velocity.x) < 5 || Math.Abs(rb.velocity.z) < 5)
                SpeedMultiplier = 3; 
            if (Math.Abs(rb.velocity.x) >10 || Math.Abs(rb.velocity.z) > 10)
                SpeedMultiplier = 1;
    }
    private int  GetHeightSpeed()
    {
        if (!UpButton.isPressed && !DownButton.isPressed)
        {
            if (rb.velocity.y == 0)
                return 0;
            if (rb.velocity.y > 0)
                return -2; 
            return 2;
        }
        if (UpButton.isPressed && !DownButton.isPressed)
            return -heightSpeed;
        if (!UpButton.isPressed && DownButton.isPressed)
            return heightSpeed;

        return 0;
    }
    private void MoveSpaceship()
    {
        float speed = GetSpeed();
        Vector3 updatedMovement = NewMovement * speed * Time.deltaTime;

        if (MaxVelocity.x - Math.Abs(rb.velocity.x) < 0)
            updatedMovement.x = 0;
        if (MaxVelocity.y - Math.Abs(rb.velocity.y) < 0)
            updatedMovement.y = 0;
        if (MaxVelocity.z - Math.Abs(rb.velocity.z) < 0)
            updatedMovement.z = 0;
        
        debugTextmesh.text = "speed is : " + speed +"\n and multiplier is " + SpeedMultiplier + "\n updated movement  " + updatedMovement +"\n velocity is " + rb.velocity;
        if (Math.Abs(rb.velocity.x) < MaxVelocity.x ||Math.Abs(rb.velocity.z) < MaxVelocity.z)
            rb.AddForce(updatedMovement);
    }
    private float GetSpeed() 
    {
        if (!speedTriggerInteract.isSelected)
        {
            float distance =SpeedTrigger.transform.position.z -  SpeedTriggerStartPoint.transform.position.z;
            distance *= 10;
         //   Debug.Log("speed is " + Speed +"distance is " + distance  + "multi is " + SpeedMultiplier);
            
            float newSpeed = Speed * distance * SpeedMultiplier; 
            
            OldSpeed = Math.Abs(newSpeed);
            return OldSpeed;
        }
        return OldSpeed;
    }


  
}
