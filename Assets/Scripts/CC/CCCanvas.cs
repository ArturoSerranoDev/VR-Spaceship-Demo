using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CCCanvas : MonoBehaviour
{
    public TextMeshProUGUI CCText;
    public GameObject player;

    void FixedUpdate()
    {
        float smoothFactor = 5.0f; //used to sharpen or dull the effect of lerp
        var newPosition = player.transform.position + new Vector3(0, -0.5f, 3);
        var t = gameObject.transform;
        t.position = Vector3.Lerp(t.position,
                                   newPosition,
                                   Time.deltaTime * smoothFactor);
    }
}
