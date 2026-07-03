using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject titlePanel;
    public GameObject PointPanel;
    public TextMeshProUGUI pointText;
    public Image mapImage;
    public List<Sprite> mapList;
    public GameObject Tutorial;

    private int curMap = 0;

    //public GameObject menuPanel;

    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            //menuPanel = GameObject.Find("Canvas").transform.Find("Menu").gameObject;
            pointText.text = gameManager.curPoint + " / " + gameManager.clearPoint;
        }/*
        else if (SceneManager.GetActiveScene().name == "Training")
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }*/
    }

    public void StartButton()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(mapList[curMap].name);
    }

    public void NextMap()
    {
        if(mapList.Count - 1 > curMap)
            curMap++;
        else
            curMap = 0;
        mapImage.sprite = mapList[curMap];

        if(curMap == 1)
        {
            PointPanel.SetActive(true);
        }
        else
        {
            PointPanel.SetActive(false);
        }
    }

    public void BackMap()
    {
        if (0 < curMap)
            curMap--;
        else
            curMap = mapList.Count - 1;
        mapImage.sprite = mapList[curMap];

        if (curMap == 1)
        {
            PointPanel.SetActive(true);
        }
        else
        {
            PointPanel.SetActive(false);
        }
    }

    public void MenuButton()
    {
        Destroy(gameManager.gameObject);
        SceneManager.LoadScene("Menu");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void PointUp()
    {
        if (gameManager.clearPoint < 15)
        {
            gameManager.clearPoint += 1;
            pointText.text = gameManager.clearPoint.ToString();
        }
    }

    public void PointDown()
    {
        if (gameManager.clearPoint > 1)
        {
            gameManager.clearPoint -= 1;
            pointText.text = gameManager.clearPoint.ToString();
        }
    }

    public void MapSetOn()
    {
        titlePanel.SetActive(false);
        mapPanel.SetActive(true);
        pointText.text = gameManager.clearPoint.ToString();
        mapImage.sprite = mapList[curMap];
    }

    public void MapSetOff()
    {
        mapPanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    public void TutorialSetOn()
    {
        titlePanel.SetActive(false);
        Tutorial.SetActive(true);
    }

    public void TutorialSetOff()
    {
        Tutorial.SetActive(false);
        titlePanel.SetActive(true);
    }

    public void MenuOff()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.menuPanel.SetActive(false);
        //gameManager.menuOn = false;
        Cursor.visible = false;
        if (gameManager.camTarget.CompareTag("Player"))
        {
            gameManager.camTarget.GetComponent<PlayerAttack>().enabled = true;
            gameManager.camTarget.GetComponent<PlayerControl>().enabled = true;
            gameManager.cam.enabled = true;
        }
        else
        {
            gameManager.camTarget.GetComponent<TankAttack>().enabled = true;
            gameManager.camTarget.GetComponent<TankControl>().enabled = true;
            gameManager.cam.enabled = true;
        }
    }

    public void PointInit(int curP, int clearP)
    {/*
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            //menuPanel = GameObject.Find("Canvas").transform.Find("Menu").gameObject;
            pointText.text = gameManager.curPoint + " / " + gameManager.clearPoint;
        }*/

        pointText.text = curP + " / " + clearP;
    }
}


