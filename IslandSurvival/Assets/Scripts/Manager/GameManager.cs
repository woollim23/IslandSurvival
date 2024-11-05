using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singletone<GameManager>
{
    bool isGameOver = false;
    bool isPaused = false;
    public void StartGame()
    {
        isGameOver = false;
        LoadGameScene();
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync("MainGame");
        CharacterManager.Instance.Player.condition.isDead = false;
        CharacterManager.Instance.Player.controller.canLook = true;
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadSceneAsync("Title");
    }

}