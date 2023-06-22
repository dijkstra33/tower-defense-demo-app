using UnityEngine;

namespace Game.HealthSystem
{
    public class Armor : MonoBehaviour
    {
        [SerializeField]
        private int baseArmor;

        public int BaseArmor => baseArmor;
    }
}