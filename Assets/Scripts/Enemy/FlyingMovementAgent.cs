using Field;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class FlyingMovementAgent : IMovementAgent
    {
        private float m_Speed;
        private Transform m_Transform;

        public FlyingMovementAgent(float mSpeed, Transform mTransform, Grid grid)
        {
            m_Speed = mSpeed;
            m_Transform = mTransform;
            
            SetTargetNode(grid.GetTargetNode());
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
                m_TargetNode = null;
                return;
            }
        
            Vector3 dir = (target - m_Transform.position).normalized;
            Vector3 delta = dir * (m_Speed * Time.deltaTime);
            Vector3 validDelta = new Vector3(
                delta.x,
                0,
                delta.z
                );
            m_Transform.Translate(validDelta);
        }

        private void SetTargetNode(Node node)
        {
            m_TargetNode = node;
        }
    }
}