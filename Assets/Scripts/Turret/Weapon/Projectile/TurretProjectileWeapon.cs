using System.Collections.Generic;
using Enemy;
using Field;
using Runtime;
using UnityEngine;

namespace Turret.Weapon.Projectile
{
    public class TurretProjectileWeapon : ITurretWeapon
    {
        private TurretProjectileWeaponAsset m_Asset;
        private TurretView m_View;
        private float m_TimeBetweenShots;
        private float m_MaxDistance;

        private float m_LastShotTime = 0f;
        private List<Node> m_ReachableNodes;
        
        public TurretProjectileWeapon(TurretProjectileWeaponAsset mAsset, TurretView mView)
        {
            m_Asset = mAsset;
            m_View = mView;
            m_TimeBetweenShots = 1f / m_Asset.rateOfFire;
            m_MaxDistance = m_Asset.maxDistance;
            m_ReachableNodes = Game.SPLayer.grid.GetNodesInCircle(m_View.transform.position, m_MaxDistance);
        }

        public void TickShoot()
        {
            float passedTime = Time.time - m_LastShotTime;
            if (passedTime < m_TimeBetweenShots)
            {
                return;
            }

            EnemyData closetEnemyData =
                EnemySearch.GetClosestEnemy(m_View.transform.position, m_ReachableNodes);

            if (closetEnemyData == null)
            {
                return;
            }
            
            Shoot(closetEnemyData);
            m_LastShotTime = Time.time;
        }

        private void Shoot(EnemyData enemyData)
        {
            m_Asset.projectileAsset.CreateProjectile(m_View.MProjectileOrigin.position,
                m_View.MProjectileOrigin.forward, enemyData);
        }
    }
}