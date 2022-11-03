using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private PlayerController playerScript;
    [SerializeField] private Enemy enemyScript;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Canvas tutorialCanvas;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            tutorialCanvas.enabled = false;
        }


        if (SceneManager.GetActiveScene().name == "SampleScene"|| SceneManager.GetActiveScene().name == "Enemy")
        {
            playerScript.enabled = false;
            enemyScript.enabled = false;

            Invoke(nameof(GameStart), 8);
        }
    }
    public void ShowTutorial()
    {
        tutorialCanvas.enabled = true;
    }
    private void GameStart()
    {
        playerScript.enabled = true;
        enemyScript.enabled = true;
    }
    public void GameOver()
    {
        playerScript.enabled = false;
        enemyScript.enabled = false;
    }

    public void OnButtonPress(string sceneName)
    {
        audioSource.PlayOneShot(audioClip, 1);
        SceneManager.LoadScene(sceneName);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
