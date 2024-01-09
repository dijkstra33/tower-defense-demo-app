using System.Collections;
using Core.ObjectPooling;
using UnityEngine;

namespace Game.SpawnSystem
{
    [RequireComponent(typeof(Poolable))]
    public class Portal : MonoBehaviour
    {
        private Poolable _poolable;
        
        private void Start()
        {
            _poolable = GetComponent<Poolable>();
            InitPortalMaterial();
        }

        private void InitPortalMaterial()
        {
            var renderer = GetComponent<Renderer>();
            // Create copy of material to have different offsets in every instance.
            renderer.material = new Material(renderer.material);
            renderer.material.SetFloat("_Offset", Random.value);
        }

        public void Init(float lifetime)
        {
            StartCoroutine(ReturnToPoolAfter(lifetime));
        }

        private IEnumerator ReturnToPoolAfter(float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            ObjectPoolManager.Instance.ReleaseObject(_poolable);
        }
    }
}