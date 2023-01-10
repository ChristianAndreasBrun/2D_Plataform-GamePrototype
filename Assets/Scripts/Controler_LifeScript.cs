using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Controler_LifeScript;

public class Controler_LifeScript : MonoBehaviour
{
    public int CurrentLifes;
    public int MaxLifes;
    public float InvencibleTime;
    public enum DeathMode { Teleport, ReloadScene, Destroy}
    public DeathMode Death_Mode;
    public Transform Respawn;
    private Rigidbody2D Rigidbody2D;
    private bool Invencible;
    
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CurrentLifes = MaxLifes;
    }

    public void Damage(int damage = 1, bool ignoreInvencible = false)
    {
        if (!Invencible || ignoreInvencible)
        {
            CurrentLifes -= damage;
            StartCoroutine(Invencible_Corutine());
            if (CurrentLifes == 0)
            {
                Death();
            }
        }
    }

    public void Death()
    {
        Debug.Log("Has muerto!");
        switch (Death_Mode)
        {
            case DeathMode.Teleport:
                Rigidbody2D.velocity = new Vector2(0, 0);
                transform.position = Respawn.position;
                CurrentLifes = MaxLifes;
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
        Invencible = true;
        yield return new WaitForSeconds(InvencibleTime);
        Invencible = false;
    }
}
