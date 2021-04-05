using System.Collections.Generic;
using Enemy;
using Field;
using Turret;
using Turret.Weapon;
using TurretSpawn;
using UnityEngine;
using Grid = Field.Grid;

namespace Runtime
{
    public class PLayer
    {
        private List<EnemyData> m_EnemyDatas = new List<EnemyData>();

        public IReadOnlyList<EnemyData> MEnemyDatas => m_EnemyDatas;
        private List<TurretData> m_TurretDatas = new List<TurretData>();
        public IReadOnlyList<TurretData> MTurretDatas => m_TurretDatas;

        public readonly GridHolder gridHolder;
        public readonly Grid grid;
        public readonly TurretMarket turretMarket;

        public PLayer()
        {
            gridHolder = Object.FindObjectOfType<GridHolder>();
            gridHolder.CreateGrid();
            grid = gridHolder.MGrid;

            turretMarket = new TurretMarket(Game.SCurrentLevel.TurretMarketAsset);
        }
        
        public void EnemySpawned(EnemyData data)
        {
            m_EnemyDatas.Add(data);
        }
        public void TurretSpawned(TurretData data)
        {
            m_TurretDatas.Add(data);
        }

    }
}