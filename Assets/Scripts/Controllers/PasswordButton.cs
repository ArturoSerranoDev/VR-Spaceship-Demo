// ----------------------------------------------------------------------------
// PasswordButton.cs
//
// Author: Arturo Serrano
// Date: 27/02/21
// Copyright: © Arturo Serrano
//
// Brief: Sends events of button pressed by player on Dial
// ----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ButtonInteractableUnityEvent : UnityEvent<int> { }

public class PasswordButton : MonoBehaviour
{
    public ButtonInteractableUnityEvent OnButtonPressed = new ButtonInteractableUnityEvent();
    public int number;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            OnButtonPressed?.Invoke(number);
            transform.DOLocalMoveZ(transform.localPosition.z - 0.01f, 0.1f).SetLoops(2, LoopType.Yoyo);
        }
    }
}
