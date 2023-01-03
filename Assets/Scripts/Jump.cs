using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Variables
    Rigidbody2D rb;
    public float force = 20;
    public float forceAir = 100;
    public int jumps_max = 2;
    int jumps;
    GroundDetect_Raycast ground;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<GroundDetect_Raycast>();
    }

    // Update is called once per frame
    void Update()
    {
        // Resetear 
        if (ground.grounded)
        {
            jumps = jumps_max;
        }

        // - Se lanza 2 acciones cundo se pulsa Jump. Los saltos no pueden ser menos que 0
        if (Input.GetButtonDown("Jump") && jumps > 0)
        {
            rb.AddForce(new Vector2(0, force));
            // - Resta 1 a los saltos maximos
            jumps--;
            
            if (ground.grounded)
            {
                rb.AddForce(new Vector2(0, force));
            }
            else
            {
                rb.AddForce(new Vector2(0, forceAir));
            }
        }
    }

}
