using UnityEngine;

public class PlayerMovementx2 : MonoBehaviour
{
    [Header("Jugador 1 - Berserk")]
    public Transform berserk;
    public float berserkSpeed = 5f;

    [Header("Jugador 2 - Mage")]
    public Transform mage;
    public float mageSpeed = 5f;

    void Update()
    {
        // Movimiento Berserk: teclas WASD
        Vector3 berserkMove = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) berserkMove += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) berserkMove += Vector3.back;
        if (Input.GetKey(KeyCode.A)) berserkMove += Vector3.left;
        if (Input.GetKey(KeyCode.D)) berserkMove += Vector3.right;

        berserk.Translate(berserkMove * berserkSpeed * Time.deltaTime, Space.World);

        // Movimiento Mage: teclas flechas
        Vector3 mageMove = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow)) mageMove += Vector3.forward;
        if (Input.GetKey(KeyCode.DownArrow)) mageMove += Vector3.back;
        if (Input.GetKey(KeyCode.LeftArrow)) mageMove += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) mageMove += Vector3.right;

        mage.Translate(mageMove * mageSpeed * Time.deltaTime, Space.World);
    }
}
