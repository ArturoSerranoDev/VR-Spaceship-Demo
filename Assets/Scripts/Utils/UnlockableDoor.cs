// ----------------------------------------------------------------------------
// UnlockableDoor.cs
//
// Author: Arturo Serrano
// Date: 27/02/21
// Copyright: © Arturo Serrano
//
// Brief: Child of Automatic door, this door only works if it is unlocked by callback
// ----------------------------------------------------------------------------
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
