using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using UnityEngine;

namespace Turret.Weapon
{
    public static class EnemySearch
    {
        [CanBeNull]
        public static EnemyData GetClosestEnemy(Vector3 center, List<Node> nodes)
        {
            float? smallestSqrDistance = null;
            EnemyData closestEnemy = null;
            foreach (Node node in nodes)
            {
                foreach (EnemyData enemyData in node.EnemyDatas)
                {
                    float currentSqrDistance = (center - enemyData.MView.transform.position).sqrMagnitude;
                    if (smallestSqrDistance == null || currentSqrDistance < smallestSqrDistance)
                    {
                        smallestSqrDistance = currentSqrDistance;
                        closestEnemy = enemyData;
                    }
                }
            }

            return closestEnemy;
        }
    }
}