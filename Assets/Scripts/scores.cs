using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class scores : MonoBehaviour
{
    public static int scoreValue = 0; // scores stated from 0 points
    Text score; // declaring score text object

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("addScores", 3.0f, 10.0f); // calling method starting after 3 seconds and repeating every 10 seconds
        score = GetComponent<Text>(); // initialising the score text
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "" + scoreValue; // showing / updating the scores on the screen
    }
    
    void addScores()
    {
        scoreValue = scoreValue + 5; // this method is called every 10 seconds
    }
}
