using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    HashSet<Collider2D> touchingObjects = new HashSet<Collider2D>();
    bool countFlag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingObjects.Count >= 1)
        {
            countFlag = true;
        }
        else
        {
            countFlag = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other is BoxCollider2D)
        {
            touchingObjects.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other is BoxCollider2D)
        {
            touchingObjects.Remove(other);
        }
    }

    public bool FlagCheck()
    {
        return countFlag;
    }
}
