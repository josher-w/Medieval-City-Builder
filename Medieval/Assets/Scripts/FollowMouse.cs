using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    Vector3 mousePosition;
    Vector2 circlePosition;
    [SerializeField] public float moveSpeed;
    [SerializeField] Rigidbody2D rb;

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        circlePosition = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(circlePosition);
    }
}
