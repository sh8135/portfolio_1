using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    Enemy enemy;

    void Awake() 
    {
        enemy = transform.GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            enemy.target = other.gameObject;
        }
        else if(other.name == "Tank")
        {
            enemy.target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player") || other.name == "Tank")
        {
            enemy.target = null;
        }
    }
}