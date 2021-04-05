using Field;
using Turret.Weapon;

namespace Turret
{
    public class TurretData
    {
        private TurretAsset m_Asset;
        private TurretView m_View;
        private Node m_Node;
        private ITurretWeapon m_Weapon;
        
        public TurretView MView => m_View;
        public Node MNode => m_Node;
        public ITurretWeapon MWeapon => m_Weapon;

        public TurretData(TurretAsset asset, Node node)
        {
            m_Asset = asset;
            m_Node = node;
        }


        public void AttachView(TurretView view)
        {
            m_View = view;
            m_View.AttachData(this);

            m_Weapon = m_Asset.WeaponAsset.GetWeapon(m_View);
        }

    }
}