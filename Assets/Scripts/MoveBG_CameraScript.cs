using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBG_CameraScript : MonoBehaviour
{
    private float Length;
    private float startPos;
    private GameObject Camera;
    [SerializeField] private float parallaxEffect;

    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        startPos = transform.position.x;
        Length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    
    void Update()
    {
        float distance = (Camera.transform.position.x * parallaxEffect);
        float temp = (Camera.transform.position.x * (1 - parallaxEffect));

        transform.position= new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + Length)
        {
            startPos += Length;
        }
        else if (temp < startPos - Length)
        {
            startPos -= Length;
        }
    }
}
