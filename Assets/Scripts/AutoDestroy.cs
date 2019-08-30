using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float maxLifetime;
    float timeElapsed = 0;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= maxLifetime)
            GameObject.Destroy(this.gameObject);
    }
}
