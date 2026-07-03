using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    private ParticleSystem Ptc;

    public void Start()
    {
        Ptc = GetComponentInChildren<ParticleSystem>();
    }

    public void Update()
    {
        if (Ptc)
        {
            if (!Ptc.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
