using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class VegetablesGenerator : MonoBehaviour
{
    public GameObject[] vegetables;
    GameObject vegetable;
    public Transform droppedVegetables;
    public ColliderCheck[] colliderCheckList;
    //GameObject[] colliderCheckList;
    public CameraScroll cameraScroll;
    bool scrollFlag;
    bool waitFlag;
    float defPos = 2.5f;

    //float pastTime = 0;
    //public float delayTime = 4.0f;
    int rand;
    bool objNxtFlag = true;
    bool objGenFlag = true;
    bool objConFlag;

    float axisH;
    float axisV;
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 90.0f;
    float moveLimit = 2.5f;

    //public static bool stoppedFlag;

    // Start is called before the first frame update
    void Start()
    {
        //colliderCheckList = GameObject.FindGameObjectsWithTag("ColliderCheck");
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

        if (objNxtFlag)
        {
            rand = Random.Range(0, vegetables.Length);
            Debug.Log(vegetables[rand].name);
            objNxtFlag = false;
        }

        if (objGenFlag)
        {
            vegetable = Instantiate(vegetables[rand], transform.position, vegetables[rand].transform.rotation);
            vegetable.transform.SetParent(transform);

            objNxtFlag = true;
            objGenFlag = false;
            objConFlag = true;
        }

        if (objConFlag)
        {
            //stoppedFlag = false;

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

        if (!waitFlag && vegetable.GetComponent<Vegetable>().CollisionCheck() && vegetable.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            vegetable.transform.SetParent(droppedVegetables);
            vegetable.tag = "droppedVegetable";

            //objGenFlag = true;
            //objConFlag = false;

            //Invoke("objDrop", 3.0f);
            //stoppedFlag = true;
            //Debug.Log("1段目は" + colliderCheckList[0].Function());
            //Debug.Log("2段目は" + colliderCheckList[1].Function());

            //int i = 9;
            //foreach (GameObject colliderCheck in colliderCheckList)
            //{
            //    Debug.Log(i + "段目は" + colliderCheck.GetComponent<ColliderCheck>().Function());
            //    i--;
            //}
            foreach (ColliderCheck colliderCheck in colliderCheckList)
            {
                //if (!colliderCheck.FlagCheck())
                //{
                //    Debug.Log(i + "段目は" + colliderCheck.TouchCheck());
                //}
                //i--;

                if (colliderCheck.FlagCheck() && !colliderCheck.EffectCheck())
                {
                    colliderCheck.gameObject.GetComponent<ColliderCheck>().EffectPlay();
                    GameController.stagePoints += 1000;
                }
            }

            if (!scrollFlag && cameraScroll.Flag())
            {
                scrollFlag = true;
                waitFlag = true;
                StartCoroutine(Coroutine());
            }
            else
            {
                objDrop();
            }
        }

        //if (scrollFlag)
        //{
        //    float currentY = Camera.main.transform.position.y;
        //    if (currentY < 2.5f)
        //    {
        //        currentY += 5.0f * Time.deltaTime;
        //        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, currentY, Camera.main.transform.position.z);
        //    }
        //    else
        //    {
        //        transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
        //        scrollFlag = false;
        //    }
        //}

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

    IEnumerator Coroutine()
    {
        float defaultY = Camera.main.transform.position.y;
        float currentY = defaultY;
        while (currentY < defaultY + defPos)
        {
            currentY += 5.0f * Time.deltaTime;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, currentY, Camera.main.transform.position.z);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + defPos, transform.position.z);
        cameraScroll.gameObject.transform.position = new Vector3(cameraScroll.gameObject.transform.position.x, cameraScroll.gameObject.transform.position.y + defPos, cameraScroll.gameObject.transform.position.z);
        GameController.stagePoints += 1000;
        yield return new WaitForSeconds(0.5f);
        scrollFlag = false;
        waitFlag = false;
    }
}
