using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Transform effPosition;       //총 쏘는 파티클 위치
    public GameObject autoEff;          //총 쏘는 파티클
    public GameObject autoHitBox;
    public GameObject ultTank;
    public float AtkSpeed = 1.0f;       //공격속도 비율
    public float SlowMoveSpd = 2.0f;    //공격 시 느려지는 비율
    public bool zoomOn = false;

    private bool isShooting = false;
    private float shootDelay = 0.0f;
    private float hitBoxTime = 0.3f;
    private Status stat;
    private PlayerControl pCon;
    private PlayerAnimation pAni;
    private CameraSetting cam;
    private InteractionSet interSet;

    [SerializeField]
    private Slider attDelayBar;
    [SerializeField]
    private Slider ultBar;
    [SerializeField]
    private GameObject scopeImg;

    void Start()
    {
        pCon = GetComponent<PlayerControl>();
        pAni = GetComponentInChildren<PlayerAnimation>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraSetting>();
        interSet = GetComponentInChildren<InteractionSet>();

        attDelayBar = GameObject.Find("AttDelayBar").GetComponent<Slider>();
        ultBar = GameObject.Find("UltBar").GetComponent<Slider>();
        print(GameObject.Find("PlayPanel").transform.GetChild(0).name);
        scopeImg = GameObject.Find("PlayPanel").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        stat = GetComponent<Status>();
        if (!stat.stun)
        {
            if (Input.GetMouseButtonDown(0) && !isShooting)
            {
                ShootGun();
            }

            if (Input.GetKey(KeyCode.R) && stat.ultGauge == 100 && !zoomOn)
            {
                stat.invincibility = true;
                Instantiate(ultTank, transform.forward * 10.0f + transform.position, transform.rotation);
                stat.ultGauge = 0;
                ultBar.value = 0;
                stat.StunActivate(0.5f);
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (!zoomOn)
                {
                    zoomOn = true;
                    cam.ZoomCheck(zoomOn);
                    scopeImg.SetActive(true);
                    pCon.enabled = false;
                    interSet.enabled = false;
                }
                else
                {
                    zoomOn = false;
                    cam.ZoomCheck(zoomOn);
                    scopeImg.SetActive(false);
                    pCon.enabled = true;
                    interSet.enabled = true;
                }
            }
        }
    }

    private void ShootGun()
    {
        if (!zoomOn)
        {
            isShooting = true;
            pCon.moveSpeed /= SlowMoveSpd;
            shootDelay = 1.0f;
            hitBoxTime = 0.3f;
            autoHitBox.SetActive(true);
            Instantiate(autoEff, effPosition.position, effPosition.rotation);
            StartCoroutine("HitDelayCheck");
            StartCoroutine(ShootDelayCheck(AtkSpeed));
            if (stat.standing)
                pAni.AutoGunAni();
        }
        else
        {
            isShooting = true;
            shootDelay = 1.0f;
            StartCoroutine(ShootDelayCheck(AtkSpeed/2));
            cam.ZoomShoot();
            pAni.SingleGunAni();
        }
    }

    IEnumerator HitDelayCheck()
    {
        while (hitBoxTime > 0.0f)
        {
            //print("HitDelayCheck 코루틴 실행중");
            hitBoxTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        pCon.moveSpeed *= SlowMoveSpd;
        autoHitBox.SetActive(false);
        hitBoxTime = 0.3f;
        yield break;
    }

    IEnumerator ShootDelayCheck(float delay)
    {
        while (shootDelay > 0.0f)
        {
            //print("ShootDelayCheck 코루틴 실행중");
            shootDelay -= delay / 10;
            attDelayBar.value = 1 - shootDelay;
            yield return new WaitForSeconds(0.1f);
        }
        isShooting = false;
        yield break;
    }
}