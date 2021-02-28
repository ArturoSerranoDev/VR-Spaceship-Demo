// ----------------------------------------------------------------------------
// WheelHandHold.cs
//
// Author: Arturo Serrano
// Date: 18/01/21
// Copyright: © Arturo Serrano
//
// Brief: BaseInteractable to sends event data to WheelInteractable.cs
// ----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractorUnityEvent : UnityEvent<XRBaseInteractor> { }

public class WheelHandHold : XRBaseInteractable
{
    public XRBaseInteractor interactorHolding;
    public bool grabbed;
    public bool activated;

    public InteractorUnityEvent OnHandHoldGrabbed = new InteractorUnityEvent();
    public InteractorUnityEvent OnHandHoldDropped = new InteractorUnityEvent();
    public InteractorUnityEvent OnHandHoldActivated = new InteractorUnityEvent();
    public InteractorUnityEvent OnHandHoldDeactivated = new InteractorUnityEvent();

    protected override void Awake()
    {
        base.Awake();
        onSelectEntered.AddListener(Grab);
        onSelectExited.AddListener(Drop);
        onActivate.AddListener(Activate);
        onDeactivate.AddListener(Deactivate);
    }

    protected override void OnDestroy()
    {
        base.Awake();
        onSelectEntered.RemoveListener(Grab);
        onSelectExited.RemoveListener(Drop);
    }

    void Grab(XRBaseInteractor interactor)
    {
        grabbed = true;
        interactorHolding = interactor;

        Debug.Log("Grabbed");
        OnHandHoldGrabbed?.Invoke(interactor);
    }

    void Drop(XRBaseInteractor interactor)
    {
        grabbed = false;
        interactorHolding = null;

        Debug.Log("Dropped");
        OnHandHoldDropped?.Invoke(interactor);
    }

    void Activate(XRBaseInteractor interactor)
    {
        activated = true;
        OnHandHoldActivated?.Invoke(interactor);
    }

    void Deactivate(XRBaseInteractor interactor)
    {
        activated = false;
        OnHandHoldDeactivated?.Invoke(interactor);
    }

}
