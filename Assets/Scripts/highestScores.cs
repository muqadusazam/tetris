using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highestScores : MonoBehaviour
{
    public static int highestScoreValue = 0; // declaring and initialising heighest score from 0
    Text highScore; // declaring high score text object

    // Start is called before the first frame update
    void Start()
    {
        highScore = GetComponent<Text>(); // initialising heigh score text
    }

    // Update is called once per frame
    void Update()
    {
        if (scores.scoreValue > highestScoreValue) // check if the score value is above highscore
        {
            highestScoreValue = scores.scoreValue; // then update the highscore to current score value
            highScore.text = "" + highestScoreValue; // update it on the screen
        }
    }
}
