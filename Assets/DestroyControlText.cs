using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DestroyControlText : MonoBehaviour
{
    
    public XRGrabInteractable joystick;

    void Update()
    {
        if (joystick.isSelected)
            Destroy(transform.parent.gameObject);
    }
}
