using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float speed = 8.0f;
    public float rotationSpeed = 32.0f;

    [HideInInspector] public Text Monitor;

    public GameObject AsteroidTemp;
    

    private void Awake () {
        Monitor = GameObject.Find("Canvas/Text").GetComponent<Text>();
    }

    void FixedUpdate () {
        // Get all 3 rotation axis for joysticks
        float yaw = Input.GetAxis("Yaw") * rotationSpeed * Time.deltaTime;
        float pitch = Input.GetAxis("Pitch") * rotationSpeed * Time.deltaTime;
        float roll = Input.GetAxis("Roll") * rotationSpeed * Time.deltaTime;
        float accel = Input.GetAxis("Accel") * speed * Time.deltaTime;

        // acceleration
        transform.Translate(0, 0, accel);

        // rotation
        transform.Rotate(-pitch, yaw, -roll);

        // monitor variables
        Monitor.text = "Yaw: " + yaw + "\n" +
                       "Pitch: " + pitch + "\n" +
                       "Roll: " + roll + "\n" +
                       "Speed: " + speed;

        if (Input.GetKeyDown(KeyCode.O))
        {
            Destroy(AsteroidTemp);
        }
    }
}