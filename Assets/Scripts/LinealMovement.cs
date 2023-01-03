using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinealMovement : MonoBehaviour
{
    // ! Variables
    public List<Transform> points;
    int actualPoint;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        // - Funccion para recorrer los puntos. Transforma un float simple en un condicional de true/false
        if (Vector3.Distance(transform.position, points[actualPoint].position) < 0.1f)
        {
            actualPoint++;

            if(actualPoint >= points.Count)
            {
                actualPoint = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, points[actualPoint].position, speed * Time.deltaTime);
        

    }
}
