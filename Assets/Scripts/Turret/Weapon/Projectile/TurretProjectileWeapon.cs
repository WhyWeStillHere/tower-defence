using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using Runtime;
using UnityEngine;

namespace Turret.Weapon.Projectile
{
    public class TurretProjectileWeapon : ITurretWeapon
    {
        private TurretProjectileWeaponAsset m_Asset;
        private TurretView m_View;
        [CanBeNull]
        private EnemyData m_ClosestEnemyData;

        private List<IProjectile> m_Projeciles = new List<IProjectile>();
        
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
            TickWeapon();
            TickTower();
            TickProjectiles();
        }

        private void TickTower()
        {
            if (m_ClosestEnemyData != null)
            {
                m_View.TowerLookAt(m_ClosestEnemyData.MView.transform.position);
            }
        }

        private void TickProjectiles()
        {
            for (var i = 0; i < m_Projeciles.Count; i++)
            {
                IProjectile projectile = m_Projeciles[i];
                projectile.TickApproaching();
                if (projectile.DidHit())
                {
                    projectile.DestroyProjectile();
                    m_Projeciles[i] = null;
                }
            }

            m_Projeciles.RemoveAll(projectile => projectile == null);
        }

        private void TickWeapon()
        {
            float passedTime = Time.time - m_LastShotTime;
            if (passedTime < m_TimeBetweenShots)
            {
                return;
            }

            m_ClosestEnemyData =
                EnemySearch.GetClosestEnemy(m_View.transform.position, m_ReachableNodes);

            if (m_ClosestEnemyData == null)
            {
                return;
            }
            
            TickTower();
            
            Shoot(m_ClosestEnemyData);
            m_LastShotTime = Time.time;
        }

        private void Shoot(EnemyData enemyData)
        {
            m_Projeciles.Add(m_Asset.projectileAsset.CreateProjectile(m_View.MProjectileOrigin.position,
                m_View.MProjectileOrigin.forward, enemyData));
        }
    }
}