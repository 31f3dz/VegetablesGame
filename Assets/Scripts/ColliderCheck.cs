using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    HashSet<Collider2D> touchingObjects = new HashSet<Collider2D>();
    bool countFlag;

    public ParticleSystem particleSystem;
    bool effectFlag;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (touchingObjects.Count >= 9)
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
        if (other is CircleCollider2D)
        {
            touchingObjects.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other is CircleCollider2D)
        {
            touchingObjects.Remove(other);
        }
    }

    public bool FlagCheck()
    {
        return countFlag;
    }

    public int TouchCheck()
    {
        return touchingObjects.Count;
    }

    public bool EffectCheck()
    {
        return effectFlag;
    }

    public void EffectPlay()
    {
        particleSystem.Play();
        effectFlag = true;
    }
}
