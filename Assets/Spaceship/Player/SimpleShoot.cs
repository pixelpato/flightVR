using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    public float shootTime = 1;
    public float timer = 0;
    
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    [Header("Location Refrences")]
    [SerializeField] private Transform barrelLocationLeft;
    [SerializeField] private Transform barrelLocationRight;
    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;
    
    UnityEngine.XR.InputDevice leftDevice;
    UnityEngine.XR.InputDevice rightDevice;

    private bool triggerValue;
    private bool secondTriggerValue;
    private bool primaryValue;
    private bool secondaryValue;
    
    
    void Start()
    {
        if (barrelLocationLeft == null)
            barrelLocationLeft = transform;

        InitController();
    }

    private void InitController()
    {
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        if(leftHandDevices.Count == 1)
        {
            leftDevice = leftHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", leftDevice.name, leftDevice.role.ToString()));
        }
        else if(leftHandDevices.Count > 1)
            Debug.Log("Found more than one left hand!");

        
        
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if(rightHandDevices.Count == 1)
        {
            rightDevice = rightHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", rightDevice.name, rightDevice.role.ToString()));
        }
        else if(rightHandDevices.Count > 1)
            Debug.Log("Found more than one left hand!");
    }

    void Update()
    {

        timer += Time.deltaTime;

        if (timer > shootTime)
        {
            
            if (leftDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue ||
                rightDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out secondTriggerValue) && secondTriggerValue)
            {
                Debug.Log("Trigger button is pressed.");
                Shoot();
                timer = 0;
            }
            
            if (rightDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryValue) && primaryValue &&
                rightDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryValue) && secondaryValue)
            {
                Scene scene = SceneManager. GetActiveScene();
                SceneManager. LoadScene(scene.name);
            }
            
        }
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            
            if(triggerValue)
                tempFlash = Instantiate(muzzleFlashPrefab, barrelLocationLeft.position, barrelLocationLeft.rotation);
            else 
                tempFlash = Instantiate(muzzleFlashPrefab, barrelLocationRight.position, barrelLocationRight.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel

        if (triggerValue)
        {
            GameObject bullet = Instantiate(bulletPrefab, barrelLocationLeft.position, barrelLocationLeft.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(barrelLocationLeft.forward * shotPower);
            Destroy(bullet, 5 );
            Debug.Log("nwe bullet");

        }else if (secondTriggerValue)
        {
            GameObject bullet = Instantiate(bulletPrefab, barrelLocationRight.position, barrelLocationRight.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(barrelLocationRight.forward * shotPower);
            Destroy(bullet, 5);
            Debug.Log("nwe bullet");

        }
    }
}
