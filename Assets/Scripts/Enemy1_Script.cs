using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Script : MonoBehaviour
{
    public GameObject PLAYER;

    private void Update()
    {
        Vector3 direction = PLAYER.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
    }
}
