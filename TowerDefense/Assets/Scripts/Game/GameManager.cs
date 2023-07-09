using System;
using Core;
using Game.HealthSystem;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public event Action OnGameOver;
        
        public bool GameOver => gameOver;
        private bool gameOver = false;
        
        protected override void Awake()
        {
            base.Awake();
            DeathManager.Instance.OnTowerDeath += HandleTowerDeath;
        }

        private void HandleTowerDeath(Tower tower)
        {
            gameOver = true;
            OnGameOver?.Invoke();
        }

        public static void RestartLevel()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}