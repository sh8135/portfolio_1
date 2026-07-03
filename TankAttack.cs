using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankAttack : MonoBehaviour
{
    public Transform effPosition;       //총 쏘는 파티클 위치
    public GameObject shootEff;          //총 쏘는 파티클
    public float AtkSpeed = 1.0f;       //공격속도 비율

    private float shootDelay = 0.0f;
    private bool isShooting = false;

    [SerializeField]
    private Slider attDelayBar;

    void Start() 
    {
        attDelayBar = GameObject.Find("AttDelayBar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            isShooting = true;
            shootDelay = 2.0f;
            Instantiate(shootEff, effPosition.position, effPosition.rotation);
            StartCoroutine(ShootDelayCheck(AtkSpeed));
        }
    }

    IEnumerator ShootDelayCheck(float delay)
    {
        while (shootDelay > 0.0f)
        {
            print("ShootDelayCheck 코루틴 실행중");
            shootDelay -= delay / 10;
            attDelayBar.value = 1 - shootDelay;
            yield return new WaitForSeconds(0.1f);
        }
        isShooting = false;
        yield break;
    }
}
