using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_CameraScript : MonoBehaviour
{
    [SerializeField]
    private float parallaxEffect;
    private float startPos;
    private GameObject Camera;

    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        startPos = transform.position.x;
    }

    
    void Update()
    {
        float distance = (Camera.transform.position.x * parallaxEffect);
        transform.position= new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
