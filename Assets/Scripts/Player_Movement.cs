using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;
    public float RayLenght = 1;
    public LayerMask mask;
    public List<Vector3> originPoints;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
   
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
        if (Input.GetKeyDown(KeyCode.W) && Grounded) 
        {
            Jump();
        }

        // Input para disparar
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }


        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }
}
