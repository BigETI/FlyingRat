using UnityEngine;
using UnitySceneLoaderManager;
using UnityUtils;

public class MainMenuControllerScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoaderManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Game.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Game.Quit();
        }
    }
}
