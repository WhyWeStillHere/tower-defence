using UnityEditor;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(menuName = "Assets/Level Asset", fileName = "Level Asset")]
    public class LevelAsset : ScriptableObject
    {
        public SceneAsset sceneAsset;
        public SpawnWavesAsset spawnWavesAsset;
    }
}