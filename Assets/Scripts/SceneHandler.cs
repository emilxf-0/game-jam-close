using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private PlayerController playerScript;
    [SerializeField] private Enemy enemyScript;
    [SerializeField] private Throw throwScript;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            playerScript.enabled = false;
            enemyScript.enabled = false;
            throwScript.enabled = false;
            Invoke(nameof(GameStart), 5);
        }

    }
    private void GameStart()
    {
        playerScript.enabled = true;
        enemyScript.enabled = true;
        throwScript.enabled = true;
    }
    public void GameOver()
    {
        playerScript.enabled = false;
        enemyScript.enabled = false;
        throwScript.enabled = false;
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