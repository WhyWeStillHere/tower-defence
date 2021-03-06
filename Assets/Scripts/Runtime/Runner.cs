using System;
using System.Collections.Generic;
using Enemy;
using EnemySpawn;
using Field;
using NUnit.Framework.Constraints;
using Turret.Weapon;
using TurretSpawn;
using UnityEngine;

namespace Runtime
{
    public class Runner : MonoBehaviour
    {
        private List<IController> m_Controllers;
        private bool m_IsRunning = false;

        private void Update()
        {
            if (!m_IsRunning)
            {
                return;
            }
            
            TickControllers();
        }

        public void StartRunning()
        {
            CreateAllControllers();
            OnStartControllers();
            m_IsRunning = true;
        }
        
        public void StopRunning()
        {
            OnStopControllers();
            m_IsRunning = false;
        }

        private void CreateAllControllers()
        {
            m_Controllers = new List<IController>()
            {
                new GridRaycastController(Game.SPLayer.gridHolder),
                new EnemySpawnController(Game.SCurrentLevel.spawnWavesAsset, Game.SPLayer.grid),
                new TurretSpawnController(Game.SPLayer.grid, Game.SPLayer.turretMarket),
                new MovementController(),
                new TurretShootController(),
            };
        }

        private void OnStartControllers()
        {
            foreach (IController controller in m_Controllers)
            {
                try
                {
                    controller.OnStart();
                } catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
        
        private void OnStopControllers()
        {
            foreach (IController controller in m_Controllers)
            {
                try
                {
                    controller.OnStop();
                } catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        private void TickControllers()
        {
            foreach (IController controller in m_Controllers)
            {
                try
                {
                    controller.Tick();
                } catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}