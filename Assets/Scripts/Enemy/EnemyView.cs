using Assets;
using NUnit.Framework.Constraints;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        private EnemyData m_Data;
        private IMovementAgent m_MovementAgent;
        private static readonly int Dead = Animator.StringToHash("Dead");

        public EnemyData MData => m_Data;

        public IMovementAgent MMovementAgent => m_MovementAgent;

        public void AttachDate(EnemyData data)
        {
            m_Data = data;
        }

        public void CreateMovementAgent(Grid grid)
        {
            if (m_Data.MAsset.isFlyingEnemy)
            {
                m_MovementAgent = new FlyingMovementAgent(10f, transform, grid, m_Data);
            }
            else
            {
                m_MovementAgent = new GridMovementAgent(m_Data.MAsset.speed, transform, grid, m_Data);
            }
        }

        public void Die()
        {
            m_MovementAgent.Die();
            m_Animator.SetTrigger(Dead);
        }
    }
}