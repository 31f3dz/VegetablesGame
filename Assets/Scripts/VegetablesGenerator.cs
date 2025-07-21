using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

public class VegetablesGenerator : MonoBehaviour
{
    public GameObject[] vegetables;
    GameObject vegetable;
    public Transform droppedVegetables;
    public ColliderCheck[] colliderCheckList;
    public CameraScroll cameraScroll;
    bool scrollFlag;
    bool waitFlag;
    float defPos = 2.5f;

    int rand = -1;
    bool isMobileInput;
    bool objNxtFlag = true;
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

        Vector2 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(currentPos.x, -moveLimit, moveLimit);
        transform.position = currentPos;

        if (objNxtFlag)
        {
            rand = Random.Range(0, vegetables.Length);
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
            if (!isMobileInput)
            {
                axisH = Input.GetAxisRaw("Horizontal");
                axisV = Input.GetAxisRaw("Vertical");
            }

            vegetable.transform.Translate(new Vector2(axisH, 0) * moveSpeed * Time.deltaTime, Space.World);
            vegetable.transform.Rotate(0, 0, axisV * rotateSpeed * Time.deltaTime);
        }

        if (!waitFlag && vegetable.GetComponent<Vegetable>().CollisionCheck() && vegetable.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            vegetable.transform.SetParent(droppedVegetables);
            vegetable.tag = "droppedVegetable";

            foreach (ColliderCheck colliderCheck in colliderCheckList)
            {
                if (colliderCheck.FlagCheck() && !colliderCheck.EffectCheck())
                {
                    colliderCheck.EffectPlay();
                    GameController.stagePoints += 1000;
                }
            }

            if (!scrollFlag && cameraScroll.FlagCheck())
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
    }

    public int NextVegetable()
    {
        return rand;
    }

    public void MobileAxisH(float x)
    {
        axisH = x;

        if (axisH == 0)
        {
            isMobileInput = false;
        }
        else
        {
            isMobileInput = true;
        }
    }

    public void MobileAxisV(float y)
    {
        axisV = y;

        if (axisV == 0)
        {
            isMobileInput = false;
        }
        else
        {
            isMobileInput = true;
        }
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
        GameController.stagePoints += 1500;
        yield return new WaitForSeconds(0.5f);
        scrollFlag = false;
        waitFlag = false;
    }
}
