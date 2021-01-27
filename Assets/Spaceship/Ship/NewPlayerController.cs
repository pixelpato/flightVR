using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class NewPlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float SpeedMultiplier = 20f;
    public float RotationSpeed = 10;
    public Rigidbody spaceShipRb;
    public JoystickControll JoystickControll;
    public GameObject SpeedTrigger; // z pos  for speed -> smth between 7.778907 and  7.978907


    public RotationButton RotationButtonLeft;
    public RotationButton RotationButtonRight;

    // Start is called before the first frame update
    void Start()
    {
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
        double zPos = SpeedTrigger.transform.localPosition.z;
        zPos = 1 - (zPos / 10);
        double newSpeed = zPos * SpeedMultiplier;
        Debug.Log("new speed is " + newSpeed);
        return (float)Math.Abs(newSpeed);
    }



}
