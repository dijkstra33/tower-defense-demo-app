using System;
using UnityEngine;

namespace Game.WeaponSystem
{
    [Serializable]
    public class WeaponVisualData
    {
        public string Name => name;
        [SerializeField]
        private string name;

        public Color Color => color;
        [SerializeField]
        private Color color;

        public Sprite Icon => icon;
        [SerializeField]
        private Sprite icon;
    }
}