
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cam; // referencia a la cámara principal

    private Rigidbody rb;
    private Vector3 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Dirección en base a la cámara
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveInput = (forward * z + right * x).normalized;
    }

    void FixedUpdate()
    {
        if (moveInput.magnitude >= 0.1f)
        {
            Vector3 moveVelocity = moveInput * moveSpeed;
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

            // Rotar player hacia dirección de movimiento
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 0.15f);
        }
    }
}
