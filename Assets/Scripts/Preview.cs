using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preview: MonoBehaviour
{
    public Image myImage;
    public Sprite[] blockPics;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change(int num)
    {
        switch (num)
        {
            case 0:
                //first block image
                myImage.sprite = blockPics[0];
                break;
            case 1:
                //first block image
                myImage.sprite = blockPics[1];
                break;
            case 2:
                //first block image
                myImage.sprite = blockPics[2];
                break;
            case 3:
                //first block image
                myImage.sprite = blockPics[3];
                break;
            case 4:
                //first block image
                myImage.sprite = blockPics[4];
                break;
            case 5:
                //first block image
                myImage.sprite = blockPics[5];
                break;
            case 6:
                //first block image
                myImage.sprite = blockPics[6];
                break;
            default:
                myImage.sprite = blockPics[7];
                break;

        }
    }
}
