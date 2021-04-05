using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using Runtime;
using Turret.Weapon.Lazer;
using UnityEngine;

namespace Turret.Weapon.Field
{
    public class TurretFieldWeapon : ITurretWeapon
    {
        private TurretFieldWeaponAsset m_Asset;
        private TurretView m_View;
        private GameObject m_FieldObject;
        [CanBeNull] private EnemyData m_ClosestEnemyData;
        private float m_Radius;
        private float m_Damage;
        private List<Node> m_ReachableNodes;

        public TurretFieldWeapon(
            TurretFieldWeaponAsset asset,
            TurretView turretView)
        {
            m_Asset = asset;
            m_View = turretView;
            m_Damage = m_Asset.damage;
            m_Radius = m_Asset.radius;
            m_FieldObject = Object.Instantiate(m_Asset.fieldObject, m_View.transform);
            
            // Still doesn't match with radius 
            m_FieldObject.transform.localScale = new Vector3(m_Radius, m_Radius, m_Radius);
            
            m_ReachableNodes = Game.SPLayer.grid.GetNodesInCircle(m_View.transform.position, m_Radius);
        }

        public void TickShoot()
        {
            m_ClosestEnemyData =
                EnemySearch.GetClosestEnemy(m_View.transform.position, m_ReachableNodes);
            if (m_ClosestEnemyData == null)
            {
                m_FieldObject.gameObject.SetActive(false);
            }
            else
            {
                m_FieldObject.gameObject.SetActive(true);
                foreach (Node node in m_ReachableNodes)
                {
                    foreach (EnemyData enemyData in node.EnemyDatas)
                    {
                        enemyData.GetDamage(m_Damage * Time.deltaTime);
                    }
                }
            }
        }
    }
}