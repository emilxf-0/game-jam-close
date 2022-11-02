using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    void Start()
    {
        panel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        panel.SetActive(true);
    }
}
