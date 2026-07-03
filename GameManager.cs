using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int curPoint = 0;
    public int clearPoint = 8;

    //[SerializeField]
    public GameObject menuPanel;
    //[SerializeField]
    //public bool menuOn = false;
    //[SerializeField]
    public Transform camTarget;
    //[SerializeField]
    public CameraSetting cam;

    private UI ui;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            //text.text = curPoint + " / " + clearPoint;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuPanel = GameObject.Find("Canvas").transform.Find("Menu").gameObject;
                cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSetting>();
                camTarget = cam.target;

                if (menuPanel.activeSelf == false && camTarget.name != "Head")
                {
                    menuPanel.SetActive(true);
                    //menuOn = true;
                    Cursor.visible = true;
                    if (camTarget.CompareTag("Player"))
                    {
                        camTarget.GetComponent<PlayerAttack>().enabled = false;
                        camTarget.GetComponent<PlayerControl>().enabled = false;
                        cam.enabled = false;
                    }
                    else
                    {
                        camTarget.GetComponent<TankAttack>().enabled = false;
                        camTarget.GetComponent<TankControl>().enabled = false;
                        cam.enabled = false;
                    }
                }
                else if(camTarget.name != "Head")
                {
                    menuPanel.SetActive(false);
                    //menuOn = false;
                    Cursor.visible = false;
                    if (camTarget.CompareTag("Player"))
                    {
                        camTarget.GetComponent<PlayerAttack>().enabled = true;
                        camTarget.GetComponent<PlayerControl>().enabled = true;
                        cam.enabled = true;
                    }
                    else
                    {
                        camTarget.GetComponent<TankAttack>().enabled = true;
                        camTarget.GetComponent<TankControl>().enabled = true;
                        cam.enabled = true;
                    }
                }
            }
        }
    }



    public void ClearGame(int Point)
    {
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            curPoint += Point;
            ui = GameObject.Find("Canvas").GetComponent<UI>();
            ui.PointInit(curPoint, clearPoint);
            if (curPoint >= clearPoint)
            {
                Cursor.visible = true;
                Destroy(gameObject);
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
