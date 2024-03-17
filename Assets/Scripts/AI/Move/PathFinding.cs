using System.Collections.Generic;

namespace AI
{
    // 최단거리 탐색방식 정의 인터페이스
    public interface PathFinding
    {
        // 경로 탐색 함수
        public List<Tile> GetNextPath(Tile start, Tile target, bool ignoreObstacle = false);
        // 경로 탐색용 2차원 타일 배열 생성
        public Tile[,] InitializeGrid();
    }
}