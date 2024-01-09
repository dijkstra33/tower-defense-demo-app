using Core.ObjectPooling;
using UnityEngine;

namespace Game.HealthSystem.DeathEffects
{
    public class SpawnEffectOnDeath : MonoBehaviour, IBeforePutToPool
    {
        [SerializeField]
        private DeathEffect _deathEffectPrefab;

        private Transform _transform;

        private void Start()
        {
            _transform = gameObject.transform;
        }

        public void Execute()
        {
            var deathEffect = 
                ObjectPoolManager.Instance.GetObject(
                    _deathEffectPrefab, _transform.position, _transform.rotation, _deathEffectPrefab.transform.localScale, parent: DeathManager.Instance.DeathEffectRoot);
            deathEffect.Execute();
        }
    }
}