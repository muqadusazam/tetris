using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void changeScene(string name)
    {
        if (name.Equals("end"))
        {
            Application.Quit();

        }
        else
        {
            SceneManager.LoadScene(name, LoadSceneMode.Single);
        }
        
    }
}
