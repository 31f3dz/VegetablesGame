using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    bool triggerFlag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ColliderCheck"))
        {
            triggerFlag = true;
            //Debug.Log(triggerFlag);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ColliderCheck"))
        {
            triggerFlag = false;
            //Debug.Log(triggerFlag);
        }
    }

    public bool FlagCheck()
    {
        return triggerFlag;
    }
}
