using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour //: Entity
{
    
    [SerializeField] int hp = 10;
    [SerializeField] float red = 0.5f;
    [SerializeField] Image redScreen = null;
    //void Update()
    //{
    //    Move();
    //    Jump();
    //}

    //public override void Move()
    //{
    //    direction = Input.GetAxis("Horizontal");
    //    base.Move();
    //}

    //public override void Jump()
    //{
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        base.Jump();
    //    }
    //}

    //protected override IEnumerator OnJump(float duration)
    //{
    //    return base.OnJump(duration);
    //}

    public void TakeDamage(int hpToAdd) {
        hp += hpToAdd;
        StartCoroutine(RedScreen());
    }

    public bool IsMoving() {
        return true;
    }

    IEnumerator RedScreen() {
        redScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(red);
        redScreen.gameObject.SetActive(false);
    }
}
