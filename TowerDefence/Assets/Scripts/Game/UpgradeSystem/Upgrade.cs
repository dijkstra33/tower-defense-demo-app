using UnityEngine;

namespace Game.UpgradeSystem
{
    [CreateAssetMenu(menuName = "Game/Upgrades/Upgrade")]
    public class Upgrade : ScriptableObject
    {
        public int Price => price;
        [SerializeField]
        private int price;

        public Sprite Sprite => sprite;
        [SerializeField]
        private Sprite sprite;
    }
}