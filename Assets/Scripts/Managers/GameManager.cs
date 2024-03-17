using System.Collections;
using System.Collections.Generic;
using Datas;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager
{
    #region Singleton

    private static class InstanceSingleton
    {
        public readonly static GameManager Instance = new GameManager();
    }

    public static GameManager Instance()
    {
        return InstanceSingleton.Instance;
    }
    #endregion

    // 불러올 타일맵 ID
    private int _mapid = 1;
    public int MapId
    {
        get { return _mapid; }
        set { _mapid = value; }
    }
    
    // 타일맵 저장
    private Tile[,] _tileMap;
    public Tile[,] GetMap
    {
        get { return _tileMap; }
    }

    // 진영 별로 캐릭터 저장
    public HashSet<GameObject> _myCharacters = new HashSet<GameObject>();
    public HashSet<GameObject> _enemyCharacters = new HashSet<GameObject>();
    
    public static bool gameStop = false;

    public void SetTileMap()
    {
        MapData maps = Managers.Data.mapDict[_mapid];
        _tileMap = new Tile[maps.gridMap.GetLength(0), maps.gridMap.GetLength(1)];
        for (int i = 0; i < maps.gridMap.GetLength(0); i++)
        {
            for (int j = 0; j < maps.gridMap.GetLength(1); j++)
            {
                _tileMap[i, j] = new Tile(i, j);
                if (maps.gridMap[i, j] == 0)
                {
                    _tileMap[i, j].hasObstacle = true;
                }
            }
        }

        foreach (var character in maps.myCharacter)
        {
            SetCharacters(character, false);
        }
        foreach (var character in maps.enemyCharacter)
        {
            SetCharacters(character, true);
        }
    }

    public void SetCharacters(Datas.Character character, bool enemy)
    {
        GameObject go = null;
        switch (character._class)
        {
            case Define.Class.Near:
                go = Managers.Resource.Instantiate("Soldier");
                break;
            case Define.Class.Range:
                go = Managers.Resource.Instantiate("Priest");
                break;
            case Define.Class.Splash:
                go = Managers.Resource.Instantiate("Knight");
                break;
        }
        go.GetComponent<CharacterController>()._pos = character.pos;
        _tileMap[character.pos.X, character.pos.Y].hasObstacle = true;
        Debug.Log($"Character Pos : {go.GetComponent<CharacterController>()._pos.X} , {go.GetComponent<CharacterController>()._pos.Y}");
        if (enemy)
        {
            go.GetComponent<CharacterController>()._worldObject = Define.WorldObject.Enemy;
            go.GetComponent<SpriteRenderer>().color = Color.red;
            _enemyCharacters.Add(go);
        }
        else
        {
            go.GetComponent<CharacterController>()._worldObject = Define.WorldObject.Player;
            _myCharacters.Add(go);
        }
    }

    // 캐릭터별 Action 실시
    // 두 진영 중 하나의 진영의 캐릭터가 없을 경우 게임 종료
    public void Action()
    {
        Debug.Log("Action!");
        foreach (var myCharacter in _myCharacters)
        {
            myCharacter.GetComponent<CharacterController>().Action(_enemyCharacters);
        }
        
        foreach (var mEnemyCharacter in _enemyCharacters)
        {
            mEnemyCharacter.GetComponent<CharacterController>().Action(_myCharacters);
        }
        if (_myCharacters.Count <= 0 || _enemyCharacters.Count <= 0)
        {
            GameEndUI ui = GameObject.FindObjectOfType<Canvas>().GetComponentInChildren<GameEndUI>(true);
            ui.gameObject.SetActive(true);
            GamePause();
        }

    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        Define.WorldObject type = go.GetComponent<CharacterController>()._worldObject;

        if(go == null)
            return Define.WorldObject.Unknown;

        return type;

    }
    public void Despawn(GameObject go, float time = 0)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Player:
                if (_myCharacters.Contains(go))
                {
                    _myCharacters.Remove(go);
                    Tile pos = go.GetComponent<CharacterController>()._pos;
                    _tileMap[pos.X, pos.Y].hasObstacle = false;
                }
                break;
            case Define.WorldObject.Enemy:
                if (_enemyCharacters.Contains(go))
                {
                    _enemyCharacters.Remove(go);
                    Tile pos = go.GetComponent<CharacterController>()._pos;
                    _tileMap[pos.X, pos.Y].hasObstacle = false;
                }
                break;
        }
        Managers.Resource.Destroy(go, time);
    }
    
    public void GameStart()
    {
        Time.timeScale = 1;
        gameStop = false;
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        gameStop = true;
    }

    public void Clear()
    {
        _myCharacters.Clear();
        _enemyCharacters.Clear();
        _tileMap = null;
    }
}
