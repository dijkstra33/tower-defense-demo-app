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
                if (GameManager.Instance.GameOver)
                {
                    yield break;
                }
                
                // TODO: this class is too greedy - maybe this should be placed inside Tower?
                tower.ReceiveCurrency(tower.GetCurrencyPassiveIncome());
                yield return new WaitForSeconds(passiveIncomeInterval);
            }
        }
        
        private void HandleUnitDeath(Unit unit)
        {
            var totalKillCurrencyReward = unit.KillCurrencyReward + Tower.Instance.GetKillCurrencyBonus();
            tower.ReceiveCurrency(totalKillCurrencyReward);
        }
    }
}