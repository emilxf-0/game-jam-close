using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private PlayerController playerScript;
    [SerializeField] private Enemy enemyScript;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene"|| SceneManager.GetActiveScene().name == "Enemy")
        {
            playerScript.enabled = false;
            enemyScript.enabled = false;

            Invoke(nameof(GameStart), 8);
        }

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
