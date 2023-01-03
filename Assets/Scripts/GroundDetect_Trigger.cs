using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect_Trigger : MonoBehaviour
{
   public bool grounded;

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Esta en el suelo
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // No esta en el suelo
        grounded = false;
    }
}
