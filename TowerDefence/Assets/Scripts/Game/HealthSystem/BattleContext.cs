using System.Collections.Generic;
using Core.ObjectPooling;
using Game.AttributeSystem.Buffs;
using UnityEngine;

namespace Game.HealthSystem
{
    public class BattleContext : MonoBehaviour, IResettable
    {
        private readonly Dictionary<BuffHolder, int> hitsCountByBuffHolder = new();

        private void Start()
        {
            BattleContextManager.Instance.Register(this);
        }

        public void RegisterHitBy(BuffHolder buffHolder)
        {
            if (buffHolder == null)
            {
                return;
            }
            
            if (!hitsCountByBuffHolder.ContainsKey(buffHolder))
            {
                hitsCountByBuffHolder[buffHolder] = 0;
            }

            hitsCountByBuffHolder[buffHolder]++;
        }

        public int GetHitsCountBy(BuffHolder buffHolder)
        {
            return hitsCountByBuffHolder.TryGetValue(buffHolder, out var hitsCount) ? hitsCount : 0;
        }

        public void RemoveInfluenceOf(BuffHolder buffHolder)
        {
            if (hitsCountByBuffHolder.ContainsKey(buffHolder))
            {
                hitsCountByBuffHolder[buffHolder] = 0;
            }
        }
        
        public void Reset()
        {
            foreach (var key in hitsCountByBuffHolder.Keys)
            {
                hitsCountByBuffHolder[key] = 0;
            }
        }
    }
}