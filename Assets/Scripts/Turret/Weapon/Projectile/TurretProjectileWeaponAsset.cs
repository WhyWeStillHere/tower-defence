namespace Turret.Weapon.Projectile
{
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