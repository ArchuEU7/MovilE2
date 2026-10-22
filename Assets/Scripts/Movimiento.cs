
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

        Debug.Log(
            $"AutoRunner activo | Speed: {speed} | Velocity X: {rb.linearVelocity.x}"
        );
    }

}
