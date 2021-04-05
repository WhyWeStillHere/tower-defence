using Enemy;
using Field;
using Runtime;
using UnityEngine;

namespace Turret.Weapon.Projectile.Rocket
{
    public class RocketProjectile : MonoBehaviour, IProjectile
    {
        private float m_Speed = 5f;
        private float m_Damage = 3f;
        private float m_BlastRadius = 20f;
        private bool m_DidHit = false;
        private EnemyData m_HitEnemy = null;
        private EnemyData m_Target = null;
        
        public void TickApproaching()
        {
            Vector3 direction;
            if (m_Target == null)
            {
                direction = transform.forward;
            }
            else
            {
                direction = (m_Target.MView.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
            transform.Translate(direction * (m_Speed * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            m_DidHit = true;
            if (other.CompareTag("Enemy"))
            {
                EnemyView enemyView = other.GetComponent<EnemyView>();
                if (enemyView != null)
                {
                    m_HitEnemy = enemyView.MData;
                }
            }
        }
        public void setTarget(EnemyData target)
        {
            m_Target = target;
        }
        
        public bool DidHit()
        {
            return m_DidHit;
        }

        public void DestroyProjectile()
        {
            if (m_HitEnemy != null)
            {
                foreach (Node node in Game.SPLayer.grid.GetNodesInCircle(m_HitEnemy.MView.transform.position, m_BlastRadius))
                {
                    foreach (EnemyData enemyData in node.EnemyDatas)
                    {
                        enemyData.GetDamage(m_Damage);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}