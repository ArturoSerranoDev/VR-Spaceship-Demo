// ----------------------------------------------------------------------------
// TutorialController.cs
//
// Author: Arturo Serrano
// Date: 27/02/21
// Copyright: © Arturo Serrano
//
// Brief: Sets the audio and CC depending on the step of the tutorial de player is in
// ----------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TutorialController : MonoBehaviour
{
    public PasswordController passwordController;
    public GameObject player;
    public CanvasGroup tutorialCanvasGroup;
    int tutorialStep = 0;

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

                yield return new WaitForSeconds(2);
                tutorialCanvasGroup.DOFade(1f, 0.25f);

                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0f);
                CCManager.Instance.DisplaySubtitle(tutorialClip1, index: 0);

                yield return new WaitForSeconds(4);
                CCManager.Instance.DisplaySubtitle(tutorialClip1, index: 1);

                yield return new WaitForSeconds(4);
                CCManager.Instance.DisplaySubtitle(tutorialClip1, index: 2);

                yield return new WaitForSeconds(5);
                SFXPlayer.Instance.PlaySFX(tutorialClip1_2, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0f);
                CCManager.Instance.DisplaySubtitle(tutorialClip1_2, index: 0);

                yield return new WaitForSeconds(3);
                tutorialCanvasGroup.DOFade(0f,0.25f);
                break;
            // Cross first trigger wall
            case 2:
                tutorialCanvasGroup.DOFade(1f, 0.25f);

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
                yield return new WaitForSeconds(2);
                tutorialCanvasGroup.DOFade(0f, 0.25f);
                break;
            // Second Trigger Wall
            case 5:
                tutorialCanvasGroup.DOFade(1f, 0.25f);
                SFXPlayer.Instance.PlaySFX(tutorialClip5, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                CCManager.Instance.DisplaySubtitle(tutorialClip5, index: 0);

                break;
            // Roll Wheel
            case 6:
                SFXPlayer.Instance.PlaySFX(tutorialClip6, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                CCManager.Instance.DisplaySubtitle(tutorialClip6, index: 0);

                break;
            // Yaw Turning
            case 7:
                SFXPlayer.Instance.PlaySFX(tutorialClip7, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                CCManager.Instance.DisplaySubtitle(tutorialClip7, index: 0);

                break;
            // Move Speedometer
            case 8:
                SFXPlayer.Instance.PlaySFX(tutorialClip8, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                CCManager.Instance.DisplaySubtitle(tutorialClip8, index: 0);

                break;
            // End
            case 9:
                SFXPlayer.Instance.PlaySFX(tutorialClip9, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = 1f,
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f);
                CCManager.Instance.DisplaySubtitle(tutorialClip9, index: 0);
                yield return new WaitForSeconds(8);
                tutorialCanvasGroup.DOFade(0f, 0.25f);
                break;
        }

        yield return new WaitForEndOfFrame();

    }

    #region Callbacks

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

    public void OnPitchOverThreshold()
    {
        if (tutorialStep == 5)
        {
            OnTutorialNextStep();
        }
    }

    public void OnRollOverThreshold()
    {
        if(tutorialStep == 6)
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

    public void OnSpeedDialChange()
    {
        if (tutorialStep == 8)
        {
            OnTutorialNextStep();
        }
    }

    #endregion
}
