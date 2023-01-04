using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float RayLenght = 1;
    public LayerMask mask;
    public List<Vector3> originPoints;

    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        // Movimiento Horizontal del Player
        Horizontal = Input.GetAxisRaw("Horizontal");

        // Input del Player para saltar
        if (Input.GetKeyDown(KeyCode.X))
        {
            Jump();
        }


        // Raycast Detector
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

        if (!Grounded)
        {
            transform.parent = null;
        }



    }

        
     



    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }
}
