// ----------------------------------------------------------------------------
// TutorialController.cs
//
// Author: Arturo Serrano
// Date: 27/02/21
// Copyright: © Arturo Serrano
//
// Brief: Sets the data depending on the step of the tutorial de player is in
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public PasswordController passwordController;
    public int tutorialStep = 0;

    // Start is called before the first frame update
    void Start()
    {
        passwordController.OnCorrectPassword.AddListener(OnPasswordCompleted);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPasswordCompleted()
    {
        // After 
    }

    public void ShowTutorialStep()
    {

    }
}
