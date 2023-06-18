using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameTimer : MonoBehaviour
    {
        private float time;
        private int prevTimeInSeconds = -1;

        [SerializeField]
        private TMP_Text timerText;

        private void Update()
        {
            time += Time.deltaTime;
            var timeInSeconds = Mathf.FloorToInt(time);
            if (timeInSeconds == prevTimeInSeconds)
            {
                return;
            }

            prevTimeInSeconds = timeInSeconds;
            var timeSpan = TimeSpan.FromSeconds(timeInSeconds);
            timerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
    }
}
