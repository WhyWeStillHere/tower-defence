using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(menuName = "Assets/Spawn Waves Asset", fileName = "Spawn Waves Asset")]
    public class SpawnWavesAsset : ScriptableObject
    {
        public EnemyAsset enemyAsset;
        public int count;
        public float timeBetweenSpawns;
    }
}