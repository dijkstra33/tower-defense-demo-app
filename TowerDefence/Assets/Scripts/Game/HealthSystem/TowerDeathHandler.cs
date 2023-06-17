using UI;
using UnityEngine;

namespace Game.HealthSystem
{
    public class TowerDeathHandler : MonoBehaviour, IDeathHandler
    {
        public void OnDeath()
        {
            HUD.Instance.ShowGameEndScreen();
        }
    }
}