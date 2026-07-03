using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankRide : MonoBehaviour
{
    //[HideInInspector]
    public GameObject owner;

    private Slider hpBar;
    private Slider ultBar;
    [SerializeField]
    private Status stat;

    CameraSetting cam;

    // Start is called before the first frame update
    void Awake()
    {
        //stat = gameObject.GetComponent<Status>();
        hpBar = GameObject.Find("HpBar").GetComponent<Slider>();
        ultBar = GameObject.Find("UltBar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            stat = owner.GetComponent<Status>();
            print("탱크하차");
            owner.transform.parent = null;
            owner.transform.position = transform.position + new Vector3(0, 4, 0);
            owner.SetActive(true);
            cam.target = owner.transform;
            owner = null;
            this.transform.GetChild(0).GetComponent<TankAttack>().enabled = false;
            GetComponent<TankControl>().enabled = false;
            this.enabled = false;
            hpBar.value = stat.curHP / stat.maxHP;
            
        }
    }

    public void Ride()
    {
        stat = gameObject.GetComponent<Status>();
        hpBar.value = stat.curHP / stat.maxHP;
        owner.SetActive(false);
        owner.transform.position = transform.position;
        owner.transform.parent = transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSetting>();
        cam.target = this.transform.GetChild(0);
        this.transform.GetChild(0).GetComponent<TankAttack>().enabled = true;
        GetComponent<TankControl>().enabled = true;
    }
}
