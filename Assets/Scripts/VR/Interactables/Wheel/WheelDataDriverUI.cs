// ----------------------------------------------------------------------------
// WheelInteractable.cs
//
// Author: Arturo Serrano
// Date: 18/01/21
// Copyright: © Arturo Serrano
//
// Brief: Interactable that handles the rotation of the wheel depending on position of hands
// ----------------------------------------------------------------------------using System.Collections;
using TMPro;
using UnityEngine;

public class WheelDataDriverUI : MonoBehaviour
{
    public WheelDataDriver dataDriver;

    public TextMeshProUGUI rollText;
    public TextMeshProUGUI pitchText;
    public TextMeshProUGUI yawText;
    public TextMeshProUGUI speedText;

    // Update is called once per frame
    void Update()
    {
        if (!dataDriver.wheelInteractable.grabbed)
            return;

        rollText.text = "Roll: " + dataDriver.rollAmount.ToString("0.0");
        pitchText.text = "Pitch: " + dataDriver.pitchAmount.ToString("0.0");
        yawText.text = "Yaw: " + dataDriver.yawAmount.ToString("0.0");
        speedText.text = "Speed: " + dataDriver.speedAmount.ToString("0.0");
    }
}
