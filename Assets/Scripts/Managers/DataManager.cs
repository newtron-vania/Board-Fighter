using System;
using System.Collections;
using System.Collections.Generic;
using Datas;
using Unity.VisualScripting;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Datas.MapData> mapDict = new Dictionary<int, Datas.MapData>();
    public void Init()
    {
        SetMapDatas();
    }

    public void SetMapDatas()
    {
        LoadJson<MapDTODict, int, MapDataDTO>("MapData").MakeDict(ref mapDict);
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Json/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

}