using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController _pController;
    public float speed = 5f;

    private void Start()
    {
        _pController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        if(_pController.GameOver == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
