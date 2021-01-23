using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour
{
    [HideInInspector] public Text Monitor;
    [HideInInspector] public Rigidbody rb;

    public float speed = 8.0f;
    public float maxSpeed = 3;
    public float minSpeed = -3;

    public float rotationSpeed = 32.0f;

    private float accel;
    
    private void Start()
    {
        
    }

    private void Awake()
    {
        Monitor = GameObject.Find("Canvas/Text").GetComponent<Text>();
    }

    void FixedUpdate()
    {
        // Get all 3 rotation axis for joysticks
        float yaw = Input.GetAxis("Yaw") * rotationSpeed * Time.deltaTime;
        float pitch = Input.GetAxis("Pitch") * rotationSpeed * Time.deltaTime;
        float roll = Input.GetAxis("Roll") * rotationSpeed * Time.deltaTime;

        // acceleration
        boost();

        // rotation
        transform.Rotate(-pitch, yaw, -roll);

        // monitor variables
        Monitor.text = "Yaw: " + yaw + "\n" +
                       "Pitch: " + pitch + "\n" +
                       "Roll: " + roll + "\n" +
                       "Speed: " + speed + "\n" +
                       "accel: " + accel;
    }

    public void boost()
    {
        if (Input.GetKey(KeyCode.W) && accel < maxSpeed)
        {
            accel += Input.GetAxis("Accel") * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) && accel > minSpeed)
        {
            accel += Input.GetAxis("Accel") * speed * Time.deltaTime;
        }

        transform.Translate(0, 0, accel);
    }
}
