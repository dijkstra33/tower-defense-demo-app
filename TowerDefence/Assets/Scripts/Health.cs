using UnityEngine;

namespace TowerDefence
{
    public class Health : MonoBehaviour
    {
        private int currentValue;

        [SerializeField]
        private int maxValue;

        private void Start()
        {
            currentValue = maxValue;
        }
    }
}
