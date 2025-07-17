using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public float startTime = 180.0f; // カウントダウンの基準
    public float displayTime; // UIと連動する残時間
    float pastTime; // 経過時間
    //bool isTimeOver; // カウントが0になったかどうか ※0なら止める
    public TextMeshProUGUI timeText;

    int currentPoint; // UIが管理しているポイント
    public TextMeshProUGUI pointText;

    // Start is called before the first frame update
    void Start()
    {
        displayTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isTimeOver)
        //{
        //    return;
        //}

        if (GameController.gameState == GameState.playing)
        {
            pastTime += Time.deltaTime;

            displayTime = startTime - pastTime;

            if (displayTime <= 0)
            {
                displayTime = 0;
                //isTimeOver = true;
                GameController.gameState = GameState.timeover;
            }

            timeText.text = Mathf.Ceil(displayTime).ToString();

            if (currentPoint != GameController.stagePoints)
            {
                currentPoint = GameController.stagePoints;
                pointText.text = currentPoint.ToString();
            }
        }

        if (GameController.gameState == GameState.timeover)
        {
            resultText.text = "TimeUp";
            resultPanel.SetActive(true);
        }

        if (GameController.gameState == GameState.gameover)
        {
            resultText.text = "GameOver";
            resultPanel.SetActive(true);
        }
    }
}
