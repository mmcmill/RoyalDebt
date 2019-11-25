using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    private bool needDestroy = false;
    private static int stockPrice;
    private static int funds = 1000;
    private static int pubOpin = 100;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void UpdateScoreDisplay(PaddleController paddle)
    {
        stockPrice = (int)paddle.StockPrice;
        Canvas can = null;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.name == "UICanvas") can = c;
        }

        foreach (Text t in can.GetComponentsInChildren<Text>())
        {

            if (t.name == "StockPrice")
            {
                t.text = "£ " + stockPrice.ToString();
            }
        }
    }

    public static void UpdateFundsDisplay(PaddleController paddle)
    {
        funds = paddle.Funds;
        pubOpin = paddle.PublicOpinion;
        Canvas can = null;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.name == "UICanvas") can = c;
        }

        foreach (Text t in can.GetComponentsInChildren<Text>())
        {

            if (t.name == "FundsAmt")
            {
                t.text = "£ " + funds.ToString();
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene" && needDestroy)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
            
        }
        if (scene.name == "GameOver")
        {
            needDestroy = true;
            Canvas can = null;
            Canvas gameOverCan = null;
            foreach (Canvas c in FindObjectsOfType<Canvas>())
            {
                if (c.name == "UICanvas") can = c;
                if (c.name == "GameOverCan") gameOverCan = c; 
            }

            foreach (Text t in can.GetComponentsInChildren<Text>())
            {
                if (t.name == "StockText")
                {
                    t.text = "Final Stock Price:";
                }
            }

            foreach (Text t in gameOverCan.GetComponentsInChildren<Text>())
            {
                if (t.name == "GameOverInfo")
                {
                    t.text = "The South Sea Company has gone under due to ";
                    if (ScoreDisplay.pubOpin <= 0)
                    {
                        t.text += "failing public opinion.";
                    }
                    else if (ScoreDisplay.funds <= 0)
                    {
                        t.text += "the lack of personal funds to bribe with.";
                    }
                    else
                    {
                        t.text += "failure to hype up the stock price.";
                    }

                }
            }
        }
        else
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }
}
