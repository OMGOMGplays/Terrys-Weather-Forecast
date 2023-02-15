using Sandbox;
using System.Collections.Generic;

namespace TWF.Weapons.Base
{
    public partial class TWFWeapon : BaseWeapon 
    {
        public virtual string WeaponModel {get; set;}

        public virtual bool Reloads {get; set;}

        public virtual float ReloadTime => 2.0f;

        public virtual int AmmoCapacity => 5;

        [Net, Predicted]
        public int AmmoMag {get; set;}

        public virtual int ReloadBy => 1;

        [Net, Predicted]
        public TimeSince TimeSinceReload {get; set;}

        [Net, Predicted]
        public bool IsReloading {get; set;}

        [Net, Predicted]
        public TimeSince TimeSinceDeployed {get; set;}

        public float BulletDamage;
        public float BulletSpread;
        public float BulletSize;

        public override void Spawn() 
        {
            base.Spawn();

            SetModel(WeaponModel);
        }

        public override void ActiveStart(Entity ent) 
        {
            base.ActiveStart(ent);

            TimeSinceDeployed = 0;
        }

        public override void Reload() 
        {
            if (Reloads && AmmoMag < AmmoCapacity)
            {
                if (IsReloading) return;

                TimeSinceReload = 0;
                IsReloading = true;

                AmmoMag += ReloadBy;

                (Owner as AnimatedEntity)?.SetAnimParameter("b_reload", true);

                StartReloadEffects();
            }
        }

        public override void Simulate(IClient owner) 
        {
            if (TimeSinceDeployed < 0.6f) return;

            if (!IsReloading) base.Simulate(owner);

            if (IsReloading && AmmoMag == AmmoCapacity) 
            {
                OnReloadFinish();
            }
        }

        public void OnReloadFinish() 
        {
            IsReloading = false;
        }

        [ClientRpc]
        public virtual void StartReloadEffects() 
        {
            (Owner as AnimatedEntity)?.SetAnimParameter("b_reload", true);
        }

        public void Remove() 
        {
            Delete();
        }

        [ClientRpc]
        protected virtual void ShootEffects() 
        {
            Game.AssertClient();

            Particles.Create("particles/weapons/weapon_test_muzzleflash.vpcf", EffectEntity, "muzzle");

            (Owner as AnimatedEntity)?.SetAnimParameter("b_attack", true);
        }

        public override IEnumerable<TraceResult> TraceBullet(Vector3 start, Vector3 end, float radius = 2.0f) 
        {
            bool underWater = Trace.TestPoint(start, "water");

            var trace = Trace.Ray(start, end)
                        .UseHitboxes()
                        .WithAnyTags("solid", "player", "npc")
                        .Ignore(this)
                        .Size(radius);

            if (!underWater) trace.WithAnyTags("water");

            var tr = trace.Run();

            if (tr.Hit) yield return tr;
        }

        public IEnumerable<TraceResult> TraceMelee(Vector3 start, Vector3 end, float radius = 2.0f) 
        {
            var trace = Trace.Ray(start, end)
                            .UseHitboxes()
                            .WithAnyTags("solid", "player", "npc")
                            .Ignore(this);

            var tr = trace.Run();

            if (tr.Hit) yield return tr;
            else 
            {
                trace = trace.Size(radius);

                tr = trace.Run();

                if (tr.Hit) yield return tr;
            }
        }

        public virtual void ShootBullet(Vector3 pos, Vector3 dir, float spread, float force, float damage, float bulletSize) 
        {
            var forward = dir;
            forward += (Vector3.Random + Vector3.Random + Vector3.Random + Vector3.Random) * spread * 0.25f;
            forward = forward.Normal;

            spread -= BulletSpread;
            damage += BulletDamage;
            bulletSize += BulletSize;

            foreach (var tr in TraceBullet(pos, pos + forward * 10000, bulletSize)) 
            {
                tr.Surface.DoBulletImpact(tr);

                if (!Game.IsServer) continue;
                if (!tr.Entity.IsValid()) continue;

                using (Prediction.Off()) 
                {
                    var damageInfo = DamageInfo.FromBullet(tr.EndPosition, forward * 100 * force, damage)
                                    .UsingTraceResult(tr)
                                    .WithAttacker(Owner)
                                    .WithWeapon(this);

                    tr.Entity.TakeDamage(damageInfo);
                }
            }
        }

        public virtual void ShootBullet(float spread, float force, float damage, float bulletSize) 
        {
            Game.SetRandomSeed(Time.Tick);

            var ray = Owner.AimRay;
            ShootBullet(ray.Position, ray.Forward, spread, force, damage, bulletSize);
        }

        public virtual void ShootBullets(int numBullets, float spread, float force, float damage, float bulletSize) 
        {
            var ray = Owner.AimRay;

            for (int i = 0; i < numBullets; i++) 
            {
                ShootBullet(ray.Position, ray.Forward, spread, force / numBullets, damage, bulletSize);
            } 
        }
    }
}