using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt_EnemyScript : MonoBehaviour
{
    public GameObject Cannon;
    public GameObject PLAYER;
    public GameObject EnemyBullet;
    public int Health = 3;

    private float LastShoot;

    private void Update()
    {
        if (PLAYER == null) return;
        // El enemigo nos trackea
        Vector3 direction = PLAYER.transform.position - transform.position;
        if (direction.x >= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // El enemigo nos dispara al alacanzar la distancia establecida
        float distance = Mathf.Abs(PLAYER.transform.position.x - transform.position.x);
        if (distance < 4 && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Debug.Log("Dispara");

        Vector3 direction;
        if (transform.localScale.x == 1)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        GameObject bullet = Instantiate(EnemyBullet, Cannon.transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
}
