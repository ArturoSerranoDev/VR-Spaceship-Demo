// ----------------------------------------------------------------------------
// WheelDataDriver.cs
//
// Author: Arturo Serrano
// Date: 30/01/21
// Copyright: © Arturo Serrano
//
// Brief: Gets transform values from WheelInteractable and converts them to useful input
// of roll,pitch,yaw and speed data 
// ----------------------------------------------------------------------------
using UnityEngine;

public class WheelDataDriver : MonoBehaviour
{
    public WheelInteractable wheelInteractable;
    public DialInteractable speedDialInteractable;

    [Header("Rot Values")]
    public float rollAmount = 0f;
    public float pitchAmount = 0f;
    public float yawAmount = 0f;

    [Header("Speed")]
    public float speedAmount = 0f;
    int maxsteps;

    float maxRotationalValue;
    Vector3 startingWheelUp;
    Vector3 startingWheelRight;

    void Start()
    {
        startingWheelUp = wheelInteractable.sphereAxisGO.transform.up;
        startingWheelRight = wheelInteractable.sphereAxisGO.transform.right;
        this.maxRotationalValue = wheelInteractable.maxRotationalValue;

        speedDialInteractable.OnDialStepChanged.AddListener(OnDialStepChanged);
        maxsteps = speedDialInteractable.Steps;
    }

    // TODO: Optimize
    void Update()
    {
        if (!wheelInteractable.grabbed)
            return;

        // Get rotationalValues
        rollAmount = Vector3.SignedAngle(wheelInteractable.sphereAxisGO.transform.right, startingWheelRight, startingWheelUp);
        pitchAmount = Vector3.SignedAngle(wheelInteractable.sphereAxisGO.transform.up, startingWheelUp, startingWheelRight);
        yawAmount = wheelInteractable.yawRotationRefPoint.transform.eulerAngles.y - wheelInteractable.wheelHandleGO.transform.eulerAngles.y;

        // Update values of wheel rot from minStep - MaxStep to [-1,1]
        rollAmount = Mathf.Lerp(-1f, 1f, (rollAmount + maxRotationalValue) / (maxRotationalValue + maxRotationalValue));
        pitchAmount = Mathf.Lerp(-1f, 1f, (pitchAmount + maxRotationalValue) / (maxRotationalValue + maxRotationalValue));
        yawAmount = Mathf.Lerp(-1f, 1f, (yawAmount + maxRotationalValue) / (maxRotationalValue + maxRotationalValue));

    }

    public void OnDialStepChanged(int step)
    {
        // Update value of dial from minStep - MaxStep to [0,1]
        speedAmount = (float)step / maxsteps;
    }
}
