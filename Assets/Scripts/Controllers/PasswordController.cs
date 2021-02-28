// ----------------------------------------------------------------------------
// PasswordController.cs
//
// Author: Arturo Serrano
// Date: 27/02/21
// Copyright: © Arturo Serrano
//
// Brief: Sets the view for the password logic and hear events of password buttons pressed
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class PasswordController : MonoBehaviour
{
    public List<PasswordButton> buttons = new List<PasswordButton>();

    public string password = "885";
    string currentPassword;
    public TextMeshProUGUI displayText;

    public UnityEvent OnCorrectPassword = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        foreach (PasswordButton item in buttons)
        {
            item.OnButtonPressed.AddListener(ProcessButtonPressed);
        }
    }

    void OnDisable()
    {
        foreach (PasswordButton item in buttons)
        {
            item.OnButtonPressed.RemoveListener(ProcessButtonPressed);
        }
    }

    public void ProcessButtonPressed(int number)
    {
        currentPassword += number;

        displayText.text = currentPassword;

        if (currentPassword.Length == password.Length)
        {
            if (password == currentPassword)
            {
                OnCorrectPassword?.Invoke();
            }
            else
            {
                currentPassword = string.Empty;
                displayText.text = string.Empty;
            }
        }
    }
}
