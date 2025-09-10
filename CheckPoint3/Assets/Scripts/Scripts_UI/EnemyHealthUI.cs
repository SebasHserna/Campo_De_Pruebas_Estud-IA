using UnityEngine;
using TMPro;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Carrier enemy; // Referencia al NPC
    [SerializeField] private TextMeshPro worldText; // Texto encima de la cabeza
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0); // altura sobre la cabeza

    private Camera mainCamera;

    private void Start()
    {
        if (enemy == null)
            enemy = GetComponent<Carrier>();

        if (worldText == null)
        {
            Debug.LogError("No hay TextMeshPro asignado en " + gameObject.name);
        }

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (enemy == null || worldText == null) return;

        // Actualizar vida actual / máxima
        worldText.text = $"{enemy.Health.CurrentValue}/{enemy.Health.MaxValue}";

        // Posicionar el texto sobre la cabeza
        worldText.transform.position = enemy.transform.position + offset;

        // Hacer que siempre mire a la cámara
        worldText.transform.rotation = Quaternion.LookRotation(worldText.transform.position - mainCamera.transform.position);
    }
}
