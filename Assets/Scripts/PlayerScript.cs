using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject Cannon;
    public GameObject PlayerBullet;
    public float Speed;
    public float JumpForce;
    public int Health = 5;
    public float RayLenght = 1;
    public LayerMask mask;
    public List<Vector3> originPoints;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator= GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento Horizontal del Player
        Horizontal = Input.GetAxisRaw("Horizontal");
        if (Horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Variable bool - running
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


        // Input del Player para saltar
        if (Input.GetKeyDown(KeyCode.X))
        {
            Jump();
        }

        // Input del Player para disparar
        if (Input.GetKey(KeyCode.C) && Time.time > LastShoot + 0.25f) 
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
            direction= Vector2.left;
        }

        GameObject bullet = Instantiate(PlayerBullet, Cannon.transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
}
