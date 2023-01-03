using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject PLAYER;

  
    void Update()
    {
        Vector3 position = transform.position;
        position.x = PLAYER.transform.position.x;
        position.y = PLAYER.transform.position.y;
        transform.position = position;
    }
}
