using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandBehaviour : MonoBehaviour
{
    private InputDevice targetDevice;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerDeviceCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerDeviceCharacteristics,devices);



        foreach (var item in devices)
        {
            Debug.Log("item is " + item.name);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue)&& primaryButtonValue);
            Debug.Log("primary button");
        
        
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue >0.1f);
            Debug.Log("trigger button with value " + triggerValue);
        
        if(targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2dAxisValue)&& primary2dAxisValue != Vector2.zero);
            Debug.Log("Primary Touchpad" + primary2dAxisValue);
    }
}
