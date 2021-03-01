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
    public GameObject player;
    public int tutorialStep = 0;

    [Header("AudioClips")]
    public AudioClip tutorialClip1;
    public AudioClip tutorialClip1_2;
    public AudioClip tutorialClip2;
    public AudioClip tutorialClip3;
    public AudioClip tutorialClip4;
    public AudioClip tutorialClip5;
    public AudioClip tutorialClip6;
    public AudioClip tutorialClip7;
    public AudioClip tutorialClip8;
    public AudioClip tutorialClip9;
   
    void Start()
    {
        passwordController.OnCorrectPassword.AddListener(OnTutorialNextStep);
        passwordController.OnIncorrectPassword.AddListener(OnIncorrectPassword);
        OnTutorialNextStep();
    }

    public IEnumerator LoadTutorialStep(int step)
    {
        tutorialStep = step;

        switch (step)
        {
            // Start
            case 1:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0f);
                CCManager.Instance.DisplaySubtitle(tutorialClip1, index: 0);

                yield return new WaitForSeconds(2);
                CCManager.Instance.DisplaySubtitle(tutorialClip1, index: 1);

                yield return new WaitForSeconds(11);
                SFXPlayer.Instance.PlaySFX(tutorialClip1_2, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0f);
                CCManager.Instance.DisplaySubtitle(tutorialClip1_2, index: 0);

                break;
            // Cross first trigger wall
            case 2:
                SFXPlayer.Instance.PlaySFX(tutorialClip2, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                CCManager.Instance.DisplaySubtitle(tutorialClip2, index: 0);

                yield return new WaitForSeconds(5);
                OnTutorialNextStep();

                break;
            // Input password 
            case 3:
                SFXPlayer.Instance.PlaySFX(tutorialClip3, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                CCManager.Instance.DisplaySubtitle(tutorialClip3, index: 0);

                break;
            // Correct Password
            case 4:
                SFXPlayer.Instance.PlaySFX(tutorialClip4, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);

                CCManager.Instance.DisplaySubtitle(tutorialClip4, index: 0);

                break;
            // Second Trigger Wall
            case 5:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                break;
            case 6:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                break;
            case 7:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                break;
            case 8:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                break;
            case 9:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                break;
        }

        yield return new WaitForEndOfFrame();

    }

    // Reset the same message
    public void OnIncorrectPassword()
    {
        tutorialStep -= 1;
        StartCoroutine(LoadTutorialStep(tutorialStep + 1));
    }

    public void OnTutorialNextStep()
    {
        StartCoroutine(LoadTutorialStep(tutorialStep + 1));
    }

    public void OnRollOverThreshold()
    {
        if(tutorialStep == 5)
        {
            OnTutorialNextStep();
        }
    }
    public void OnPitchOverThreshold()
    {
        if (tutorialStep == 6)
        {
            OnTutorialNextStep();
        }
    }
    public void OnYawOverThreshold()
    {
        if (tutorialStep == 7)
        {
            OnTutorialNextStep();
        }
    }


}
