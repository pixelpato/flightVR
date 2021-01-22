using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class NewPlayerController : MonoBehaviour
{

    public float Speed = 50f;
    
    
    public Rigidbody spaceShipRb;
    
    public Rigidbody JoystickRB;
    public JoystickControll JoystickControll;
    private CharacterJoint characterJoint;
    
    
    
    
    
    public GameObject SpeedTrigger;
    private ConfigurableJoint ConfigurableJoint; //Speed triggers joint for the data
    public RotationButton RotationButton;
    
    // Start is called before the first frame update
    void Start()
    {
        characterJoint = JoystickRB.gameObject.GetComponent<CharacterJoint>();
        ConfigurableJoint = SpeedTrigger.GetComponent<ConfigurableJoint>();
        
        
        if(ConfigurableJoint == null)
            Debug.Log("Character joint is null");
    }



    
    
    void FixedUpdate()
    {
        Vector3 direction = new Vector3(JoystickControll.sideToSideTilt *-1, spaceShipRb.velocity.y,
            JoystickControll.forwardBackwardTilt);

        direction = Vector3.Normalize(direction);



        Vector3 newMovement = direction * Speed * Time.deltaTime;




         transform.Translate(newMovement *-1);
    }
    
    
    
    // Update is called once per frame
    void Update()
    {


        //Debug.Log("Jostick tilt is * " + JoystickControll.sideToSideTilt + " and " +JoystickControll.forwardBackwardTilt);
        //Debug.Log("CharacterJoint: " + ConfigurableJoint.projectionDistance);
        //Debug.Log("RotationButton: " + RotationButton.ButtonIsPressed);
        
        //rb.velocity = new Vector3(JoystickControll.sideToSideTilt, rb.velocity.y, JoystickControll.forwardBackwardTilt) * Speed;




    }
}
