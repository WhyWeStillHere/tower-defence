using UnityEngine;

namespace Turret
{
    public class TurretView : MonoBehaviour
    {
        [SerializeField] private Transform m_ProjectileOrigin;
        [SerializeField] private Transform m_Tower;
        public Transform MProjectileOrigin => m_ProjectileOrigin;

        private TurretData m_Data;

        public TurretData MData => m_Data;

        public void AttachData(TurretData data)
        {
            m_Data = data;
            transform.position = m_Data.MNode.m_Position;
        }

        public void TowerLookAt(Vector3 point)
        {
            point.y = m_Tower.position.y;
            m_Tower.LookAt(point);
        }
    }
}