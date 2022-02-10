using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    [SerializeField]
    int hp = 10;
    void Update()
    {
        Move();
        Jump();
    }

    public override void Move()
    {
        direction = Input.GetAxis("Horizontal");
        base.Move();
    }

    public override void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            base.Jump();
        }
    }

    protected override IEnumerator OnJump(float duration)
    {
        return base.OnJump(duration);
    }

    public void TakeDamage(int hpToAdd) {
        hp += hpToAdd;
    }

    public bool IsMoving() {
        return true;
    }
}
