using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject shapeL;
    public int bottonNumber = -40;
    public bool isActive = false;

    public float timeElapsed = 0;

    public GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        //Debug.Log
    }

    // Update is called once per frame
    // Every frame, increment time elapsed
    // If time elapsed has passed movement interval, do movement
    // Move object down by movement amount
    // Check if movement amount == max Limit
    // if we're at the max limit, play particle efecct
    void Update()
    {
        if (!isActive) return;
        
        timeElapsed += Time.deltaTime;
        Debug.Log(timeElapsed);

        if (!(transform.position.y <= bottonNumber))
        {

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                shapeL.transform.position += new Vector3(0, -10, 0);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                shapeL.transform.position += new Vector3(0, 10, 0);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                shapeL.transform.position += new Vector3(10, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                shapeL.transform.position += new Vector3(-10, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
            return;
        }
        else
        {
            GameObject.Instantiate(gameManager.placedParticle, this.transform);
            isActive = false;
        }


    }
}
