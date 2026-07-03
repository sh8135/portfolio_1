using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    public float moveSpeed = 1500f;
    public float rotateSpeed = 20f;
    public GameObject frontBox;
    public GameObject backBox;

    public Vector3 moveVec;
    private Rigidbody rigid;
    private Status stat;
    private float rotateAngle;
    private Vector3 maxVel;

    // Start is called before the first frame update
    void Start()
    {
        stat = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        rigid = GetComponent<Rigidbody>();
        moveVec = new Vector3(0, 0, 0);

        if (Input.GetAxisRaw("Vertical") > 0.1f)
        {
            //pAni.WalkFoward();
            //stat.standing = false;
            moveVec += transform.forward;
            rotateAngle = rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            transform.Rotate(0, rotateAngle, 0);
            stat.standing = false;
            frontBox.SetActive(true);
        }
        else if (Input.GetAxisRaw("Vertical") < -0.1f)
        {
            //pAni.WalkBack();
            //stat.standing = false;
            moveVec -= transform.forward;
            rotateAngle = rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            transform.Rotate(0, rotateAngle, 0);
            stat.standing = false;
            backBox.SetActive(true);
        }
        else
        {
            if (!stat.standing)
            {
                stat.standing = true;
                frontBox.SetActive(false);
                backBox.SetActive(false);
            }
        }

        moveVec *= moveSpeed;
        //moveVec.x= Mathf.Round(moveVec.x);
        //moveVec.x = Mathf.Round(moveVec.z);
        rigid.angularVelocity = new Vector3(0, 0, 0);

        //moveVec *= 27;
        moveVec.x = Mathf.Round(moveVec.x);
        moveVec.z = Mathf.Round(moveVec.z);
        rigid.AddForce(moveVec.x, 0, moveVec.z);
        //transform.Translate(moveVec * Time.deltaTime);
        limitMoveSpeed(new Vector3(7, 0, 7));
    }

    void limitMoveSpeed(Vector3 maxVelocity)
    {

        maxVel.x = Mathf.Clamp(rigid.linearVelocity.x, -maxVelocity.x, maxVelocity.x);
        maxVel.z = Mathf.Clamp(rigid.linearVelocity.z, -maxVelocity.z, maxVelocity.z);

        rigid.linearVelocity = new Vector3(maxVel.x, rigid.linearVelocity.y, maxVel.z);

        /*
                System.Math.Round(rigid.velocity.x, 2);
                System.Math.Round(rigid.velocity.z, 2);
                if (rigid.velocity.x > maxVelocity.x && rigid.velocity.x > 0)
                {
                    rigid.velocity = new Vector3(maxVelocity.x, rigid.velocity.y, rigid.velocity.z);
                }
                if (rigid.velocity.x < maxVelocity.x && rigid.velocity.x < 0)
                {
                    rigid.velocity = new Vector3(maxVelocity.x, rigid.velocity.y, rigid.velocity.z);
                }

                if (rigid.velocity.z > maxVelocity.z && rigid.velocity.z > 0)
                {
                    rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, maxVelocity.z);
                }
                if (rigid.velocity.z < maxVelocity.z && rigid.velocity.z < 0)
                {
                    rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, maxVelocity.z);
                }*/
    }
}
