﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    private bool needDestroy = false;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void UpdateScoreDisplay(PaddleController paddle)
    {

        Canvas can = null;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.name == "UICanvas") can = c;
        }

        foreach (Text t in can.GetComponentsInChildren<Text>())
        {

            if (t.name == "StockPrice")
            {
                t.text = "£ " +((int)paddle.StockPrice).ToString();
            }
        }
    }

    public static void UpdateFundsDisplay(PaddleController paddle)
    {
        Canvas can = null;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.name == "UICanvas") can = c;
        }

        foreach (Text t in can.GetComponentsInChildren<Text>())
        {

            if (t.name == "FundsAmt")
            {
                t.text = "£ " + ((int)paddle.Funds).ToString();
            }
        }
    }

    public static void UpdatePubOpinDisplay(PaddleController paddle)
    {
        Canvas can = null;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.name == "UICanvas") can = c;
        }

        //TODO implement public opinion UI display. Should be a bar of health
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
            foreach (Canvas c in FindObjectsOfType<Canvas>())
            {
                if (c.name == "UICanvas") can = c;
            }

            foreach (Text t in can.GetComponentsInChildren<Text>())
            {
                if (t.name == "StockText")
                {
                    t.text = "Final Stock Price:";
                }
            }

            AudioClip gameOver = Resources.Load<AudioClip>("Sound/Game Over Theme");
            Debug.Log(gameOver);
            AudioSource.PlayClipAtPoint(gameOver, Vector3.zero, 1.0f);
        }
        else
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }
}
