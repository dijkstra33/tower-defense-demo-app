﻿using System.Collections;
using Game.AttributeSystem;
using Game.AttributeSystem.Buffs;
using Game.HealthSystem;
using UnityEngine;

namespace Game.CurrencySystem
{
    public class CurrencyManager : MonoBehaviour
    {
        [SerializeField]
        private float passiveIncomeStartDelay;
        
        [SerializeField]
        private float passiveIncomeInterval;

        private Tower tower;

        private void Start()
        {
            tower = Tower.Instance;
            StartCoroutine(ProducePassiveCurrencyIncome());
            DeathManager.Instance.OnUnitDeath += HandleUnitDeath;
        }

        private IEnumerator ProducePassiveCurrencyIncome()
        {
            yield return new WaitForSeconds(passiveIncomeStartDelay);
            while (true)
            {
                if (GameManager.Instance.GameOver)
                {
                    yield break;
                }
                
                var currencyPassiveIncome = (int)tower.AttributeOwner.GetValue(AttributeType.CurrencyPassiveIncome);
                tower.ReceiveCurrency(currencyPassiveIncome);
                yield return new WaitForSeconds(passiveIncomeInterval);
            }
        }
        
        private void HandleUnitDeath(Unit unit, BuffHolder weaponBuffHolder, Health killerHealth)
        {
            var basicKillCurrencyReward = tower.AttributeOwner.GetValue(AttributeType.KillCurrencyBonus);
            var unitKillCurrencyRewardBonus = unit.AttributeOwner.GetValue(AttributeType.KillCurrencyBonus);
            
            var totalKillCurrencyReward = basicKillCurrencyReward + unitKillCurrencyRewardBonus;

            tower.ReceiveCurrency((int)totalKillCurrencyReward);
        }
    }
}