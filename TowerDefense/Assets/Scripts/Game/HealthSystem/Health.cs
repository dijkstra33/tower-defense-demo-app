using System;
using Core.ObjectPooling;
using Game.AttributeSystem;
using Game.AttributeSystem.Buffs;
using Game.WeaponSystem;
using UnityEngine;

namespace Game.HealthSystem
{
    [RequireComponent(typeof(AbstractAttributeOwner))]
    public class Health : MonoBehaviour, IResettable
    {
        public event Action OnDamageReceived;
        public event Action OnValueChanged;
        public event Action OnDeath;
        
        public int MaxValue => maxValue;
        [SerializeField]
        private int maxValue;

        public int CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = value;
                OnValueChanged?.Invoke();
            }
        }
        private int currentValue;

        private bool isDead = false;

        private AbstractAttributeOwner attributeOwner;
        private BuffOwner buffOwner;

        private void Awake()
        {
            CurrentValue = maxValue;
            attributeOwner = GetComponent<AbstractAttributeOwner>();
            buffOwner = GetComponent<BuffOwner>();
        }

        public void ReceiveDamage(float damage, AbstractWeapon attackerWeapon = null, Health attackerHealth = null)
        {
            if (isDead)
            {
                return;
            }

            var pureDamage = GetPureDamage(damage);
            OnDamageReceived?.Invoke();
            BattleContextManager.Instance.OnDamageReceived(this, attackerWeapon);
            CurrentValue = Math.Max(CurrentValue - pureDamage, 0);

            if (CurrentValue == 0)
            {
                Die(attackerHealth, attackerWeapon);
            }
            else
            {
                TryToApplyBuffsOnHit(attackerWeapon);
            }
        }

        public void ReceiveHeal(AbstractAttributeOwner attrOwner, AttributeType healType)
        {
            if (isDead || attrOwner == null)
            {
                return;
            }

            var healValue = (int)Math.Abs(attrOwner.GetValue(healType));
            CurrentValue = Math.Min(maxValue, CurrentValue + healValue);
        }

        private void TryToApplyBuffsOnHit(AbstractWeapon attackerWeapon)
        {
            if (attackerWeapon == null || buffOwner == null)
            {
                return;
            }
            
            foreach (var buffOnHit in attackerWeapon.GetBuffsToTargetOnHit())
            {
                buffOwner.ApplyBuff(buffOnHit, BuffApplicationType.Direct);
            }
        }

        private int GetPureDamage(float damage)
        {
            var armor = (int)attributeOwner.GetValue(AttributeType.Armor);
            return Math.Max(1, (int)damage - armor);
        }

        private void Die(Health attackerHealth, AbstractWeapon attackerWeapon)
        {
            isDead = true;
            DeathManager.Instance.OnDeath(gameObject, attackerWeapon, attackerHealth);
            OnDeath?.Invoke();
        }

        public void Reset()
        {
            isDead = false;
            CurrentValue = maxValue;
        }
    }
}
