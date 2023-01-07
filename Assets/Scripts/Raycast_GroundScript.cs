using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Raycast_GroundScript : MonoBehaviour
{
    public float RayLenght = 1;
    public LayerMask mask;
    public List<Vector3> originPoints;
    public bool Grounded;

    void Start()
    {
        Grounded = false;
        for (int i = 0; i < originPoints.Count; i++)
        {
            Debug.DrawRay(transform.position + originPoints[i], Vector3.down * RayLenght, Color.red);

            // Crea un rayo invisible que detecta colision
            RaycastHit2D hit = Physics2D.Raycast(transform.position + originPoints[i], Vector3.down, RayLenght, mask);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "MobliePlataform")
                {
                    transform.parent = hit.transform;
                }

                Debug.DrawRay(transform.position + originPoints[i], Vector3.down * hit.distance, Color.green);
                Grounded = true;
            }
            else
            {
                transform.parent = null;
            }
        }
        //if (!Grounded)
        //{
        //    transform.parent = null;
        //}

    }
}

