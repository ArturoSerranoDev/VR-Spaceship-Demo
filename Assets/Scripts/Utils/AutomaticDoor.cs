using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutomaticDoor : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public BoxCollider triggerArea;
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

        isOpen = !isOpen;
    }
}
