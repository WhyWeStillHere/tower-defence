using Assets;
using NUnit.Framework.Constraints;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyData m_Data;
        private IMovementAgent m_MovementAgent;

        public EnemyData MData => m_Data;

        public IMovementAgent MMovementAgent => m_MovementAgent;

        public void AttachDate(EnemyData data)
        {
            m_Data = data;
        }

        public void CreateMovementAgent(Grid grid)
        {
            if (m_Data.m_Asset.isFlyingEnemy)
            {
                m_MovementAgent = new FlyingMovementAgent(10f, transform, grid, m_Data);
            }
            else
            {
                m_MovementAgent = new GridMovementAgent(5f, transform, grid, m_Data);
            }
        }
    }
}