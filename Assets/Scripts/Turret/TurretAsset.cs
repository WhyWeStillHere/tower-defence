using UnityEngine;

namespace Turret
{
    [CreateAssetMenu(menuName = "Assets/Turret Asset", fileName = "Turret Asset")]
    public class TurretAsset : ScriptableObject
    {
        public TurretView viewPrefab;
        
        public TurretAsset(TurretView viewPrefab)
        {
            this.viewPrefab = viewPrefab;
        }
    }

}