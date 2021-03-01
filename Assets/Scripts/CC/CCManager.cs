using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/// <summary>
/// The CCManager will track each CCSource in the scene and hide/display/align them based on the view direction and
/// distance.
/// </summary>
[DefaultExecutionOrder(-999)]
public class CCManager : MonoBehaviour
{
    static CCManager s_Instance;
    public static CCManager Instance => s_Instance;

    public CCDatabase Database;
    public TextMeshProUGUI displayText;
    List<CCSource> m_Sources = new List<CCSource>();
    Camera m_Camera;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (s_Instance != null)
        {
            Destroy(this);
            return;
        }
        
        s_Instance = this;
    }

    void Start()
    {
        Database.BuildMap();
    }

    void OnDisable()
    {
        
    }

    public void DisplaySubtitle(AudioClip clip, int index)
    {
        // Transition effect between texts
        bool isDelayed = false;
        if (displayText.alpha > 0.1f)
        {
            isDelayed = true;
            displayText.DOFade(0f, 0.5f).OnComplete(() => displayText.text = Database.GetTextEntry(clip, index));
        }
        else
            displayText.text = Database.GetTextEntry(clip, index);

        displayText.DOFade(1f, 0.5f).SetDelay(isDelayed ? 0.5f : 0f);
    }

    public static void RegisterSource(CCSource source)
    {
        s_Instance.m_Sources.Add(source);
    }

    public static void RemoveSource(CCSource source)
    {
        s_Instance.m_Sources.Remove(source);
    }
}
