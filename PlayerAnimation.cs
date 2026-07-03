using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //public Animation pAni;
    public Animator anim;
    
    //[HideInInspector]
    //public bool standing = false;

    //private bool getReady = false;

    // Start is called before the first frame update
    void Start()
    {
        //pAni = GetComponent<Animation>();
        //pAni.CrossFade("Idle_Shoot_Ar");

        anim = GetComponent<Animator>();
        //pAnimator.Play("Idle_Shoot_Ar");


        //pAnimator.SetBool("isRunnig", true);
    }

    public void MoveAni()
    {
        if (Input.GetAxisRaw("Vertical") > 0.1f)
        {
            anim.Play("WalkFront_Shoot_AR");
        }
        else if (Input.GetAxisRaw("Vertical") < -0.1f)
        {
            anim.Play("WalkBack_Shoot_AR");
        }
        else if (Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            anim.Play("WalkRight_Shoot_AR");
        }
        else if (Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            anim.Play("WalkLeft_Shoot_AR");
        }/*
        else
        {
            pAnimator.Play("Idle_Shoot_Ar");
        }*/
    }

    public void WalkFoward()
    {
        //pAnimator.SetBool("isRunnig", false);
        //pAni.GetComponent<Animation>()[jump.name].layer = 2;
        //pAni.CrossFade("WalkFront_Shoot_AR");
        anim.Play("WalkFront_Shoot_AR");
    }

    public void WalkBack()
    {
        anim.Play("WalkBack_Shoot_AR");
    }

    public void WalkRight()
    {
        anim.Play("WalkRight_Shoot_AR");
    }

    public void WalkLeft()
    {
        anim.Play("WalkLeft_Shoot_AR");
    }


    public void StandAni()
    {
        anim.Play("Idle_Shoot_Ar");

        //pAnimator.SetBool("isRunnig", true);
        /*
        if(!standing)
        {
        pAni.Play(readyShoot.name);
        StartCoroutine(StandDelay(5.0f));
        }
        */
    }
    

    public void JumpAni()
    {
        //pAnimator.GetComponent<Animation>()[jump.name].layer = 2;
        anim.Play("Jump");
    }

    public void AutoGunAni()
    {
        //pAnimator.GetComponent<Animation>()[AutoShoot.name].layer = 2;
        anim.Play("Shoot_Autoshot_AR");
    }

    public void SingleGunAni()
    {
        //pAnimator.GetComponent<Animation>()[AutoShoot.name].layer = 2;
        anim.Play("Shoot_SingleShot_AR");
    }

    public void DieAni()
    {
        //pAnimator.GetComponent<Animation>()[die.name].layer = 2;
        anim.Play("Die");
    }

    public void DuckingAni()
    {
        //pAnimator.GetComponent<Animation>()[ducking.name].layer = 2;
        anim.Play("Idle_Ducking_AR");
    }

/*
    IEnumerator StandDelay(float delay)
    {
        while (delay > 0.0f && !standing)
        {
            print("StandDelay 코루틴 실행중");
            delay -= 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        pAni.Play(stand.name);
        standing = true;
        yield break;
    }
    */
}
