using Assets;
using UnityEngine;

namespace Enemy
{
    public class EnemyData
    {
        private EnemyView m_View;
        private float m_Health;

        public EnemyView MView => m_View;

        public readonly EnemyAsset m_Asset;

        public EnemyData(EnemyAsset asset)
        {
            m_Asset = asset;
            m_Health = m_Asset.startHealth;
        }

        public void AttachView(EnemyView view)
        {
            m_View = view;
            m_View.AttachDate(this);
        }

        public void GetDamage(float damage)
        {
            m_Health -= damage;
            if (m_Health < 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Die");
        }
    }
}