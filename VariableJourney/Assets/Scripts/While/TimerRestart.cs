using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerRestart : MonoBehaviour
{
    private float timer = 210f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            LevelRestart();
    }

    private void LevelRestart()
    {
        SceneManager.LoadScene(2);
    }
}
