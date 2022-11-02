using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{ 
    [SerializeField] private TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score " + 0;
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score " + playerScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
