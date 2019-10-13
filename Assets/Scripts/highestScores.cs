using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highestScores : MonoBehaviour
{
    public static int highestScoreValue = 0;
    Text highScore;
    // Start is called before the first frame update
    void Start()
    {
        highScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scores.scoreValue > highestScoreValue)
        {
            highestScoreValue = scores.scoreValue;
            highScore.text = "" + highestScoreValue;
        }
    }
}
