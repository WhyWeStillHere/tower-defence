using EnemySpawn;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(menuName = "Assets/Spawn Waves Asset", fileName = "Spawn Waves Asset")]
    public class SpawnWavesAsset : ScriptableObject
    {
        public SpawnWave[] SpawnWaves;
    }
}