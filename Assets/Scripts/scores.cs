using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class scores : MonoBehaviour
{
    public static int scoreValue = 100;
    Text score;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("addScores", 3.0f, 10.0f);
        score = GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "" + scoreValue;
    }
    
    void addScores()
    {
        scoreValue = scoreValue + 5;
    }
}
