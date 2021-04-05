using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field
{
    public class Grid
    {
        private Node[,] m_Nodes;

        private int m_Width;
        private int m_Height;
        private Vector3 m_Offset;
        private float m_NodeSize;

        private Vector2Int m_StartCoordinate;
        private Vector2Int m_TargetCoordinate;

        private Node m_SelectedNode;

        private FlowFieldPathfinding m_PathFinding;

        public int Width => m_Width;

        public int Height => m_Height;

        public Grid(int width, int height, Vector3 offset, float nodeSize, Vector2Int start, Vector2Int target)
        {
            m_Width = width;
            m_Height = height;
            m_Offset = offset;
            m_NodeSize = nodeSize;

            m_StartCoordinate = start;
            m_TargetCoordinate = target;

            m_Nodes = new Node[m_Width, m_Height];

            for (int i = 0; i < m_Nodes.GetLength(0); i++)
            {
                for (int j = 0; j < m_Nodes.GetLength(1); j++)
                {
                    m_Nodes[i, j] = new Node(offset + new Vector3(i + .5f, 0, j + .5f) * nodeSize);
                }
            }

            m_PathFinding = new FlowFieldPathfinding(this, target, start);
            
            m_PathFinding.UpdateField();
        }

        public void SelectCoordinate(Vector2Int coordinate)
        {
            m_SelectedNode = GetNode(coordinate);
        }
        
        public void UnselectNode()
        {
            m_SelectedNode = null;
        }

        public bool HasSelectedNode()
        {
            return m_SelectedNode != null;
        }
        public Node GetSelectedNode()
        {
            return m_SelectedNode;
        }
        public Node GetStartNode()
        {
            return GetNode(m_StartCoordinate);
        }

        public Node GetTargetNode()
        {
            return GetNode(m_TargetCoordinate);
        }

        public Node GetNode(Vector2Int coordinate)
        {
            return GetNode(coordinate.x, coordinate.y);
        }

        public Node GetNode(int i, int j)
        {
            if (i < 0 || i >= m_Width || j < 0 || j >= m_Height)
            {
                return null;
            }
            return m_Nodes[i, j];
        }

        public Node GetNodeAtPoint(Vector3 point)
        {
            Vector3 difference = point - m_Offset;
            int x = (int) (difference.x / m_NodeSize);
            int y = (int) (difference.z / m_NodeSize);
            return GetNode(x, y);
        }

        public Vector3 GetNodeCenter(int x, int y)
        {
            Vector3 center = m_Offset;
            center.x += (x + .5f) * m_NodeSize;
            center.z += (y + .5f) * m_NodeSize;
            return center;
        }

        public List<Node> GetNodesInCircle(Vector3 point, float radius)
        {
            Vector3 difference = point - m_Offset;
            int x = (int) (difference.x / m_NodeSize);
            int y = (int) (difference.z / m_NodeSize);
            
            int x_start = Math.Max((int) (x - radius), 0);
            int x_end = Math.Min((int) (x + radius + 1), m_Width - 1);
            int y_start = Math.Max((int) (y - radius), 0);
            int y_end = Math.Min((int) (y + radius + 1), m_Height - 1);
            
            float sqrRadius = radius * radius;
            List<Node> nodes = new List<Node>();
            
            for (int i = x_start; i <= x_end; ++i)
            {
                for (int j = y_start; j <= y_end; ++j)
                {
                    Vector3 nodeCenter = GetNodeCenter(i, j);
                    if ((nodeCenter - point).sqrMagnitude < sqrRadius)
                    {
                        nodes.Add(GetNode(i, j));
                    }
                }
            }

            return nodes;
        }

        public IEnumerable<Node> EnumerateAllNodes()
        {
            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Height; j++)
                {
                    yield return GetNode(i, j);
                }
            }
        }

        public bool TryOccupyNode(Vector2Int coordinate)
        {
            if (m_PathFinding.CanOccupy(coordinate))
            {
                GetNode(coordinate).m_IsOccupied = true;
                UpdatePathFinding();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vector2Int GetNodeCoordinate(Node node)
        {
            Vector3 difference = node.m_Position - m_Offset;
            int x = (int) (difference.x / m_NodeSize);
            int y = (int) (difference.z / m_NodeSize);
            return new Vector2Int(x, y);
        }

        public bool CanOccupy(Vector2Int coordinate)
        {
            return m_PathFinding.CanOccupy(coordinate);
        }

        public void UpdatePathFinding()
        {
            m_PathFinding.UpdateField();
        }
    }
}