﻿using Core.ObjectPooling;
using UnityEngine;

namespace Game.Health
{
    [RequireComponent(typeof(Poolable))]
    public class PoolableDeathHandler : MonoBehaviour, IDeathHandler
    {
        public void OnDeath()
        {
            var poolable = GetComponent<Poolable>();
            poolable.ReleaseObject();
        }
    }
}