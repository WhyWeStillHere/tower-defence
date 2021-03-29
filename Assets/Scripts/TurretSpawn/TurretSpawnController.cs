using Field;
using Runtime;
using Turret;
using UnityEngine;
using Grid = Field.Grid;

namespace TurretSpawn
{
    public class TurretSpawnController : IController
    {
        private Grid m_Grid;
        private TurretMarket m_Market;

        public TurretSpawnController(Grid mGrid, TurretMarket mMarket)
        {
            m_Grid = mGrid;
            m_Market = mMarket;
        }

        public void OnStart()
        {
            
        }

        public void OnStop()
        {
            
        }

        public void Tick()
        {
            if (m_Grid.HasSelectedNode() && Input.GetMouseButtonDown(0))
            {
                Node selectedNode = m_Grid.GetSelectedNode();

                if (selectedNode.m_IsOccupied /* && m_Grid.CanOccupy*/)
                {
                    return;
                }
                
                SpawnTurret(m_Market.ChosenTurret, selectedNode);
            }
        }

        private void SpawnTurret(TurretAsset asset, Node node)
        {
            TurretView view = Object.Instantiate(asset.viewPrefab);
            TurretData data = new TurretData(asset, node);
            
            data.AttachView(view);

            node.m_IsOccupied = true; // TryOccupy()
            m_Grid.UpdatePathFinding();
        }
    }
}

/*                if (!m_Grid.CanOccupy(m_Grid.GetNodeCoordinate(selectedNode)))
                {
                    return;
                }
                
                SpawnTurret(m_Market.ChosenTurret, selectedNode);
            }
        }

        private void SpawnTurret(TurretAsset asset, Node node)
        {
            TurretView view = Object.Instantiate(asset.viewPrefab);
            TurretData data = new TurretData(asset, node);
            
            data.AttachView(view);

            m_Grid.TryOccupyNode(m_Grid.GetNodeCoordinate(node));
        }*/