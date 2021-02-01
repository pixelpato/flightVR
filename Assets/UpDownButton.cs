using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UpDownButton : MonoBehaviour
{
    public Material defaultMaterial;
    public Material pressedMaterial;
    
    private MeshRenderer _meshRenderer = null;
    private XRGrabInteractable _interactable;

    [SerializeField]
    public bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _interactable = GetComponent<XRGrabInteractable>();
    }
    
    public void TriggerButton()
    {
        isPressed = !isPressed;
        
        if(isPressed)
            _meshRenderer.material = pressedMaterial;
        else
            _meshRenderer.material = defaultMaterial;

    }
}
