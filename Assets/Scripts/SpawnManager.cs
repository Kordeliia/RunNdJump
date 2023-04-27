using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController _pController;
    public GameObject [] obstaclePrefabs;
    private Vector3 spawnPos;
    private float firsTime = 1f;
    private float eachTime = 3f;

    private void Start()
    {
        _pController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnPos = transform.position;
            InvokeRepeating("SpawnObstacle", firsTime, eachTime);
    }
    void SpawnObstacle()
    {
        int indexObstacle = Random.Range(0, obstaclePrefabs.Length);
        if (_pController.GameOver == false)
        {
            Instantiate(obstaclePrefabs[indexObstacle], spawnPos, obstaclePrefabs[indexObstacle].transform.rotation);
        }
    }
}
