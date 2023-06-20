using UnityEngine;

namespace Game.HealthSystem
{
    [RequireComponent(typeof(Unit), typeof(Health))]
    public class ArmoredUnit : Armored
    {
        [SerializeField]
        private int baseArmor;

        public override int GetArmor() => baseArmor;
    }
}