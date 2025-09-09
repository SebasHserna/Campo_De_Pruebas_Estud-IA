using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum CharacterType { Berserk, Mage }
    public CharacterType SelectedCharacter { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre escenas
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Solo actuamos si estamos en la escena de selección
        if (scene.name == "Scene_Selection")
        {
            // Buscar botones dinámicamente
            Button BerserkButton = GameObject.Find("BerserkButton")?.GetComponent<Button>();
            Button MageButton = GameObject.Find("MageButton")?.GetComponent<Button>();

            if (BerserkButton != null)
            {
                BerserkButton.onClick.RemoveAllListeners();
                BerserkButton.onClick.AddListener(() => SelectCharacter(CharacterType.Berserk));
            }

            if (MageButton != null)
            {
                MageButton.onClick.RemoveAllListeners();
                MageButton.onClick.AddListener(() => SelectCharacter(CharacterType.Mage));
            }
        }
    }

    // Método que carga la escena del personaje seleccionado
    public void SelectCharacter(CharacterType character)
    {
        SelectedCharacter = character;

        switch (SelectedCharacter)
        {
            case CharacterType.Berserk:
                SceneManager.LoadScene("Berserk_Action");
                break;
            case CharacterType.Mage:
                SceneManager.LoadScene("Mage_Action");
                break;
        }
    }
}
