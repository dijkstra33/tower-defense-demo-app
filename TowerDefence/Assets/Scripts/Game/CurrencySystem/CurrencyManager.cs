using System.Collections;
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

        [SerializeField]
        private int passiveIncomeBaseValue;

        private void Start()
        {
            StartCoroutine(ProducePassiveCurrencyIncome());
            DeathManager.Instance.OnUnitDeath += HandleUnitDeath;
        }

        private IEnumerator ProducePassiveCurrencyIncome()
        {
            yield return new WaitForSeconds(passiveIncomeStartDelay);
            while (true)
            {
                Tower.Instance.ReceiveCurrency(passiveIncomeBaseValue);
                yield return new WaitForSeconds(passiveIncomeInterval);
            }
        }
        
        private void HandleUnitDeath(Unit unit)
        {
            Tower.Instance.ReceiveCurrency(unit.DeathCurrencyReward);
        }
    }
}