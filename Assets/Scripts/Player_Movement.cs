using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float RayLenght = 1;
    public LayerMask mask;
    public List<Vector3> originPoints;

    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;
    private Animator Animator;
   
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento del jugador
        Horizontal = Input.GetAxisRaw("Horizontal");
        transform.position = transform.position + new Vector3(Horizontal * Time.deltaTime * Speed, 0);

        // Mirror effect dependiendo donde apunte el player
        if (Horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Animator.SetBool("running", Horizontal != 0.0f);


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



        // Input para saltar
        if (Input.GetKeyDown(KeyCode.Space) && Grounded) 
        {
            Jump();
        }
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

}
