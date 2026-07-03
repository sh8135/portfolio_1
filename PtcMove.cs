using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtcMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 30f;
    public float moveDistance = 50f;

    private Vector3 startPoint;

    private void Start() 
    {
        startPoint = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(Vector3.Distance(startPoint, transform.position) > moveDistance)
        {
            Destroy(gameObject);
        }
    }
}
