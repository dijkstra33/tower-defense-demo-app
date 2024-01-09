using System.Collections;
using Core.ObjectPooling;
using UnityEngine;

namespace Game.HealthSystem.DeathEffects
{
    [RequireComponent(typeof(Poolable), typeof(ParticleSystem))]
    public class DeathEffect : MonoBehaviour
    {
        [SerializeField]
        private float _duration;
        
        private ParticleSystem _particleSystem;
        private Poolable _poolable;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _poolable = GetComponent<Poolable>();
        }

        public void Execute()
        {
            StartCoroutine(ExecuteCoroutine());
        }

        private IEnumerator ExecuteCoroutine()
        {
            _particleSystem.Play();
            yield return new WaitForSeconds(_duration);
            _poolable.ReleaseObject();
        }
    }
}