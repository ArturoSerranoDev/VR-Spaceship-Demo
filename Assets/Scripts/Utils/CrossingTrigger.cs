using UnityEngine;
using UnityEngine.Events;

public class CrossingTrigger : MonoBehaviour
{
    public UnityEvent OnPlayerCrossed = new UnityEvent();

    private void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnPlayerCrossed?.Invoke();

            // Disable this component for the time being not to trigger it multiple times
            this.enabled = false;
        }
    }
}
