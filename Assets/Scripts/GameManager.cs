using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is the master to store each object of the game
    public List<Block> blockPrefebs; // list of the block prefabs
    public GameObject placedParticle; // particles effect stored in this variable
    public Block blockSpawn; // each block to be spawned
    public Transform spawnPoint; // tranform point from where the block has to be spawned
    public float movementInterval; // time interval for the movement
    public float movementAmount; // amount of movement

}
