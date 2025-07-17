using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    private HashSet<Collider2D> touchingObjects = new HashSet<Collider2D>();

    bool flag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingObjects.Count >= 1)
        {
            flag = true;
            //effectFlag = true;

            //if (!effectFlag)
            //{
            //    particleSystem.Play();
            //    effectFlag = true;
            //}
        }
        else
        {
            flag = false;
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

    public bool Flag()
    {
        return flag;
    }
}
