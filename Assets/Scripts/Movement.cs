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
    public AudioClip blocksRemovedAudio; // audio effect of blocks removed
    public Block border; // blocks which makes up the border
    public GameObject[] obsticlesList; // list of obsticles onjects
    public GameObject life1; // life 1 game object image sprite
    public GameObject life2; // life 2 game object image sprite
    public GameObject life3; // life 3 game object image sprite
    public int lifeCount = 3; // count of life stored and is changed based on the state
    public List<Block> blocklist; // getting all the blocks in a list
    public int bottonNumber = -40; // bottom number of the bottom boundary
    public bool isActive = false; // movement of the blocks coming down
    public GameManager gameManager; // main game manager variable which contains every object of the game
    public Preview image; // image of the next spawning block
    public List<Block> currentShape; // current shape where trigger is on
    public Scene currentScene; // current scene which is loaded
    public int set; // it stores the current state of the touch down number and is checked against to perform action


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
        currentScene = SceneManager.GetActiveScene(); // getting the currently active scene
        gameManager = FindObjectOfType<GameManager>(); // getting the object of game manager
    }

    void Start()
    {
        
        obsticlesList = GameObject.FindGameObjectsWithTag("obsticles"); // initialising obsticles list by getting the tag
        nextShape = (Block)Instantiate(blocklist[randomNum()], new Vector3(0f, 100.0f, 0f), Quaternion.identity); // initialising next shape by random number
        num = randomNum(); // randomly gemerated number
        image.change(num); // changing the image by the number generated
        currentShape.Add(nextShape); // setting trigger on current shape by setting next shape as current block
        movement = currentShape[sIndex].transform.position; // activating movement on current shape
        isActive = true; // setting the active true so the block moves
    }

    void Update()
    {
        if (!isActive) return; // checking if not active then return out
        set = IsTouchedDown(); // updating set as the current state number from touchdown
        timeElapsed += Time.deltaTime; // completion time in seconds since the last frame
        fallDown(); // activating the time in order for the auto drop of the blocks per second
        setLifeCount(lifeCount);

        if (hit) // if the prefebs have hit the roof
        {
            if (currentScene.name == "Modified") {
                removeBlocksRandomly();
            } // if current scene is modified then simply stop the movement
            else { // otherwise stop the movement and return back to the main menu page
                isActive = false;
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }

        if (set > 0) // if not hit
        {
            // Activating down arrow button to lower the current block
            if (Input.GetKeyDown(KeyCode.DownArrow) && set != -1)
            {
                movement += new Vector3(0, -10, 0); // moveing down in y axis
                audioSource.clip = movementAudio; // setting the audio source of movement
                audioSource.Play(); // playing the audio
            }

            // Activating right arrow button to turn right the current block
            if (Input.GetKeyDown(KeyCode.RightArrow) && set != 3)
            {
                movement += new Vector3(10, 0, 0); // moveing down in x axis
                audioSource.clip = movementAudio; // setting the audio source of movement
                audioSource.Play(); // playing the audio
            }

            // Activating left arrow button to turn left the current block
            if (Input.GetKeyDown(KeyCode.LeftArrow) && set != 2)
            {
                movement += new Vector3(-10, 0, 0); // moveing down in x axis
                audioSource.clip = movementAudio; // setting the audio source of movement
                audioSource.Play(); // playing the audio
            }

            // Activating spacebar button to reshape the current block
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // checking if the rotation of the current block is 90 degree
                currentShape[sIndex].transform.rotation = Quaternion.Euler(0, 0, rotation);
                if (rotation >= 360) rotation = 0; // if the rotation is more or same as 360 then zero the rotation
                if (!IsCollide() && IsTouchedDown() > 0) // if the block has not landed
                {
                    rotation += 90; // add 90 degree angle on the block
                    audioSource.clip = movementAudio; // setting the audio source of movement
                    audioSource.Play(); // playing the audio
                }
            }

        }
        else
        {
            
            //create new block in right position
            gameManager.blockPrefebs.Add(currentShape[sIndex]);
            // everything zeroed to start with
            set = 0;
            timeElapsed = 0;
            timeFalling = 0;
            currentShape.RemoveAt(0); // remove the last block as current block
            nextShape = Block.Instantiate(blocklist[num], new Vector3(0f, 100.0f, 0f), Quaternion.identity); // Instantiating a new random block
            currentShape.Add(nextShape); // add the new shape as the current shape
            num = randomNum(); // get the next random number
            image.change(num); // change the image based on the new random number
            set = IsTouchedDown(); // get the updated value of the touchdown state
            movement = currentShape[sIndex].transform.position; // activating movement on current shape
            isActive = true; // setting the active true so the block moves

        }
        set = 1; // setting the number as touch down
        currentShape[sIndex].transform.position = movement; // activating the movement on the current block
    }

    // method for touch down which returns a number based on the their state
    private int IsTouchedDown()
    {   
        int test = 1; // default value as 1
        foreach (Transform curr in currentShape[sIndex].blocks) { // iterating through all the shapes in the currentshape list
            if (curr.transform.position.y <= bottonNumber || IsCollide()) // if position of the block coming has either touched the bottom numnber or collided
            {
                GameObject.Instantiate(gameManager.placedParticle, curr.transform); // Instantiate particle effects on the current position of the block
                checkObsticles(curr); // check for the obsticles set in modified level using the current block position
                audioSource.clip = touchDownAudio; // setting the audio source of touch down
                audioSource.Play(); // playing the audio
                    
                test = -1; // return -1 as touched down state value
            }
            else if (curr.position.x <= -60) { // checking if the left border to put constraint of movement
                test = 2; // state it as 2
            }
            else if (curr.position.x >= 50) { // checking if the right border to put constraint of movement
                test = 3; // state it as 3
            }
        }
        return test; // retun the dafault value
    }

    private bool HitRoof() // method for checking if the blocks have hit the roof
    {
            if (currentShape[sIndex].transform.position.y > 90) // checking if the position of cureent shape is above 90
            {
                return true; // then return true
            }
        return false; // otherwise false
    }

    private void setLifeCount(int count) // method to set the lives of the player by passing in the count numbner
    {
        if (lifeCount <= 0) // if all livea are gone
        {
            isActive = false; // stop the movement by deactivating
            // deactivating all lives images
            life1.SetActive(false);
            life2.SetActive(false);
            life3.SetActive(false);
            // finally load the main menu scene meaning the player is dead
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        else if (lifeCount == 1) // if only 1 life is left
        {
            // deactivating 2 lives images
            life1.SetActive(false);
            life2.SetActive(false);
        }
        else if (lifeCount == 2) // if 2 lives are left
        {
            // deactivating 1 life images
            life1.SetActive(false);
        }
    }

    private bool IsCollide() // checking if the block has collided with another block
    {
            foreach (var blockExist in gameManager.blockPrefebs) // iterate through all the blocks present in the game manager block list
            {
                if (blockExist != null) // checking if the list is not empty
                    foreach (Transform each in blockExist.blocks) // if not then iterate over through the individual block which makes up a prefab
                    {
                        foreach (Transform current in currentShape[sIndex].blocks) // for the current block prefab in the list
                        {
                            if (each.position + new Vector3(0, 10, 0) == current.position) // check if it's position is same as each other blocks in the prefab
                            {
                                if (currentShape[sIndex].transform.position.y == 100) // checking if the current shape is at 100
                                {
                                    hit = true; // means it has hit the roof
                                    return false; // returning flase because it has not collided instead hit the roof
                                }
                                else
                                {
                                return true; // otherwise it means that it has collided with another block present in the list
                                }

                            }
                        }
                    }
            }
        return false; // default return value
    }

    private int randomNum() { // method to generate random number
        return (int)Random.Range(0, blocklist.Count); // generating and returning random number based on the blocks list length

    }

    private void fallDown() // controls the movement
    {
        if (timeElapsed > timeFalling) { // checking if time has not elapased
            movement += new Vector3(0, -10, 0); // activate the movement for the blocks to come down
            timeFalling++; // increment it to bring it down
        }
    }

    private void removeBlocksRandomly() // method to remove the blocks randomly
    {
        foreach (var blockExist in gameManager.blockPrefebs) // iterating over through all the blocks in the game manager
        {
            if (blockExist != null) // checking the list is not empty
                if (blockExist.transform.position.y >= 10) // then check for all the blocks prefabs where position is above or equal to 10
                {
                    Destroy(blockExist.gameObject); // get them and destroy (remove) them from the game
                    audioSource.clip = blocksRemovedAudio; // setting the audio source of blocks removal
                    audioSource.Play(); // playing the audio
                    hit = false; // set the hit to be false (meaning it has not hit thr roof)
                }
        }
        lifeCount = lifeCount - 1; // remove one life from the player's lives
    }

    private void checkObsticles(Transform current) // method to check for the red trap blocks
    {
        foreach (GameObject obsticle in obsticlesList) // iterate over through the list of traps
        {
            // check if their position matches the traps position
            if (current.transform.position.x == obsticle.transform.position.x && current.transform.position.y == obsticle.transform.position.y)
            {
                lifeCount = lifeCount - 1; // then take down 1 life
            }
        }
    }
}