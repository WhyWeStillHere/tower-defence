using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "Assets/Enemy Asset", fileName = "Enemy Asset")]
    public class EnemyAsset : ScriptableObject
    {
        public float startHealth;
        public bool isFlyingEnemy;

        public EnemyView viewPrefab;
    }
}