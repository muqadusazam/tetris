using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Block : MonoBehaviour
{
    public List<Transform> blocks;

    private void Awake()
    {
        blocks = GetComponentsInChildren<Transform>().Where(x => x != this.transform).ToList();
    }
}
