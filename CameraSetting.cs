using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSetting : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float targetHeight = 1.5f;
    public float distance = 5.0f;
    public float zoomRate = 80;
    public float minDistance = 3.0f;
    public float maxDistance = 7.0f;
    public float yMinLimit = -10;
    public float yMaxLimit = 70;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    public bool zoomOn = false;
    public GameObject gunHitPtc;
    //public LayerMask layerMask;

    [SerializeField]
    private Slider ultBar;
    private float zoomHeight = 2.0f;
    private float zoomDistance = -0.7f;
    private float x = 20.0f;
    private float y = 0.0f;
    Ray ray;
    float rayDistance = 600.0f;
    RaycastHit rayHit;

    private Status stat;

    //private GameManager gameManager;
    //private PlayerAttack playerAtt;

    void Start()
    {
        stat = target.GetComponent<Status>();
        ultBar = GameObject.Find("UltBar").GetComponent<Slider>();
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    // Update is called once per frame
    void Update()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (target) //&& gameManager.menuOn)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            //playerAtt = target.GetComponent<PlayerAttack>();
            if (!zoomOn)
            {
                //마우스 휠로 카메라와 target 거리 변화
                distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
                //거리의 최소 최대 값 제한
                distance = Mathf.Clamp(distance, minDistance, maxDistance);

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                Quaternion rotation = Quaternion.Euler(y, x, 0);
                transform.rotation = rotation;

                //traget 위치 살짝 뒤로 카메라 위치 이동
                Vector3 camPosition = target.position - (rotation * Vector3.forward * distance + new Vector3(0.0f, -targetHeight, 0.0f));
                transform.position = camPosition;
            }
            else
            {
                y = ClampAngle(y, yMinLimit - 60, yMaxLimit);

                Quaternion rotation = Quaternion.Euler(y, x, 0);
                transform.rotation = rotation;

                //traget 위치 살짝 뒤로 카메라 위치 이동
                Vector3 camPosition = target.position - (rotation * Vector3.forward * zoomDistance + new Vector3(0.0f, -zoomHeight, 0.0f));
                transform.position = camPosition;
            }

            //타겟 rotation 변경
            if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
             && target.CompareTag("Player"))
            {
                stat = target.GetComponent<Status>();
                if (!stat.stun && !stat.jumping)
                {
                    target.transform.rotation = Quaternion.Euler(0, x, 0);
                }
            }
            else if (target.CompareTag("TankHead"))
            {
                target.transform.rotation = Quaternion.Euler(0, x, 0);
            }
        }
    }

    // 앵글의 최소 최대 제한
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    public void ZoomCheck(bool zoom)
    {
        zoomOn = zoom;
    }

    public void ZoomShoot()
    {
        ray = new Ray();

        print("zoomshoot 실행");

        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.blue, 10.0f);

        if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayDistance))
        {
            print(rayHit.collider.name);
            
            if (rayHit.collider.CompareTag("Enemy"))
            {
                Instantiate(gunHitPtc, rayHit.transform);
                rayHit.transform.parent.gameObject.GetComponent<Status>().curHP -= 70;
                if (stat.ultGauge < 100)
                {
                    stat.ultGauge += 25;
                    ultBar.value += 0.1f;
                }
            }
        }


        /*
        rayHit = Physics.RaycastAll(transform.position, transform.forward, rayDistance);
        
        foreach(RaycastHit i in rayHit )
        {
            if(i.collider.CompareTag("Enemy"))
            {
                print(i.collider.name);
                Instantiate(gunHitPtc, i.collider.transform);
                i.transform.parent.gameObject.GetComponent<Status>().curHP -= 70;
                if (stat.ultGauge < 100)
                {
                    stat.ultGauge += 25;
                    ultBar.value += 0.1f;
                }
                break;
            }
            else if(i.transform.CompareTag("Wall") || i.transform.CompareTag("Ground"))
            {
                print(i.collider.name);
                //Instantiate(gunHitPtc, i.transform);
                break;
            }
        }*/
    }
}
