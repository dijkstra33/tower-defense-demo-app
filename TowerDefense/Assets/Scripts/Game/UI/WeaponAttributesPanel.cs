using UnityEngine;

namespace Game.UI
{
    public class WeaponAttributesPanel : MonoBehaviour
    {
        [SerializeField]
        private WeaponAttributesView attributesViewPrefab;
        
        [SerializeField]
        private Transform attributesViewsRoot;

        private WeaponAttributesView[] _attributesViews;

        private void Start()
        {
            var towerWeapons = Tower.Instance.Weapons;
            _attributesViews = new WeaponAttributesView[towerWeapons.Length];
            for (int i = 0; i < _attributesViews.Length; i++)
            {
                var weapon = towerWeapons[i];
                _attributesViews[i] = Instantiate(attributesViewPrefab, attributesViewsRoot);
                _attributesViews[i].SetData(weapon.VisualData, weapon.AttributeOwner);
            }
        }
    }
}