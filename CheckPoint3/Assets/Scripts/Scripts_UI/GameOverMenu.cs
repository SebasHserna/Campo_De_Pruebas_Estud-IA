using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void ReturnToSelection()
    {
        SceneManager.LoadScene("Scene_Selection");
    }
}
