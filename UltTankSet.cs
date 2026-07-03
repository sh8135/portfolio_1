using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltTankSet : MonoBehaviour
{
    private float hitBoxTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("HitDelayCheck");
    }

    // Update is called once per frame
    void Update()
    {
        if(hitBoxTime < 0.0f)
        {
            transform.GetChild(0).parent = null;
            Destroy(gameObject);
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
        yield break;
    }
}
