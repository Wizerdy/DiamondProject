using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToolsBoxEngine;

public class TempPlayerController : MonoBehaviour {

    public float speed = 5f;
    public Transform attackParent = null;
    public GameObject bullet = null;
    public float bulletSpeed = 10f;

    private PlayerControls controls = null;
    private Animator animator = null;
    private Rigidbody2D rb = null;

    private Vector2 facingDirection = Vector2.zero;
    private Vector2 direction = Vector2.zero;
    public bool isAttacking = false;

    public float rangedAttackCooldown = 1f;
    private bool canRangeAttack = true;

    [SerializeField] float red = 0.5f;
    [SerializeField] Image redScreen = null;

    public Vector2 Position {
        get { return rb.position; } set { rb.position = value; }
    }

    public bool IsMoving {
        get { return direction != Vector2.zero; }
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

        controls.Battle.Enable();
        controls.Battle.Attack.performed += cc => Attack(facingDirection);
        controls.Battle.RangedAttack.performed += cc => RangedAttack(facingDirection);

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
        isAttacking = true;
        attackParent.rotation = Quaternion.LookRotation(Vector3.forward, direction.To3D());
        animator.SetTrigger("Attack");
        controls.GamePlay.Disable();
    }

    public void RangedAttack(Vector2 direction) {
        Debug.Log("poiuomhn");
        if (!canRangeAttack) { return; }

        GameObject bull = Instantiate(bullet, transform.position, Quaternion.identity);
        bull.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        canRangeAttack = false;
        StartCoroutine(Tools.Delay(() => canRangeAttack = true, rangedAttackCooldown));
    }

    public void NotAttacking() {
        isAttacking = false;
        controls.GamePlay.Enable();
    }

    public void TakeDamage() {
        StartCoroutine(RedScreen());
    }

    IEnumerator RedScreen() {
        redScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(red);
        redScreen.gameObject.SetActive(false);
    }
}
