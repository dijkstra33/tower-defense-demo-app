using UI;
using UnityEngine;

namespace Game.Health
{
    public class TowerDeathHandler : MonoBehaviour, IDeathHandler
    {
        public void OnDeath()
        {
            HUD.Instance.ShowGameEndScreen();
        }
    }
}