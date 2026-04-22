using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private string startScene;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject typeText;

    private Player player;

    private void Start()
    {
        var playerObj = GameObject.Find("Player");
        if (playerObj)
            player = playerObj.GetComponent<Player>();
    }

    public void MoveToScene()
    {
        SceneManager.LoadScene(startScene);
        Time.timeScale = 1.0f;
    }

    public void ContinueGame()
    {
        //Time.timeScale = 1.0f;
        //pauseMenu.SetActive(false);
        //typeText.SetActive(true);
        player.ContinueGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
