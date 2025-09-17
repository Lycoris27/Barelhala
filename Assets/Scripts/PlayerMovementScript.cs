using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour
{
    //private InputSystem inputSystem;
    private Rigidbody rb;
    public float speed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        PlayerInputManagerScript.OnMovePerformed += PlayerMove;
    }
    private void OnDisable()
    {
        PlayerInputManagerScript.OnMovePerformed -= PlayerMove;
    }

    private void PlayerMove(Vector2 moveVal)
    {
        rb.linearVelocity = new Vector3(-moveVal.y, 0, moveVal.x) * speed;
    }
}
