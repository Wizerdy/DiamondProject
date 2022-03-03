using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cube : MonoBehaviour {

    PlayerControls controls;

    Vector2 directionStrength;
    Vector2 rotationStrength;
    float rotationStrengthY;

    private void Awake() {
        controls = new PlayerControls();

        //controls.GamePlay.Grow.performed += cc => Grow();

        //controls.GamePlay.Reduce.performed += cc => Reduce();


        controls.GamePlay.Move.performed += cc => directionStrength = cc.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += cc => directionStrength = Vector2.zero;

        //controls.GamePlay.Rotate.performed += cc => rotationStrength = cc.ReadValue<Vector2>();
        //controls.GamePlay.Rotate.canceled += cc => rotationStrength = Vector2.zero;

        //controls.GamePlay.RotateY.performed += cc => rotationStrengthY = cc.ReadValue<float>();
        //controls.GamePlay.RotateY.canceled += cc => rotationStrengthY = 0;
    }

    private void Update() {
        Move();
        Rotate();
    }
    void Grow() {
        transform.localScale *= 1.1f;
    }

    void Reduce() {
        transform.localScale /= 1.1f;
    }

    void OnEnable() {
        controls.GamePlay.Enable();
    }

    private void OnDisable() {
        controls.GamePlay.Disable();
    }

    void Move() {

        Vector3 move = new Vector3(directionStrength.x, 0, directionStrength.y) * Time.deltaTime;
        transform.Translate(move, Space.World);
    }

    void Rotate() {
        Vector3 rotate = new Vector3(rotationStrength.y, rotationStrengthY, -rotationStrength.x) * 100f * Time.deltaTime;
        transform.Rotate(rotate, Space.World);
    }

}
