using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float maxLifetime; // maximum time of life
    float timeElapsed = 0; // time gone since last frame

    void Update()
    {
        timeElapsed += Time.deltaTime; // incrementing time elapsed

        if (timeElapsed >= maxLifetime) // checking if time elapsed is greater or equal to max life (means time gone beyond or equal to game end)
            GameObject.Destroy(this.gameObject); // then detroy the game object
    }
}
