// ----------------------------------------------------------------------------
// CrossingTrigger.cs
//
// Author: Arturo Serrano
// Date: 30/02/21
// Copyright: © Arturo Serrano
//
// Brief: Used to trigger events after player crosses it
// ----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;

public class CrossingTrigger : MonoBehaviour
{
    public UnityEvent OnPlayerCrossed = new UnityEvent();

    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnPlayerCrossed?.Invoke();

            // Disable this component for the time being not to trigger it multiple times
            this.gameObject.SetActive(false);
        }
    }
}
