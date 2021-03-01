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
    public AudioClip tutorialClip2;
    public AudioClip tutorialClip3;
    public AudioClip tutorialClip4;
    public AudioClip tutorialClip5;
    public AudioClip tutorialClip6;
    public AudioClip tutorialClip7;
    public AudioClip tutorialClip8;
    public AudioClip tutorialClip9;
   
    // Start is called before the first frame update
    void Start()
    {
        passwordController.OnCorrectPassword.AddListener(OnTutorialNextStep);

        OnTutorialNextStep();
    }

    public void LoadTutorialStep(int step)
    {
        tutorialStep = step;

        switch (step)
        {
            // Start
            case 1:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = Random.Range(0.8f, 1.2f),
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f,true);
                break;
            case 2:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = Random.Range(0.8f, 1.2f),
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f, true);
                break;
            case 3:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = Random.Range(0.8f, 1.2f),
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f, true);
                break;
            case 4:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = Random.Range(0.8f, 1.2f),
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f, true);
                break;
            case 5:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = Random.Range(0.8f, 1.2f),
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f, true);
                break;
            case 6:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = Random.Range(0.8f, 1.2f),
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f, true);
                break;
            case 7:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = Random.Range(0.8f, 1.2f),
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f, true);
                break;
            case 8:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = Random.Range(0.8f, 1.2f),
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f, true);
                break;
            case 9:
                SFXPlayer.Instance.PlaySFX(tutorialClip1, player.transform.position, new SFXPlayer.PlayParameters()
                {
                    Pitch = Random.Range(0.8f, 1.2f),
                    SourceID = 17624,
                    Volume = 1.0f
                }, 0.2f, true);
                break;
        }
    }

    public void OnTutorialNextStep()
    {
        LoadTutorialStep(tutorialStep + 1);
    }


}
