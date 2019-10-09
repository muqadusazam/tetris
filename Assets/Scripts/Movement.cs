using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip movementAudio;
    public AudioClip touchDownAudio;
    public Block border;
    public List<Block> blocklist;
    public int bottonNumber = -40;
    public bool isActive = false;
    public GameManager gameManager;
    public Preview image;

    public List<Block> currentShape;
    private Block nextShape;
    //private Block previewShape;
    private int timeFalling = 0;
    private float timeElapsed = 0;
    private int rotation = 90;
    private Vector3 movement;
    //private int index = 0;
    private int sIndex = 0;
    private bool hit = false;
    int num;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        //Debug.Log
    }

    void Start()
    {
        nextShape = (Block)Instantiate(blocklist[randomNum()], new Vector3(0f, 100.0f, 0f), Quaternion.identity);
        num = randomNum();
        image.change(num);
        currentShape.Add(nextShape);
        movement = currentShape[sIndex].transform.position;
        isActive = true;
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
        RowCheck();
        int set = IsTouchedDown();
        timeElapsed += Time.deltaTime;
        fallDown();
        if (hit)
        {
            isActive = false;
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        if (set > 0)
        {

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                movement += new Vector3(0, -10, 0);
                audioSource.clip = movementAudio;
                audioSource.Play();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && set != 3)
            {
                movement += new Vector3(10, 0, 0);
                audioSource.clip = movementAudio;
                audioSource.Play();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && set != 2)
            {
                movement += new Vector3(-10, 0, 0);
                audioSource.clip = movementAudio;
                audioSource.Play();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {

                currentShape[sIndex].transform.rotation = Quaternion.Euler(0, 0, rotation);
                if (rotation >= 360) rotation = 0;
                if (!IsCollide() && IsTouchedDown() > 0)
                {
                    rotation += 90;
                    audioSource.clip = movementAudio;
                    audioSource.Play();
                }
            }

        }
        else
        {

            //create new block in right position
            gameManager.blockPrefebs.Add(currentShape[sIndex]);
            set = 0;
            timeElapsed = 0;
            timeFalling = 0;

            currentShape.RemoveAt(0);


            nextShape = Block.Instantiate(blocklist[num], new Vector3(0f, 100.0f, 0f), Quaternion.identity);

            currentShape.Add(nextShape);
            num = randomNum();
            image.change(num);
            set = IsTouchedDown();
            movement = currentShape[sIndex].transform.position;
            isActive = true;


        }

        set = 1;
        currentShape[sIndex].transform.position = movement;

    }

    private int IsTouchedDown()
    {
        int test = 1;
        foreach (Transform curr in currentShape[sIndex].blocks) {
            if (curr.transform.position.y <= bottonNumber || IsCollide())
            {
                GameObject.Instantiate(gameManager.placedParticle, curr.transform);
                audioSource.clip = touchDownAudio;
                audioSource.Play();
                isActive = false;
                //return -1;
                test = -1;
            }
            else if (curr.position.x <= -60) {

                //return 2;
                test = 2;
            }
            else if (curr.position.x >= 50) {

                //return 3;
                test = 3;
            }
        }
        return test;
    }
    private bool HitRoof()
    {
        //foreach (Transform curr in currentShape[sIndex].blocks)
        {
            if (currentShape[sIndex].transform.position.y > 90)
            {
                return true;
            }
        }
        return false;
    }
    private bool IsCollide()
    {
        //if (currentShape[sIndex].transform.position.y < 90)
        {
            foreach (var blockExist in gameManager.blockPrefebs)
            {
                if (blockExist != null)
                    foreach (Transform each in blockExist.blocks)
                    {
                        foreach (Transform current in currentShape[sIndex].blocks)
                        {
                            if (each.position + new Vector3(0, 10, 0) == current.position)
                            {
                                if (currentShape[sIndex].transform.position.y == 100)
                                {
                                    hit = true;
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }

                            }
                        }
                    }
            }
        }
        return false;
    }

    private int randomNum() {
        //return blocklist[(int)Random.Range(0,blocklist.Count-1)];
        return (int)Random.Range(0, blocklist.Count);

    }

    private void fallDown()
    {
        if (timeElapsed > timeFalling) {
            movement += new Vector3(0, -10, 0);
            timeFalling++;
        }
    }

    private void RowCheck()
    {
        int[,] array3D = new int[12,14];
        int test = 0; 
        int[] count = new int[15];
        foreach (var blockExist in gameManager.blockPrefebs)
        {
            if (blockExist != null)
                foreach (Transform each in blockExist.blocks)
                {
                    /*
                    for (int i = -4; i == 10; i++) { // checking individual rows
                        if (each.position.y == i * 10)
                        {
                            count[index]++;
                        }
                    }
                    while (row <= 90) {
                        //if()
                            if (each.position.y == row)
                                count[index]++;
                        row += 10;
                    }
                    foreach (Transform each2 in blockExist.blocks)
                        if (each2.position.y == row)
                            count[index]++; */
                            
                    /*for (int i = 0; i <= 14; i++)
                    {
                        row = row + (i * 10);
                        if (each.position.y == row)
                            count[i]++;
                        
                    }*/

                    test++;
                }
        }

        for (int i = 0; i <= 14; i++)
        {
            if (count[i] == 12)
                removeBlocks();
            //Debug.Log(i + "row count: " +  count[i]);
        }
        //Debug.Log(test);
    }

    private void removeBlocks() {
        Debug.LogWarning("success");
    }
}
