using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public VegetablesGenerator vg;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    int currentPoint; // UIが管理しているポイント
    public TextMeshProUGUI pointText;

    int highScore;
    public TextMeshProUGUI highScorePoint;

    public float startTime = 180.0f; // カウントダウンの基準
    float displayTime; // UIと連動する残時間
    float pastTime; // 経過時間
    public TextMeshProUGUI timeText;

    public Image image;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("Score");
        highScorePoint.text = highScore.ToString();

        displayTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState == GameState.playing)
        {
            if (currentPoint != GameController.stagePoints)
            {
                currentPoint = GameController.stagePoints;
                pointText.text = currentPoint.ToString();
            }

            pastTime += Time.deltaTime;
            displayTime = startTime - pastTime;

            if (displayTime <= 0.0f)
            {
                displayTime = 0.0f;
                GameController.gameState = GameState.timeover;
            }

            timeText.text = Mathf.Ceil(displayTime).ToString();

            int rand = vg.NextVegetable();

            if (rand != -1 || rand != vg.NextVegetable())
            {
                image.sprite = sprites[rand];
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Pause();
            }
        }

        if (GameController.gameState == GameState.timeover || GameController.gameState == GameState.gameover)
        {
            if (GameController.stagePoints > highScore)
            {
                highScore = GameController.stagePoints;
                PlayerPrefs.SetInt("Score", highScore);
            }

            highScorePoint.text = highScore.ToString();
            resultPanel.SetActive(true);
        }
    }

    public void Pause()
    {
        if (resultPanel.activeSelf)
        {
            Time.timeScale = 1.0f;
            resultText.text = "Result";
            resultPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0.0f;
            resultText.text = "Pause";
            resultPanel.SetActive(true);
        }
    }
}
