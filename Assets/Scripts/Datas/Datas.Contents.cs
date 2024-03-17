using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Datas
{
    // save chracter class and start position
    [System.Serializable]
    public class Character
    {
        public Define.Class _class;
        public Tile pos;
    }
    
    // the data model for database
    [System.Serializable]
    public class MapDataDTO 
    {
        public int mapid;
        public GridRow[] gridMap;
        public List<Character> myCharacter;
        public List<Character> enemyCharacter;
        
        // DTO를 
        public MapData DtoToMap()
        {
            MapData map = new MapData()
            {
                mapid =  this.mapid,
                gridMap = toTwoDimArray(),
                myCharacter = this.myCharacter,
                enemyCharacter = this.enemyCharacter
            };
            return map;
        }
        
        public int[,] toTwoDimArray()
        {
            int col = gridMap.GetLength(0);
            int row = gridMap[0].rows.GetLength(0);
            int[,] twoDimArray = new int[col, row];
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    twoDimArray[i, j] = gridMap[i].rows[j];
                }
            }

            return twoDimArray;
        }
    }
    // Dictionary<mapid, mapDataDTo 변환
    public class MapDTODict : ILoader<int, MapDataDTO>
    {
        public List<MapDataDTO> mapdatadtos = new List<MapDataDTO>();
        public Dictionary<int, MapDataDTO> MakeDict()
        {
            Dictionary<int, MapDataDTO> dict = new Dictionary<int, MapDataDTO>();
            foreach (MapDataDTO data in mapdatadtos)
            {
                Debug.Log($"mapid : {data.mapid}");
                dict.Add(data.mapid, data);
            }
            return dict;
        }

        public void MakeDict(ref Dictionary<int, MapData> dict)
        {
            dict = new Dictionary<int, MapData>();
            foreach (MapDataDTO data in mapdatadtos)
            {
                dict.Add(data.mapid, data.DtoToMap());
            }
        }
    }
    // 1차원 배열 전용 클래스
    [System.Serializable]
    public class GridRow
    {
        public int[] rows;
    }
    
    public class MapData
    {
        public int mapid;
        public int[,] gridMap;
        public List<Character> myCharacter;
        public List<Character> enemyCharacter;
    }
    

    
}
