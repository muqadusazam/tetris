using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Block : MonoBehaviour
{
    public List<Transform> blocks; // list of block prefabs to store

    private void Awake()
    {
        // getting the list of blocks object present in the game and storing them in the list of block prefabs variable
        blocks = GetComponentsInChildren<Transform>().Where(x => x != this.transform).ToList();
    }
}
