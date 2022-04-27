using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Select_Tile : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3Int position;
    [SerializeField] private Sprite tileToBecome;
    private Vector3 startPos;

    public void Select(InputAction.CallbackContext context)
    {
        Debug.Log("Clicked");
        startPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.zero, 5f, groundLayer);

        if(hit)
        {
            Debug.Log("Hit");
            hit.collider.GetComponent<SpriteRenderer>().sprite = tileToBecome;
        }

    }
}
