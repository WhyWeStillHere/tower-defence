using Assets;

namespace EnemySpawn
{
    [System.Serializable]
    public class SpawnWave
    {
        public EnemyAsset enemyAsset;
        public int count;
        public float timeBetweenSpawns;
        public float timeBeforeStartWave;
    }
}