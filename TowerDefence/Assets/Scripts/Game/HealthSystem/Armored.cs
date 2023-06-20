using System;
using UnityEngine;

namespace Game.HealthSystem
{
    public abstract class Armored : MonoBehaviour
    {
        public abstract int GetArmor();

        public int GetPureDamage(float damage)
        {
            var armor = GetArmor();
            return Math.Max(1, (int)damage - armor);
        }
    }
}