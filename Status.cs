using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public float maxHP = 100;
    public float curHP;
    public float dieDelay = 5.0f;   //5초 후 Destory
    public bool invincibility = false;
    public bool stun = false;
    public int ultGauge = 0;

    //[HideInInspector]
    public bool standing = false;
    public bool jumping = true;

    [SerializeField]
    private Slider hpBar;

    private PlayerAnimation pAni;
    private CameraSetting cam;
    private GameManager gameManager;

    void Start()
    {
        pAni = GetComponentInChildren<PlayerAnimation>();
        curHP = maxHP;

        if (gameObject.CompareTag("Player"))
        {
            hpBar = GameObject.Find("HpBar").GetComponent<Slider>();
            hpBar.value = curHP / maxHP;
        }
    }

    void Update()
    {
        if (curHP <= 0)
        {
            if (gameObject.CompareTag("Player") && dieDelay == 5.0f)
            {
                StunActivate(10000);
                pAni.DieAni();
                //UI ON
                cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraSetting>();
                cam.target = null;
                StartCoroutine("DieDelay");
            }
            else if (gameObject.name == "Tank" && dieDelay == 5.0f)
            {
                StunActivate(10000);
                //tank 폭파 파티클
                //UI ON
                cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraSetting>();
                cam.target = null;
                StartCoroutine("DieDelay");
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                gameManager.ClearGame(1);
                Destroy(gameObject);
                //tank 폭파 파티클
                //UI ON
            }
            else if (gameObject.CompareTag("Wall"))
            {
                Destroy(gameObject);
                //tank 폭파 파티클
                //UI ON
            }
        }
    }

    public void StunActivate(float stunDelay)
    {
        stun = true;
        StartCoroutine(StunDelay(stunDelay));
    }

    IEnumerator DieDelay()
    {
        while (dieDelay > 0.0f)
        {
            //print("DieDelay 코루틴 실행중");
            dieDelay -= 1.0f;
            yield return new WaitForSeconds(1.0f);
        }
        StopCoroutine("StunDelay");
        Destroy(gameObject);
        yield break;
    }

    IEnumerator StunDelay(float Delay)
    {
        while (Delay > 0.0f)
        {
            //print("StunDelay 코루틴 실행중");
            Delay -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        stun = false;
        invincibility = false;
        yield break;
    }
}
