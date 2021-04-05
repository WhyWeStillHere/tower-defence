using Turret.Weapon.Projectile;
using UnityEngine;

namespace Turret.Weapon.Lazer
{
    [CreateAssetMenu(menuName = "Assets/Turret Lazer Weapon Asset", fileName = "Turret Lazer Weapon Asset")]
    public class TurretLazerWeaponAsset : TurretWeaponAssetBase
    {
        public LineRenderer LineRenderPrefab;
        public float maxDistance;
        public float damage;

        public override ITurretWeapon GetWeapon(TurretView view)
        {
            return new TurretLazerWeapon(this, view);
        }
    }
}