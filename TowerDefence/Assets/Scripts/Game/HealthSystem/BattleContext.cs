using System.Collections.Generic;
using System.Linq;
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
            var keys = hitsCountByBuffHolder.Keys.ToArray();
            foreach (var key in keys)
            {
                hitsCountByBuffHolder[key] = 0;
            }
        }
    }
}