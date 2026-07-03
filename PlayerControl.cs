using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float moveSpeed = 7f;
    public float jumpPower = 4.5f;
    //public float maxVelocity = 7f;

    public Vector3 test;

    public Vector3 moveVec;
    private Rigidbody rigid;
    private Status stat;
    private PlayerAnimation pAni;

    void Start()
    {

        rigid = GetComponent<Rigidbody>();
        pAni = GetComponentInChildren<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        stat = GetComponent<Status>();
        if (!stat.stun && !stat.jumping)
        {
            Move();
            Jump();
        }/*
        else if(jumping)
        {
            CheckJump();
        }*/
    }

    void Move()
    {
        rigid = GetComponent<Rigidbody>();

        // 애니메이션 실행
        pAni.MoveAni();
        moveVec = new Vector3(0, 0, 0);
        if (Input.GetAxisRaw("Vertical") > 0.1f)
        {
            //pAni.WalkFoward();
            stat.standing = false;
            moveVec += transform.forward;
        }
        if (Input.GetAxisRaw("Vertical") < -0.1f)
        {
            //pAni.WalkBack();
            stat.standing = false;
            moveVec -= transform.forward / 2;
        }
        if (Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            //pAni.WalkRight();
            stat.standing = false;
            moveVec += transform.right / 2;
        }
        if (Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            //pAni.WalkLeft();
            stat.standing = false;
            moveVec -= transform.right / 2;
        }


        if (moveVec.z == 0 && moveVec.x == 0 && stat.standing == false)
        {
            stat.standing = true;

            pAni.StandAni();
        }

        moveVec = moveVec.normalized * moveSpeed;
        //rigid.angularVelocity = new Vector3(0, 0, 0);
        //rigid.velocity = new Vector3(moveVec.x, rigid.velocity.y, moveVec.z);

        rigid.AddForce(moveVec.x, 0, moveVec.z);
        limitMoveSpeed(moveVec/2f);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stat.standing = false;
            stat.jumping = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            //rigid.angularVelocity = new Vector3(0, 0, 0);
            GetComponentInChildren<PlayerAnimation>().JumpAni();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && stat.jumping) //&& rigid.velocity.x == 0 && rigid.velocity.z == 0)
        {
            VelCheck();

            //if(stat.standing)
            //    stat.standing = 
        }
    }

    private void VelCheck()
    {
        rigid = GetComponent<Rigidbody>();
        Vector3 testVel = rigid.linearVelocity;

        testVel.x = Mathf.Round(testVel.x);
        testVel.y = Mathf.Round(testVel.y);
        testVel.z = Mathf.Round(testVel.z);

        test = testVel;

        if (testVel == Vector3.zero)
        {
            stat.jumping = false;
        }
    }

    /*
        private void OnCollisionExit(Collision other)
        {
            if (!jumping)
                jumping = true;
        }*/

    void limitMoveSpeed(Vector3 maxVelocity)
    {
        if (rigid.linearVelocity.x > maxVelocity.x && rigid.linearVelocity.x > 0)
        {
            rigid.linearVelocity = new Vector3(maxVelocity.x, rigid.linearVelocity.y, rigid.linearVelocity.z);
        }
        if (rigid.linearVelocity.x < maxVelocity.x && rigid.linearVelocity.x < 0)
        {
            rigid.linearVelocity = new Vector3(maxVelocity.x, rigid.linearVelocity.y, rigid.linearVelocity.z);
        }

        if (rigid.linearVelocity.z > maxVelocity.z && rigid.linearVelocity.z > 0)
        {
            rigid.linearVelocity = new Vector3(rigid.linearVelocity.x, rigid.linearVelocity.y, maxVelocity.z);
        }
        if (rigid.linearVelocity.z < maxVelocity.z && rigid.linearVelocity.z < 0)
        {
            rigid.linearVelocity = new Vector3(rigid.linearVelocity.x, rigid.linearVelocity.y, maxVelocity.z);
        }

    }
    /*
        void CheckJump()
        {

            if (rigid.velocity == Vector3.zero)
            {
                jumping = false;
            }
        }*/
}