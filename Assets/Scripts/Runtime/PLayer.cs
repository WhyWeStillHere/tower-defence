using System.Collections.Generic;
using Enemy;
using Field;
using UnityEngine;
using Grid = Field.Grid;

namespace Runtime
{
    public class PLayer
    {
        private List<EnemyData> m_EnemyDatas = new List<EnemyData>();

        public IReadOnlyList<EnemyData> MEnemyDatas => m_EnemyDatas;

        public readonly GridHolder gridHolder;
        public readonly Grid grid;

        public PLayer()
        {
            gridHolder = Object.FindObjectOfType<GridHolder>();
            gridHolder.CreateGrid();
            grid = gridHolder.MGrid;
        }
        
        public void EnemySpawned(EnemyData data)
        {
            m_EnemyDatas.Add(data);
        }
    }
}