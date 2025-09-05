using UnityEngine;

public class LookAtPointer : MonoBehaviour
{
    public Transform player;
    public Camera cam;

    void Update()
    {
        // Ray desde el mouse al suelo
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            transform.position = hit.point;
        }

        // El player mira hacia el puntero SOLO si mantienes clic derecho
        if (Input.GetMouseButton(1))
        {
            Vector3 dir = transform.position - player.position;
            dir.y = 0;
            if (dir.sqrMagnitude > 0.01f)
            {
                player.rotation = Quaternion.LookRotation(dir);
            }
        }
    }
}
