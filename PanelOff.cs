using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOff : MonoBehaviour
{
    void Off()
    {
        transform.parent.gameObject.SetActive(false);
    }

}
