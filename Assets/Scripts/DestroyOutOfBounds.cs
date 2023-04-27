using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float boundLeft = -13f;
    void Update()
    {
        if(transform.position.x < boundLeft)
        {
            Destroy(this.gameObject);
        }
    }
}
