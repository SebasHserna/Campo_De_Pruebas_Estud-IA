using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public void LoadMage()
    {
        SceneManager.LoadScene("Mage_Action");
    }

    public void LoadBerserk()
    {
        SceneManager.LoadScene("Berserk_Action");
    }
}
