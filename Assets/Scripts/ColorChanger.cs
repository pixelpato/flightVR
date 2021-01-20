using System;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorChanger : MonoBehaviour
{
    public Material selectMaterial = null;
    public Material unSelectedMaterial = null;

    private MeshRenderer meshRenderer = null;
    private XRBaseInteractable interactable = null;

    private bool shouldSetSelectedColor = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        interactable = GetComponent<XRBaseInteractable>();
        interactable.onHoverExited.AddListener(ChangeMaterial);
    }

    private void OnDestroy()
    {
        interactable.onHoverExited.RemoveListener(ChangeMaterial);
    }

    private void ChangeMaterial(XRBaseInteractor interactor)
    {
        if (!shouldSetSelectedColor)
        {
            shouldSetSelectedColor = true;
            meshRenderer.material = unSelectedMaterial;
        }
        else
        {
            shouldSetSelectedColor = false;
            meshRenderer.material = selectMaterial;
        }
    }
}
