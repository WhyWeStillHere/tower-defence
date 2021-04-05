using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using Runtime;
using Turret.Weapon.Projectile;
using UnityEngine;

namespace Turret.Weapon.Lazer
{
    public class TurretLazerWeapon : ITurretWeapon
    {
        private TurretLazerWeaponAsset m_Asset;
        private TurretView m_View;
        private LineRenderer m_LineRenderer;
        [CanBeNull] private EnemyData m_ClosestEnemyData;
        private float m_MaxDistance;
        private float m_Damage;
        private List<Node> m_ReachableNodes;

        public TurretLazerWeapon(
            TurretLazerWeaponAsset asset,
            TurretView turretView)
        {
            m_Asset = asset;
            m_View = turretView;
            m_Damage = m_Asset.damage;
            m_MaxDistance = m_Asset.maxDistance;
            m_LineRenderer = Object.Instantiate(m_Asset.LineRenderPrefab, m_View.transform);
            m_ReachableNodes = Game.SPLayer.grid.GetNodesInCircle(m_View.transform.position, m_MaxDistance);
        }

        public void TickShoot()
        {
            m_ClosestEnemyData =
                EnemySearch.GetClosestEnemy(m_View.transform.position, m_ReachableNodes);
            if (m_ClosestEnemyData == null)
            {
                m_LineRenderer.gameObject.SetActive(false);
            }
            else
            {
                m_LineRenderer.gameObject.SetActive(true);
                m_LineRenderer.SetPositions(new Vector3[] {m_View.transform.position, m_ClosestEnemyData.MView.transform.position});
                m_ClosestEnemyData.GetDamage(m_Damage * Time.deltaTime);
            }
        }
    }
}