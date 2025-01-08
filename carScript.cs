using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;

    private float moveInput = 0f;
    private float rotationInput = 0f;

    void Update()
    {
        transform.Translate(Vector2.up * moveInput * speed * Time.deltaTime);
        transform.Rotate(Vector3.forward, -rotationInput * rotationSpeed * Time.deltaTime);
    }

    public void SetMoveInput(float input)
    {
        moveInput = input;
    }

    public void SetRotationInput(float input)
    {
        rotationInput = input;
    }
}