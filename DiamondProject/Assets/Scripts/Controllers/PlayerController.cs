using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{
    PlayerControls controls;

    void Update()
    {
        Move();
    }

    private void Awake()
    {
        controls = new PlayerControls();

        controls.GamePlay.Move.performed += cc => direction = cc.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += cc => direction = Vector2.zero;
    }

    public override void Move()
    {
        // Direction value updated ?
        Debug.Log(direction);
        base.Move();
    }

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }
}
