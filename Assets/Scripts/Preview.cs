using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preview: MonoBehaviour
{
    public Image myImage; // variavke to store the image
    public Sprite[] blockPics; // variable to store the list of sprites
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change(int num) // method to change the image based on the random number generated
    {
        switch (num) // swtich statement to switch image based on the number
        {
            case 0:
                //first block image
                myImage.sprite = blockPics[0];
                break;
            case 1:
                //second block image
                myImage.sprite = blockPics[1];
                break;
            case 2:
                //third block image
                myImage.sprite = blockPics[2];
                break;
            case 3:
                //fourth block image
                myImage.sprite = blockPics[3];
                break;
            case 4:
                //fifth block image
                myImage.sprite = blockPics[4];
                break;
            case 5:
                //sisth block image
                myImage.sprite = blockPics[5];
                break;
            case 6:
                //seventh block image
                myImage.sprite = blockPics[6];
                break;
            default:
                // default picture
                myImage.sprite = blockPics[7];
                break;

        }
    }
}
