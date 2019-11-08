using System.Collections;
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
    }

    void OnEnable()
    {
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

            if (t.name == "ScoreNum")
            {
                t.text = ((int)paddle.Score).ToString();
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene" && needDestroy)
        {
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
                if (t.name == "ScoreText")
                {
                    t.text = "Final Score:";
                }
            }
        }
    }
}
