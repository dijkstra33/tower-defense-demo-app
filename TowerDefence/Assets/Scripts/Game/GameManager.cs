using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager
    {
        public static void RestartLevel()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}