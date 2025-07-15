using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    bool collisionFlag;
    //public Transform[] blockPos;
    //public Transform[] sensorPoints;
    //LineRenderer linerend;

    // Start is called before the first frame update
    void Start()
    {
        //linerend = gameObject.AddComponent<LineRenderer>();

        //Vector3 pos1 = new Vector3(0, -3, 0);
        //Vector3 pos2 = new Vector3(0, 3, 0);

        // 線を引く場所を指定する
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (Transform pos in blockPos)
        //{
        //    Debug.Log(pos.position);
        //    Vector3 leftTopPos = pos.position + new Vector3(-0.25f, 0.25f, 0);
        //    Vector3 leftBtmPos = pos.position + new Vector3(-0.25f, -0.25f, 0);
        //    Vector3 rightTopPos = pos.position + new Vector3(0.25f, 0.25f, 0);
        //    Vector3 rightBtmPos = pos.position + new Vector3(0.25f, -0.25f, 0);
        //    Vector3[] array = {leftTopPos, leftBtmPos, rightTopPos, rightBtmPos};
        //    float max = array[0].x;
        //    float y = array[0].y;
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        if (max < array[i].x)
        //        {
        //            max = array[i].x;
        //            y = array[i].y;
        //        }
        //    }
        //}

        //foreach (Transform sensorPoint in sensorPoints)
        //{
        //    Debug.Log(sensorPoint.position);
        //    linerend.SetPosition(0, sensorPoint.position);
        //    linerend.SetPosition(1, sensorPoint.position + Vector3.down);
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameOverObject"))
        {
            GameController.gameState = GameState.gameover;
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Vegetable"))
        {
            collisionFlag = true;
        }
    }

    public bool CollisionCheck()
    {
        return collisionFlag;
    }
}
