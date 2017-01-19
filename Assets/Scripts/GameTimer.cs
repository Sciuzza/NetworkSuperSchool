using UnityEngine.UI;
using UnityEngine;

public class GameTimer : MonoBehaviour {
    public float countdownFrom;
    public Text textbox;

    void Update()
    {
        float time = countdownFrom - Time.timeSinceLevelLoad;
        textbox.text = "Time left: " + time.ToString("0.00") + "s";

        if (time <= 0f)
        {
            TimeUp();
        }
    }

    void TimeUp()
    {
        // this function is called when the timer runs out
    }
}
