using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : AutomaticDoor
{
    public bool unlocked;

    public void OnUnlocked()
    {
        unlocked = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (unlocked)
        {
            base.OnTriggerEnter(other);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (unlocked)
        {
            base.OnTriggerExit(other);
        }
    }
}
