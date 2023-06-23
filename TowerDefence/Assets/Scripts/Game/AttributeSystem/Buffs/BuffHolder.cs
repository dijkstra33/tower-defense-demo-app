using System;
using System.Collections.Generic;
using Core.ObjectPooling;
using Game.AttributeSystem.Upgrades;
using Game.Weapons;
using UnityEngine;

namespace Game.AttributeSystem.Buffs
{
    public class BuffHolder : MonoBehaviour, IResettable
    {
        [SerializeField]
        private BuffHolderType type;
        public BuffHolderType Type => type;
        
        private readonly Dictionary<AttributeType, List<AbstractBuff>> activeBuffsDict = new();
        
        public List<AbstractBuff> BuffsToTargetOnHit => buffsToTargetOnHit;
        private readonly List<AbstractBuff> buffsToTargetOnHit = new();
        
        public float GetBuffedValue(float baseValue, AttributeType attributeType, AttackContext context = null)
        {
            var buffs = activeBuffsDict.TryGetValue(attributeType, out var buffsList) ? buffsList : null;
            if (buffs == null)
            {
                return baseValue;
            }

            var value = baseValue;
            foreach (var buff in buffs)
            {
                value += buff.GetValue(context ?? AttackContext.Empty);
            }
            return value;
        }

        public void ApplyBuff(AbstractBuff buff)
        {
            ApplyBuff(buff, buff.ApplicationType);
        }

        public void ApplyBuff(AbstractBuff buff, BuffApplicationType applicationType)
        {
            switch (applicationType)
            {
                case BuffApplicationType.Direct:
                    var attributeType = buff.BuffedAttributeType;
                    if (!activeBuffsDict.TryGetValue(attributeType, out var activeBuffs))
                    {
                        activeBuffs = new List<AbstractBuff>();
                        activeBuffsDict.Add(attributeType, activeBuffs);
                    }

                    activeBuffs.Add(buff);
                    break;
                case BuffApplicationType.ToTargetOnHit:
                    buffsToTargetOnHit.Add(buff);
                    break;
                default:
                    throw new Exception($"Buff application type {applicationType} is not supported!");
            }
        }

        public void Reset()
        {
            foreach (var buffs in activeBuffsDict)
            {
                buffs.Value.Clear();
            }
            
            buffsToTargetOnHit.Clear();
        }
    }
}