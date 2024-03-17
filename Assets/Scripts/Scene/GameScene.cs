using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField]
    private float time = 0.0f;
    [SerializeField]
    private float timeTerm = 1.0f;
    
    public override Define.SceneType _sceneType
    {
        get { return Define.SceneType.GameScene; }
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        if (timeTerm <= time)
        {
            time = 0.0f;
            GameManager.Instance().Action();
        }
    }

    protected override void Init()
    {
        base.Init();
        
        GameManager.Instance().SetTileMap();
        GridManager gridManager = GameObject.FindObjectOfType<GridManager>();
        gridManager.Init();
        
        GameManager.Instance().GameStart();
    }

    public override void Clear()
    {
        GameManager.Instance().Clear();
    }
}
