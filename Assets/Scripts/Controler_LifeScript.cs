using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Controler_LifeScript;

public class Controler_LifeScript : MonoBehaviour
{
    public int lifes_current;
    public int lifes_max;
    public float invencible_time;
    public enum DeathMode { Teleport, ReloadScene, Destroy }
    public DeathMode death_mode;
    public Transform respawn;

    private Rigidbody2D Rigidbody2D;
    private bool invencible;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        lifes_current = lifes_max;
    }


    // Hace daño al jugador y lo pone en modo invencible
    public void Damage(int damage = 1, bool ignoreInvencible = false)
    {
        if (!invencible || ignoreInvencible)
        {
            lifes_current -= damage;
            StartCoroutine(Invencible_Corutine());
            if (lifes_current <= 0)
            {
                Death();
            }
        }

    }

    public void Death()
    {
        Debug.Log("Has muerto!");
        switch (death_mode)
        {
            case DeathMode.Teleport:
                Rigidbody2D.velocity = new Vector2(0, 0);
                transform.position = respawn.position;
                lifes_current = lifes_max;
                break;
            case DeathMode.ReloadScene:
                break;
            case DeathMode.Destroy:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    // Rutina para el estado de invencible
    IEnumerator Invencible_Corutine()
    {
        invencible = true;
        yield return new WaitForSeconds(invencible_time);
        invencible = false;
    }
}
