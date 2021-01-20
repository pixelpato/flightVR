using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandButton : XRBaseInteractable
{
    
    public UnityEvent OnPress = null;
    private bool previousPress = false;
    
    private float yMin = 0.0f ;
    private float yMax = 0.0f ;
    
    
    private float previousHandHeight = 0.0f;
    private XRBaseInteractor hoverInteractor = null;
    protected override void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
    }

    protected override void OnDestroy()
    {
        onHoverEntered.RemoveListener((StartPress));
        onHoverExited.RemoveListener(EndPress);
    }


    private void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);

    }

    private void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousHandHeight = 0;
        previousPress = false;
        SetYPosition(yMax);

    }


    private void Start()
    {
        SetMinMax();
    }

    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        yMin = transform.localPosition.y - (collider.bounds.size.y * 0.5f);
        yMax = transform.localPosition.y;

    }


    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (hoverInteractor)
        {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;



            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);
            CheckPress();
        }
    }


    private float GetLocalYPosition(Vector3 pos)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(pos);
        return localPosition.y;
    }
    private void SetYPosition(float pos)
    {
        Vector3 newPos = transform.localPosition;
        newPos.y = Mathf.Clamp(pos, yMin, yMax);
        transform.localPosition = newPos;
    }

    private void CheckPress()
    {
        bool inPosition = InPosition();


        if (inPosition && inPosition != previousPress)
            OnPress.Invoke();


        previousPress = inPosition;
    }
    private bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);
        return transform.localPosition.y == inRange;
    }


}
