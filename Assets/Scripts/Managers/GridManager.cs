using System.Collections;
using System.Collections.Generic;
using Datas;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int _columnLength, _rowLength;
    [SerializeField]
    private float _xSpace, _ySpace;
    
    
    // Start is called before the first frame update
    public void Init()
    {
        MapData mapdata = Managers.Data.mapDict[1];
        _columnLength = mapdata.gridMap.GetLength(1);
        _rowLength = mapdata.gridMap.GetLength(0);
        this.transform.position = new Vector3((float)-_columnLength / 2, (float)_rowLength / 2, 0);
        
        GridTile(mapdata);
        
        SetCharacter();

    }

    private void GridTile(MapData mapdata)
    {
        //DataManager -> GridManager
        for (int i = 0; i < _columnLength * _rowLength; i++)
        {
            int column = i % _columnLength;
            int row = i / _columnLength;
            GameObject go = Managers.Resource.Instantiate("Tile", this.transform.position + new Vector3(_xSpace / 2 +_xSpace * column,  -_ySpace / 2 - _ySpace * row), this.transform);
            if (mapdata.gridMap[row,column] > 0)
            {
                go.GetComponent<SpriteRenderer>().sprite =
                    Managers.Resource.LoadSprite($"MapTile/Tile{mapdata.gridMap[row,column].ToString()}");
            }
        }
    }

    private void SetCharacter()
    {
        foreach (var character in GameManager.Instance()._myCharacters)
        {
            Tile pos = character.GetComponent<CharacterController>()._pos;
            character.transform.position = this.transform.position +
                                           new Vector3(_xSpace / 2 + _xSpace * pos.Y, -_ySpace / 2 - _ySpace * pos.X);
        }

        foreach (var character in GameManager.Instance()._enemyCharacters)
        {
            Tile pos = character.GetComponent<CharacterController>()._pos;
            character.transform.position = this.transform.position +
                                           new Vector3(_xSpace / 2 + _xSpace * pos.Y, -_ySpace / 2 - _ySpace * pos.X);
            Vector3 scale = character.transform.localScale;
            // 적대 캐릭터 방향 전환
            scale.x = Mathf.Abs(scale.x);
            character.transform.localScale = scale;
        }
    }

    public void GetTile(float id)
    {
        Managers.Resource.LoadSprite($"Tile" + id);
    }
}
