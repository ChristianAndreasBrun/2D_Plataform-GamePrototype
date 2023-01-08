using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioClip Sound;
    public float Speed;

    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;
    private Controler_LifeScript CurrentLifes;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
        CurrentLifes = GetComponent<Controler_LifeScript>();
    }

    void Update()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }



    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet() 
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScript PLAYER = collision.GetComponent<PlayerScript>();
        Grunt_EnemyScript GruntEnemy = collision.GetComponent<Grunt_EnemyScript>();
        if (PLAYER != null)
        {
            PLAYER.CurrentLifes();
        }
        if (GruntEnemy != null)
        {
            GruntEnemy.Hit();
        }
        DestroyBullet();
    }
}


