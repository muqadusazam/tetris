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
        else if (name.Equals("Original"))
        {
            SceneManager.LoadScene("Original", LoadSceneMode.Single);
        }
        else if (name.Equals("Modified"))
        {
            SceneManager.LoadScene("Modified", LoadSceneMode.Single);
        }

    }
}
