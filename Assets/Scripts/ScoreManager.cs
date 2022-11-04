using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{ 
    [SerializeField] private TMP_Text scoreText;
    public TMP_Text highscore;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + 0;
        highscore.text = "Highscore " + PlayerPrefs.GetInt("Highscore", 0);
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score " + playerScore;
        
        if (playerScore > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", playerScore);
            highscore.text = "Highscore " + playerScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
