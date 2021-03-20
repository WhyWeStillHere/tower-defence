using Enemy;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(menuName = "Assets/Enemy Asset", fileName = "Enemy Asset")]
    public class EnemyAsset : ScriptableObject
    {
        public int startHealth;
        public bool isFlyingEnemy;

        public EnemyView viewPrefab;
    }
}