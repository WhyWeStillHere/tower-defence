using System;
using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;


namespace Runtime
{
    public static class Game
    {
        private static PLayer s_PLayer;
        private static AssetRoot s_AssetRoot;
        private static LevelAsset s_CurrentLevel;
        private static Runner s_Runner;

        public static PLayer SPLayer => s_PLayer;
        public static AssetRoot SAssetRoot => s_AssetRoot;
        public static LevelAsset SCurrentLevel => s_CurrentLevel;

        public static void SetAssetRoot(AssetRoot assetRoot)
        {
            s_AssetRoot = assetRoot;
        }

        public static void StartLevel(LevelAsset levelAsset)
        {
            s_CurrentLevel = levelAsset;
            AsyncOperation operation = SceneManager.LoadSceneAsync(levelAsset.sceneAsset.name);
            operation.completed += StartPlayer;
        }

        private static void StartPlayer(AsyncOperation operation)
        {
            if (!operation.isDone)
            {
                throw new Exception();
            }
            s_PLayer = new PLayer();
            s_Runner = Object.FindObjectOfType<Runner>();
            s_Runner.StartRunning();
        }

        public static void StopPlayer()
        {
            s_Runner.StopRunning();
        }
    }
}