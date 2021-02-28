using System;
using System.Collections;
using System.Collections.Generic;
using Field;
using UnityEngine;

namespace DefaultNamespace.Unit
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private GridMovementAgent m_MovementAgent;

        [SerializeField] private GridHolder m_GridHolder;

        private IEnumerator SpawnUnitsCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                SpawnUnit();
            }
        }

        private void Awake()
        {
            StartCoroutine(SpawnUnitsCoroutine());
        }

        private void SpawnUnit()
        {
            Node startNode = m_GridHolder.MGrid.GetNode(m_GridHolder.MStartCoordinate);
            Vector3 position = startNode.m_Position;
            GridMovementAgent movementAgent = Instantiate(m_MovementAgent, position, Quaternion.identity);
            movementAgent.SetStartNode(startNode);
        }
    }
}