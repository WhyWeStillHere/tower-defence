using Field;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class GridMovementAgent : IMovementAgent
    {
        private float m_Speed;
        private Transform m_Transform;
        private EnemyData m_EnemyData;
        private Grid m_Grid;
        private Node currentNode;

        public GridMovementAgent(float mSpeed, Transform mTransform, Grid grid, EnemyData mEnemyData)
        {
            m_Speed = mSpeed;
            m_Transform = mTransform;
            m_EnemyData = mEnemyData;
            m_Grid = grid;
            
            SetTargetNode(m_Grid.GetStartNode());
            Node startNode = m_Grid.GetNodeAtPoint(m_Transform.position);
            currentNode = startNode;
            if (currentNode != null)
            {
                currentNode.EnemyDatas.Add(m_EnemyData);
            }
        }

        private const float TOLERANCE = 0.1f;

        private Node m_TargetNode; 
        public void TickMovement()
        {
            if (m_TargetNode == null)
            {
                return;
            }

            Vector3 target = m_TargetNode.m_Position;
            
            float distance = (target - m_Transform.position).magnitude;
            if (distance < TOLERANCE)
            {
                m_TargetNode = m_TargetNode.m_NextNode;
                return;
            }
        
            Vector3 dir = (target - m_Transform.position).normalized;
            Vector3 delta = dir * (m_Speed * Time.deltaTime);
            
            m_Transform.Translate(delta);
            if (currentNode != null)
            {
                currentNode.EnemyDatas.Remove(m_EnemyData);
            }
            currentNode = m_Grid.GetNodeAtPoint(m_Transform.position);
            if (currentNode != null)
            {
                currentNode.EnemyDatas.Add(m_EnemyData);
            }
        }

        private void SetTargetNode(Node node)
        {
            m_TargetNode = node;
        }
    }
}