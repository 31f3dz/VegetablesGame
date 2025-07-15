using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetablesGenerator : MonoBehaviour
{
    public GameObject[] vegetables;
    GameObject vegetable;
    public Transform droppedVegetables;

    //float pastTime = 0;
    //public float delayTime = 4.0f;
    bool objGenFlag = true;
    bool objConFlag;

    float axisH;
    float axisV;
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 90.0f;
    float moveLimit = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState != GameState.playing) return;

        //pastTime += Time.deltaTime;

        //if (pastTime >= delayTime)
        //{
        //    pastTime = 0;
        //    objGenFlag = true;
        //}

        //axisH = Input.GetAxisRaw("Horizontal");
        //axisV = Input.GetAxisRaw("Vertical");
        //transform.Translate(new Vector2(axisH, 0) * moveSpeed * Time.deltaTime);

        Vector2 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(currentPos.x, -moveLimit, moveLimit);
        transform.position = currentPos;

        if (objGenFlag)
        {
            int rand = Random.Range(0, vegetables.Length);
            vegetable = Instantiate(vegetables[rand], transform.position, vegetables[rand].transform.rotation);
            vegetable.transform.SetParent(transform);

            objGenFlag = false;
            objConFlag = true;
        }

        if (objConFlag)
        {
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
            vegetable.transform.Translate(new Vector2(axisH, 0) * moveSpeed * Time.deltaTime, Space.World);
            vegetable.transform.Rotate(0, 0, axisV * rotateSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Space))
            {
                //vegetable.transform.SetParent(droppedVegetables);
                vegetable.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                //objConFlag = false;
                //Invoke("objDrop", 1);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                vegetable.GetComponent<Rigidbody2D>().gravityScale = 0.05f;
            }
        }

        if (vegetable.GetComponent<Vegetable>().CollisionCheck() && vegetable.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            vegetable.transform.SetParent(droppedVegetables);

            objGenFlag = true;
            objConFlag = false;

            //Invoke("objDrop", 3.0f);
        }

        //if (vegetable.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        //{
        //    Debug.Log("止まった");
        //}
    }

    void objDrop()
    {
        objGenFlag = true;
        objConFlag = false;
    }
}
