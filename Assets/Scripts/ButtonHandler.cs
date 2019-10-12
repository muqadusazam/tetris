using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void changeScene(string name) // method to change the scene based on the button pressed
    {
        if (name.Equals("end")) // if the button is "equal"
        {
            Application.Quit(); // then exit out of the game

        }
        else if (name.Equals("Original")) // if the button is "Original"
        {
            SceneManager.LoadScene("Original", LoadSceneMode.Single); // then load the original game scene in a single load scene view
        }
        else if (name.Equals("Modified")) // if the button is "Modified"
        {
            SceneManager.LoadScene("Modified", LoadSceneMode.Single); // then load the modified game level in a single load scene view
        }
        else if (name.Equals("MainMenu")) // if the button is "MainMenu"
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); // then load the main menu game scene in a single load scene view
        }

    }
}
