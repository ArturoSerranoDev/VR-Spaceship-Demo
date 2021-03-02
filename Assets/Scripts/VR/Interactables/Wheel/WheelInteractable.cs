// ----------------------------------------------------------------------------
// WheelInteractable.cs
//
// Author: Arturo Serrano
// Date: 18/01/21
// Copyright: © Arturo Serrano
//
// Brief: Interactable that handles the rotation of the wheel depending on position of hands
// ----------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelInteractable : MonoBehaviour
{
    public List<WheelHandHold> wheelHandHolds = new List<WheelHandHold>();
    public List<XRBaseInteractor> controllerInteractors = new List<XRBaseInteractor>();
    public List<XRBaseInteractor> controllersActivated = new List<XRBaseInteractor>();

    public GameObject sphereAxisGO;
    public GameObject wheelHandleGO;
    public GameObject wheelforwardRefGO;

    // The middle point of the real controllers that the wheel must lerp to
    public GameObject middlePointFollowUpGO;
    // Transform similar to wheelHandle but without a rotation in it, so it can serve as limit for the yaw rot
    public GameObject yawRotationRefPoint; 

    // Maximun rotation over its center that the sphere can rotate
    public float maxRotationalValue = 30f;

    public float dampingSpeed = 30f;
    public float returnToCenterAnimTime = 1f;
    public float yawTurnThreshold = 0.1f;
    public int returnToCenterMaxAngle = 5;

    public bool grabbed;
    public bool yawTurningActivated;
    public bool tweening;

    Vector3 startingDirection;
    Vector3 controllersMiddlePoint;
    Vector3 handlesDirAtStart;
    Coroutine returnCenterCoroutine;

    Vector3 lastValidPos;

    void Start()
    {
        // Add event listeners
        for (int i = 0; i < wheelHandHolds.Count; i++)
        {
            WheelHandHold handHold = wheelHandHolds[i];
            handHold.OnHandHoldGrabbed.AddListener(OnHandHoldGrabbed);
            handHold.OnHandHoldDropped.AddListener(OnHandHoldDropped);
            handHold.OnHandHoldActivated.AddListener(OnHandHoldActivated);
            handHold.OnHandHoldDeactivated.AddListener(OnHandHoldDeactivated);
        }

        Vector3 startingMiddlePoint = (wheelHandHolds[0].transform.position + wheelHandHolds[1].transform.position) / 2;
        startingDirection = startingMiddlePoint - sphereAxisGO.transform.position;

        // Get dir vector between wheel
        handlesDirAtStart = wheelHandHolds[1].transform.position - wheelHandHolds[0].transform.position;
        handlesDirAtStart.y = 0;
        handlesDirAtStart = handlesDirAtStart.normalized;
        handlesDirAtStart = Quaternion.AngleAxis(-90, wheelHandleGO.transform.up) * handlesDirAtStart;

        Debug.Log("startingHandleVector " + handlesDirAtStart);
    }

    public void Update()
    {
        if (!grabbed)
            return;

        RollPitchRotation();

        if (yawTurningActivated)
            YawRotation();
    }

    #region Callbacks

    void OnHandHoldGrabbed(XRBaseInteractor interactor)
    {
        controllerInteractors.Add(interactor);

        grabbed = true;
        tweening = false;

        if (returnCenterCoroutine != null)
            StopCoroutine(returnCenterCoroutine);

        // TODO: Refactor
        if (controllerInteractors.Count == 2)
        {
            controllerInteractors = ReorderControllerInteractors();
        }
    }

    void OnHandHoldDropped(XRBaseInteractor interactor)
    {
        controllerInteractors.Remove(interactor);

        if (controllerInteractors.Count == 0)
        {
            grabbed = false;

            // Check if close to center
            // If yes, interpolate to leave it at center
            if (IsCloseToCenter() && !tweening)
                returnCenterCoroutine = StartCoroutine(ReturnToCenter());

            // TODO: Smooth drop rotation with the direction of last movement
        }
    }

    void OnHandHoldActivated(XRBaseInteractor interactor)
    {
        controllersActivated.Add(interactor);

        // TODO: Refactor
        if (controllersActivated.Count == 2)
        {
            yawTurningActivated = true;
        }

    }

    void OnHandHoldDeactivated(XRBaseInteractor interactor)
    {
        // Check if close to center, then interpolate to leave it at center
        if (IsCloseToCenter() && !tweening && controllersActivated.Count == 0)
            returnCenterCoroutine = StartCoroutine(ReturnToCenter());

        controllersActivated.Remove(interactor);
        yawTurningActivated = false;

        // TODO: Smooth drop rotation with the direction of last movement
    }

    #endregion

    #region Wheel Rotation

    public void RollPitchRotation()
    {
        if (controllerInteractors.Count < 1)
            return;

        // Get middle point to push towards depending on the controllers holding it
        controllersMiddlePoint = GetMiddlePointOfWheel();

        // Move sphere comparing the controllersPos to center pos
        Vector3 direction = controllersMiddlePoint - sphereAxisGO.transform.position;
        float angle = Vector3.Angle(direction, startingDirection);

        // Update lerping position if the angle is less than the threshold
        if (angle < maxRotationalValue)
        {
            middlePointFollowUpGO.transform.position = controllersMiddlePoint;

            lastValidPos = controllersMiddlePoint;
        }

        Vector3 dir = lastValidPos - sphereAxisGO.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(dir, wheelforwardRefGO.transform.forward);

        // Update speed of pull depending on number of controllers holding it
        float updatedDampSpeed = controllerInteractors.Count == 2 ? dampingSpeed : dampingSpeed / 2;

        // Lerp
        Quaternion wheelRotation = Quaternion.Slerp(sphereAxisGO.transform.rotation, toRotation, updatedDampSpeed * Time.deltaTime);

        // Apply
        sphereAxisGO.transform.rotation = wheelRotation;

        //Debug.Log("RotateWheel: Angle: " + (int)Vector3.Angle(direction, startingDirection));
    }

    void YawRotation()
    {
        Vector3 controllersDir = controllerInteractors[1].transform.position - controllerInteractors[0].transform.position;
        controllersDir.y = 0;
        controllersDir = controllersDir.normalized;

        // Set the maximum dot product to +-60 degrees of movement
        // alpha = acos(dot/|a| * |b|)
        float dotProduct = Vector3.Dot(handlesDirAtStart, controllersDir);
        int direction = dotProduct > 0f ? 1 : -1;

        float alpha = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
        alpha = direction * (alpha - 90);
        alpha = Mathf.Clamp(alpha, -60f, 60f);

        // local rotation, so rot * Quaternion.
        Quaternion newRot = yawRotationRefPoint.transform.rotation * Quaternion.AngleAxis(alpha * direction, Vector3.up);

        // Lerp it
        Quaternion WheelHandleRot = Quaternion.Slerp(wheelHandleGO.transform.rotation, newRot, dampingSpeed * 5f * Time.deltaTime);

        // Apply
        wheelHandleGO.transform.rotation = WheelHandleRot;
    }

    // Helper method to get the current middle point between controllers
    Vector3 GetMiddlePointOfWheel()
    {
        if (controllerInteractors.Count == 2)
        {
            // Get middle point of both hands
            return (controllerInteractors[0].transform.position + controllerInteractors[1].transform.position) / 2;
        }
        else
        {
            WheelHandHold freeHold = null;
            foreach (WheelHandHold hold in wheelHandHolds)
            {
                if (!controllerInteractors.Contains(hold.interactorHolding))
                    freeHold = hold;
            }

            // Middle point between hand held and the other handhold
            return (controllerInteractors[0].transform.position + freeHold.transform.position) / 2;
        }
    }

    public bool IsCloseToCenter()
    {
        Vector3 direction = controllersMiddlePoint - sphereAxisGO.transform.position;

        return Vector3.Angle(direction, startingDirection) < returnToCenterMaxAngle;
    }

    // Used to make same operations disregarding what controller grabbed first the wheel
    List<XRBaseInteractor> ReorderControllerInteractors()
    {
        List<XRBaseInteractor> reorderedControllers = new List<XRBaseInteractor>();

        foreach (WheelHandHold hold in wheelHandHolds)
        {
            reorderedControllers.Add(hold.interactorHolding);
        }

        return reorderedControllers;
    }

    public IEnumerator ReturnToCenter()
    {
        tweening = true;
        float totalTime = 0f;
        Debug.Log("Returning To Center");
        Quaternion toRotation = Quaternion.LookRotation(startingDirection);
        Quaternion startingRotation = sphereAxisGO.transform.rotation;

        while (totalTime < returnToCenterAnimTime)
        {
            sphereAxisGO.transform.rotation = Quaternion.Slerp(startingRotation, toRotation, (totalTime / returnToCenterAnimTime));
            yield return new WaitForEndOfFrame();
            totalTime += Time.deltaTime;
        }

        tweening = false;
    }
    #endregion
}
