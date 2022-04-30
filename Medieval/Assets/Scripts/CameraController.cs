using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed;
    private Controls controls;
    private readonly Vector3 pos; //Unity done readonly, if it works it works :shrug:

    private void Awake()
    {
        controls = new Controls();

        controls.Player.Place.performed += context => CameraNorth(pos);
        controls.Player.Place.canceled += context => StopCamera(pos);
    }

    void Update()
    {

    }

    private void CameraNorth(Vector3 pos)
    {
        pos.y += panSpeed * Time.deltaTime;
        Debug.Log("North");
    }

    private void StopCamera(Vector3 pos)
    {
        pos.y = 0;
    }

    private void OnEnable()
    {
        controls.Player.CameraNorth.Enable();
    }

    private void OnDisable()
    {
        controls.Player.CameraNorth.Disable();
    }
}
