using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    public float shootTime = 1;
    public float timer = 0;
    
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    [Header("Location Refrences")]
    [SerializeField] private Transform barrelLocation;
    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;
    
    UnityEngine.XR.InputDevice leftDevice;
    UnityEngine.XR.InputDevice rightDevice;


    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

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
            bool triggerValue;
            bool secondTriggerValue;
            if (leftDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue ||
                rightDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out secondTriggerValue) && secondTriggerValue)
            {
                Debug.Log("Trigger button is pressed.");
                Shoot();
                timer = 0;
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
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        GameObject bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        Destroy(bullet, destroyTimer +8);
        Debug.Log("nwe bullet");

    }
    

}
