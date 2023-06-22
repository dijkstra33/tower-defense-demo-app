using System.Collections.Generic;
using Core.ObjectPooling;
using Game.Weapons;
using UnityEngine;

namespace Game.AttributeSystem.Buffs
{
    public class BuffHolder : MonoBehaviour, IResettable
    {
        [SerializeField]
        private BuffHolderType type;
        public BuffHolderType Type => type;
        
        private readonly Dictionary<AttributeType, List<AbstractBuff>> buffsDict = new();
        
        public float GetBuffedValue(float baseValue, AttributeType attributeType, AttackContext context = null)
        {
            var buffs = buffsDict.TryGetValue(attributeType, out var buffsList) ? buffsList : null;
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
            var attributeType = buff.BuffedAttributeType;
            if (!buffsDict.TryGetValue(attributeType, out var buffs))
            {
                buffs = new List<AbstractBuff>();
                buffsDict.Add(attributeType, buffs);
            }

            buffs.Add(buff);
        }

        public void Reset()
        {
            foreach (var buffs in buffsDict)
            {
                buffs.Value.Clear();
            }
        }
    }
}