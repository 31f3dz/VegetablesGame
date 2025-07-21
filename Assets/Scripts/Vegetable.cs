using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    bool collisionFlag;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameOverObject"))
        {
            if (GameController.gameState == GameState.playing)
            {
                GameController.gameState = GameState.gameover;
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("droppedVegetable"))
        {
            collisionFlag = true;
        }
    }

    public bool CollisionCheck()
    {
        return collisionFlag;
    }
}
