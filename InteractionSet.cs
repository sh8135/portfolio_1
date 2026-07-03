using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSet : MonoBehaviour
{
    private TankRide tRide;
    private bool downE = false;

    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            downE = true;
        }
        else
        {
            downE = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Tank")
        {
            if (downE)
            {
                print("탱크탑승");
                tRide = other.GetComponent<TankRide>();
                tRide.enabled = true;
                tRide.owner = transform.parent.gameObject;
                tRide.Ride();
            }
        }
    }
}
