using UnityEngine;

namespace Turret.Weapon.Projectile
{
    [CreateAssetMenu(menuName = "Assets/Turret Projectile Weapon Asset", fileName = "Turret Projectile Weapon Asset")]

    public class TurretProjectileWeaponAsset : TurretWeaponAssetBase
    {
        public float rateOfFire;
        public float maxDistance;

        public ProjectileAssetBase projectileAsset;
        
        public override ITurretWeapon GetWeapon(TurretView view)
        {
            return new TurretProjectileWeapon(this, view);
        }
    }
}