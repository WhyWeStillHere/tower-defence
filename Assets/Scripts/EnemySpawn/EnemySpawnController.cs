using Assets;
using Enemy;
using Runtime;
using UnityEngine;
using Grid = Field.Grid;

namespace EnemySpawn
{
    public class EnemySpawnController : IController
    {
        private SpawnWavesAsset m_SpawnWaves;
        private Grid m_Grid;

        private float m_SpawnStartTime;
        private float m_PassedTimeAtPreviousFrame = -1f;
        
        public EnemySpawnController(SpawnWavesAsset mSpawnWaves, Grid mGrid)
        {
            m_SpawnWaves = mSpawnWaves;
            m_Grid = mGrid;
        }

        public void OnStart()
        {
            m_SpawnStartTime = Time.time;
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            float passedTime = Time.time - m_SpawnStartTime;
            float timeToSpawn = 0f;

            foreach (SpawnWave wave in m_SpawnWaves.SpawnWaves)
            {
                timeToSpawn += wave.timeBeforeStartWave;

                for (int i = 0; i < wave.count; i++)
                {
                    if (passedTime >= timeToSpawn && m_PassedTimeAtPreviousFrame < timeToSpawn)
                    {
                        SpawnEnemy(wave.enemyAsset);
                    }

                    if (i < wave.count - 1)
                    {
                        timeToSpawn += wave.timeBetweenSpawns;
                    }
                }
            }

            m_PassedTimeAtPreviousFrame = passedTime;

        }

        private void SpawnEnemy(EnemyAsset asset)
        {
            EnemyView view = Object.Instantiate(asset.viewPrefab);
            EnemyData data = new EnemyData(asset);
            
            Vector3 nodePosition = m_Grid.GetStartNode().m_Position;
            view.transform.position = new Vector3(
                nodePosition.x,
                data.MAsset.viewPrefab.transform.position.y,
                nodePosition.z
            );
            
            data.AttachView(view);
            view.CreateMovementAgent(m_Grid);
            
            Game.SPLayer.EnemySpawned(data);
        }
    }
}