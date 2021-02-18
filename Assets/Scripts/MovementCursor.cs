using System;
using System.Linq.Expressions;
using UnityEngine;

namespace DefaultNamespace
{
    public class MovementCursor : MonoBehaviour
    {
        [SerializeField]
        private int m_GridWidth;
        [SerializeField]
        private int m_GridHeight;

        [SerializeField]
        private float m_NodeSize;
        
        [SerializeField]
        private MovementAgent m_MovementAgent;
        
        [SerializeField]
        private GameObject m_Cursor;

        private Camera m_Camera;

        private Vector3 m_Offset;

        private void OnValidate()
        {
            float width = m_GridWidth * m_NodeSize;
            float height = m_GridHeight * m_NodeSize;
            
            // Default plane size is 10 by 10
            transform.localScale = new Vector3(
                width * 0.1f,
                1f, 
                height * 0.1f);

            m_Offset = transform.position - 
                       (new Vector3(width, 0, height) * 0.5f);
        }

        private void Awake()
        {
            m_Camera = Camera.main;
        }

        private void Update()
        {
            if (m_Camera == null)
            {
                return;
            }

            Vector3 mousePosition = Input.mousePosition;

            Ray ray = m_Camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform != transform)
                {
                    return;
                }

                Vector3 hitPosition = hit.point;

                Vector3 difference = hitPosition - m_Offset;

                int x = (int) (difference.x / m_NodeSize);
                int y = (int) (difference.z / m_NodeSize);
                Vector3 targetPosition = new Vector3(
                    x * m_NodeSize + m_Offset.x + m_NodeSize * 0.5f,
                    hit.point.y + m_MovementAgent.transform.localScale.y / 2,
                    y * m_NodeSize + m_Offset.z + m_NodeSize * 0.5f);
                
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log(x + " " + y);
                    m_MovementAgent.SetTarget(targetPosition);
                }
                m_Cursor.transform.position = targetPosition;
                m_Cursor.SetActive(true);
            
            }
            else
            {
                m_Cursor.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            
            // Draw grid
            for (int i = 0; i <= m_GridWidth; i++)
            {
                Vector3 from = new Vector3(
                    i * m_NodeSize,
                    0f,
                    0f) + m_Offset;
                Vector3 to = new Vector3(
                    i * m_NodeSize,
                    0f,
                    m_GridHeight * m_NodeSize) + m_Offset;
                Gizmos.DrawLine(from, to);
            }
            
            for (int i = 0; i <= m_GridHeight; i++)
            {
                Vector3 from = new Vector3(
                    0f,
                    0f,
                    i * m_NodeSize) + m_Offset;
                Vector3 to = new Vector3(
                    m_GridWidth * m_NodeSize, 
                    0f,
                    i * m_NodeSize) + m_Offset;
                Gizmos.DrawLine(from, to);
            }
        }
    }
}