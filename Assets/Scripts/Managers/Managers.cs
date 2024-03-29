using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    public static Managers Instance { get { Init(); return _instance; } }


    #region core

    private ResourceManager _resource = new ResourceManager();
    private DataManager _data = new DataManager();
    private SceneManagerEx _scene = new SceneManagerEx();


    public static ResourceManager Resource { get { return Instance._resource; } }
    public static DataManager Data{ get { return Instance._data; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }

    #endregion

    public static float GameTime { get; set; } = 0;

    void Awake()
    {
        Init();
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null) 
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();
            _instance._data.Init();

        }
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
    }

    public static void Clear()
    {

    }



}
