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
                // TODO: this class is too greedy - maybe this should be placed inside Tower?
                tower.ReceiveCurrency(tower.CurrencyPassiveIncome);
                yield return new WaitForSeconds(passiveIncomeInterval);
            }
        }
        
        private void HandleUnitDeath(Unit unit)
        {
            tower.ReceiveCurrency(unit.DeathCurrencyReward);
        }
    }
}