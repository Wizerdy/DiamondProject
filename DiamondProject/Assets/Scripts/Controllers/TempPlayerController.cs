using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class TempPlayerController : MonoBehaviour {

    public float speed = 5f;
    public Transform attackParent = null;

    private PlayerControls controls = null;
    private Animator animator = null;
    private Rigidbody2D rb = null;

    private Vector2 facingDirection = Vector2.zero;
    private Vector2 direction = Vector2.zero;

    public Vector2 Position {
        get { return rb.position; } set { rb.position = value; }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        controls = new PlayerControls();
        controls.GamePlay.Enable();
        controls.GamePlay.Move.performed += cc => direction = cc.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += cc => direction = Vector2.zero;

        controls.Battle.Attack.Enable();
        controls.Battle.Attack.performed += cc => Attack(facingDirection);

        facingDirection = Vector2.up;
    }

    private void Update() {
        UpdateMovements();
    }

    private void UpdateMovements() {
        Move(direction);
    }

    public void Move(Vector2 direction) {
        direction.Normalize();
        if (direction != Vector2.zero) { facingDirection = direction; }
        Position += direction * speed * Time.deltaTime;
    }

    public void Attack(Vector2 direction) {
        attackParent.rotation = Quaternion.LookRotation(Vector3.forward, direction.To3D());
        animator.SetTrigger("Attack");
    }
}
