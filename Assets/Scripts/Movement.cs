using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // ----------------------------------------- Public Variables -----------------------------------------
    public AudioSource audioSource; // Audio source which gets and switches the audio clip to play
    public AudioClip movementAudio; // pop audio clip which plays on the movement of the block
    public AudioClip touchDownAudio; // audio effect played once the block is landed on the ground or collided
    public Block border; // blocks which makes up the border
    public List<Block> blocklist; // getting all the blocks in a list
    public int bottonNumber = -40; // bottom number of the bottom boundary
    public bool isActive = false; // movement of the blocks coming down
    public GameManager gameManager; // main game manager variable which contains every object of the game
    public Preview image; // image of the next spawning block
    public List<Block> currentShape; // current shape where trigger is on
    public Scene currentScene; // current scene which is loaded


    // ----------------------------------------- Private Variables -----------------------------------------
    private Block nextShape; // next block is randomly generated to be spawned
    private int timeFalling = 0; // fall down time
    private float timeElapsed = 0; // delta Time which is added on every frame in update
    private int rotation = 90; // rotation value of movement
    private Vector3 movement; // movement vector position
    private int sIndex = 0; // first index
    private bool hit = false; // boolean value used for hit to check if the blocks have hit the roof
    int num; // number generated randomly is stored in this variable


    // ----------------------------------------- NOTE -----------------------------------------
    // Update is called once per frame
    // Every frame, increment time elapsed
    // If time elapsed has passed movement interval, do movement
    // Move object down by movement amount
    // Check if movement amount == max Limit
    // if we're at the max limit, play particle efecct
    // ----------------------------------------- NOTE -----------------------------------------


    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        gameManager = FindObjectOfType<GameManager>();
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

    void Update()
    {
        if (!isActive) return;
        int set = IsTouchedDown();
        timeElapsed += Time.deltaTime;
        fallDown();
        if (hit)
        {
            if (currentScene.name == "Modified")
            {
                foreach (var blockExist in gameManager.blockPrefebs)
                {
                    //blockExist.gameObject.SetActive(false);
                    Destroy(blockExist);
                    //blockExist.gameObject.SetActive(false);
                }
                
            }
            else
            {
                isActive = false;
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }

        if (set > 0)
        {

            if (Input.GetKeyDown(KeyCode.DownArrow) && set != -1)
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
                
                if (sIndex == 3)
                {
                    rotation = 0;
                }
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
                test = -1;
            }
            else if (curr.position.x <= -60) {
                test = 2;
            }
            else if (curr.position.x >= 50) {
                test = 3;
            }
        }
        return test;
    }

    private bool HitRoof()
    {
            if (currentShape[sIndex].transform.position.y > 90)
            {
                return true;
            }
        return false;
    }

    private bool IsCollide()
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
        return false;
    }

    private int randomNum() {
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
        }
        //Debug.Log(test);
    }

    private void removeBlocks() {
        Debug.LogWarning("success");
    }
}
