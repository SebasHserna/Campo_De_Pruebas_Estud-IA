
using UnityEngine;

public class CameraFollowFree : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform target; // El jugador

    [Header("Offsets")]
    public float distance = 6f;     // Distancia atrás del jugador
    public float height = 3f;       // Altura respecto al jugador

    [Header("Rotación libre")]
    public float sensitivityX = 150f;
    public float sensitivityY = 100f;
    public float minY = -20f;
    public float maxY = 60f;

    private float rotX;
    private float rotY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Arrancar mirando al frente del player
        rotY = target.eulerAngles.y;
    }

    void LateUpdate()
    {
        if (!target) return;

        // Movimiento del mouse
        rotY += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        rotX -= Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, minY, maxY);

        // Calcular rotación
        Quaternion rotation = Quaternion.Euler(rotX, rotY, 0);

        // Posición de la cámara = posición del player + offset
        Vector3 offset = rotation * new Vector3(0, height, -distance);
        transform.position = target.position + offset;

        // Mira al jugador
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
