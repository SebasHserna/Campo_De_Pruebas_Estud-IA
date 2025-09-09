using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathUI : MonoBehaviour
{
    [SerializeField] private PlayableCarrier player;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject explosionPrefab;

    private bool isDead = false;
 

    private void Start()
    {
        if (deathScreen != null)
            deathScreen.SetActive(false);

        if (player == null)
            Debug.LogError("No se asignó el Player en PlayerDeathUI.");

       
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

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

    
}
