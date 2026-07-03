using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public GameObject attackBox;

    Animator anim;

    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        nav = transform.parent.GetComponent<NavMeshAgent>();

        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    private void UpdateTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 30f, 1 << 7);

        if (cols.Length > 0)
        {
            for( int i = 0; i < cols.Length; i++)
            {/*
                if(cols[i].CompareTag("Player"))
                {
                    target = cols[i].gameObject;
                    break;
                }
                else
                {
                    target = null;
                }*/
                target = cols[i].gameObject;
            }
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (!anim.GetBool("Open_Anim"))
            {
                anim.SetBool("Open_Anim", true);
                anim.SetBool("Walk_Anim", true);
            }
            nav.SetDestination(target.transform.position);
            /*
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_Walk_Loop"))
                        {
                            nav.SetDestination(target.position);
                        }*/
        }
        else if(target == null)
        {
            anim.SetBool("Open_Anim", false);
            anim.SetBool("Walk_Anim", false);
            nav.SetDestination(transform.position);
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.name == "Tank")
        {
            Instantiate(attackBox, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
            //히트박스 생성
            //제거
        }
    }
}
