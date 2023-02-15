namespace TWF.Weapons
{
    public partial class TestWeapon : TWFWeapon 
    {
        public override string WeaponModel => "models/citizen/citizen.vmdl";

        public override bool Reloads => true;

        public override float ReloadTime => 1.0f;

        public override int AmmoCapacity => 10;
        
        public override bool CanPrimaryAttack() 
        {
            return base.CanPrimaryAttack() && Input.Pressed(InputButton.PrimaryAttack);
        }

        public override void AttackPrimary() 
        {
            TimeSincePrimaryAttack = 0;

            (Owner as AnimatedEntity)?.SetAnimParameter("b_attack", true);

            ShootEffects();
            PlaySound("test.shoot");
            ShootBullets(5, 0.05f, 1.5f, 9.0f, 3.0f);
        }
    }
}