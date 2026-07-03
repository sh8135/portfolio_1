using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    private Transform mainCam;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnCamera = Instantiate(cam, transform.position , transform.rotation);
        GameObject spawnPlayer = Instantiate(player, transform.position , transform.rotation);

        mainCam = spawnCamera.transform;
        CameraSetting camSet = mainCam.GetComponent<CameraSetting>();
		if(mainCam && camSet){
            camSet.target = spawnPlayer.transform;
		}
    }
}
