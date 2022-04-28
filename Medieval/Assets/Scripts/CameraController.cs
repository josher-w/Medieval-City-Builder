using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed;

    void Update()
    {
        Vector3 pos = transform.position;
    }

    private void CameraNorth(InputAction.CallbackContext context, Vector3 pos)
    {
        if(context.performed)
        {
            Debug.Log("Performed");
            pos.y += panSpeed * Time.deltaTime;
        }
        if(context.canceled)
        {
            Debug.Log("Cancelled");
            pos.y = 0;
        }
    }
}
