using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float Speed;
    public Vector3 offSet;
    public Transform Target;

    //private GameObject PLAYER;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + offSet, Speed * Time.deltaTime);
    }
}
