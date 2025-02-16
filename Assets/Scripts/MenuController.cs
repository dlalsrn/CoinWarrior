using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadToGameScene() {
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public void Quit() {
        Application.Quit();
    }
}
