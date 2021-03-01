// ----------------------------------------------------------------------------
// UnlockableDoor.cs
//
// Author: Arturo Serrano
// Date: 27/02/21
// Copyright: © Arturo Serrano
//
// Brief: Handles the opening/closing of doors depending if the player crosses its trigger area
// ----------------------------------------------------------------------------
using UnityEngine;
using DG.Tweening;

public class AutomaticDoor : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public BoxCollider triggerArea;
    public AudioSource doorsAudioSource;
    public float moveAmount;
    public float lerpTime;

    Tween doorTween;
    bool isOpen;
    bool isTweening;
    bool mustRedoTween;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SwitchDoor();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SwitchDoor();

            if (isTweening && isOpen)
                mustRedoTween = true;
        }
    }

    protected void SwitchDoor()
    {
        if (isTweening)
            return;
        isTweening = true;

        doorTween = leftDoor.transform.DOLocalMoveX(leftDoor.transform.localPosition.x + (isOpen ? moveAmount : -moveAmount), lerpTime).SetEase(Ease.InOutSine);
        rightDoor.transform.DOLocalMoveX(rightDoor.transform.localPosition.x + (isOpen ? -moveAmount : moveAmount), lerpTime).SetEase(Ease.InOutSine).OnComplete(() => 
        {
            isTweening = false;

            if (mustRedoTween)
            {
                mustRedoTween = false;
                SwitchDoor();
            }
        });

        doorsAudioSource.Play();
        isOpen = !isOpen;
    }
}
