using UnityEngine;

namespace Field
{
    public struct Connection
    {
        public Vector2Int cooordinate;
        public float weight;

        public Connection(Vector2Int cooordinate, float weight)
        {
            this.cooordinate = cooordinate;
            this.weight = weight;
        }
    }
}