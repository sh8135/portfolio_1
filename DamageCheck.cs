using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCheck : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private Slider ultBar;

    private Status enemyStat;
    private Status playerStat;
    private string myName;

    public bool enemyAtt = false;
    public float damege = 35;
    public GameObject gunHitEff;

    void Start()
    {
        myName = this.gameObject.name;

        hpBar = GameObject.Find("HpBar").GetComponent<Slider>();
        ultBar = GameObject.Find("UltBar").GetComponent<Slider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyStat = other.transform.parent.gameObject.GetComponent<Status>();
            if (enemyStat.invincibility == false)
            {
                enemyStat.curHP -= damege;
                if (gameObject.name == "AutoHitBox")
                {
                    playerStat = transform.parent.GetComponent<Status>();
                    if (playerStat.ultGauge < 100)
                    {
                        playerStat.ultGauge += 25;
                        ultBar.value += 0.1f;
                    }
                }
                Instantiate(gunHitEff, other.transform.position, other.transform.rotation);
            }
        }
        if (other.gameObject.CompareTag("Wall") &&
        (myName == "UltHitBox(Clone)" || myName == "TankShoot_1(Clone)" || myName == "FrontHitBox" || myName == "BackHitBox"))
        {
            enemyStat = other.gameObject.GetComponent<Status>();
            if (enemyStat.invincibility == false)
            {
                enemyStat.curHP -= damege;
                Instantiate(gunHitEff, other.transform.position, other.transform.rotation);
            }
        }
        if (enemyAtt && (other.gameObject.CompareTag("Player") || other.gameObject.name == "Tank"))
        {
            enemyStat = other.gameObject.GetComponent<Status>();
            if (enemyStat.invincibility == false)
            {
                enemyStat.curHP -= damege;
                hpBar.value = enemyStat.curHP / enemyStat.maxHP;
                Instantiate(gunHitEff, other.transform.position, other.transform.rotation);
            }
        }
    }
}
