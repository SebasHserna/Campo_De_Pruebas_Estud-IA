using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathUI : MonoBehaviour
{
    [SerializeField] private PlayableCarrier player;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject explosionPrefab;

    private bool isDead = false;
    private float deathTimer = 0f;
    private float delayBeforeReturn = 4f; // segundos antes de volver al menu

    private void Start()
    {
        if (deathScreen != null)
            deathScreen.SetActive(false);

        if (player == null)
            Debug.LogError("No se asignó el Player en PlayerDeathUI.");

        ResetState();
    }

    private void OnEnable()
    {
        ResetState();
    }

    private void ResetState()
    {
        isDead = false;
        deathTimer = 0f;
        if (deathScreen != null)
            deathScreen.SetActive(false);
        if (player != null)
            player.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (player == null) return;

        if (!isDead && player.Health.CurrentValue <= player.Health.MinValue)
        {
            Die();
        }

        if (isDead)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= delayBeforeReturn)
            {
                GoToCharacterSelection();
            }
        }
    }

    private void Die()
    {
        isDead = true;

        if (deathScreen != null)
            deathScreen.SetActive(true);

        if (explosionPrefab != null)
        {
            // Instanciamos la explosión y destruimos automáticamente después de 2 segundos
            GameObject exp = Instantiate(explosionPrefab, player.transform.position, Quaternion.identity);
            Destroy(exp, 2f);
        }

        player.gameObject.SetActive(false);
    }

    private void GoToCharacterSelection()
    {
     

        // Cargar escena de selección
        SceneManager.LoadScene("Scene_Selection");
    }
}
