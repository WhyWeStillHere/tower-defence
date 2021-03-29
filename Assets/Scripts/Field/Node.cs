using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Field
{
    public class Node
    {
        public Vector3 m_Position;

        public Node m_NextNode;
        public bool m_IsOccupied;
        public OccupationAvailability m_OccupationAvailability;
        public List<EnemyData> EnemyDatas;

        public float m_PathWeight;

        public Node(Vector3 mPosition)
        {
            m_Position = mPosition;
            m_OccupationAvailability = OccupationAvailability.Undefined;
            EnemyDatas = new List<EnemyData>();
        }
        
        public void ResetWeight()
        {
            m_PathWeight = float.MaxValue;
        }
    }
}