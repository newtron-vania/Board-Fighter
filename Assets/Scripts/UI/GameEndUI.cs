using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField] 
    private TextMeshProUGUI text;

    void Start()
    {
        if (GameManager.Instance()._myCharacters.Count == 0)
        {
            text.text = "GameOver";
        }
        else
        {
            text.text = "Victory";
        }
        button.onClick.AddListener(RestartGame);
    }

    public void RestartGame()
    {
        Managers.GameTime = 0.0f;
        Managers.Scene.LoadScene(Define.SceneType.GameScene);
    }
}
