using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IMeetARealBoss : MonoBehaviour {
    [SerializeField] IMeetARealForm _currentForm;
    [SerializeField] Coroutine _rt_CurrentAttack;

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _animator;

    [SerializeField] UnityEvent<int> OnInvincibility;

    public void ChangeForm(IMeetARealForm newForm) {
        _currentForm = newForm;
        ChangeSprite(_currentForm.Sprite);
        ChangeAnimatorController(_currentForm.AnimatorController);
    }

    public void Attack(BaseAttack attack, Player player, Boss boss, Vector3 aimPosition, int duration) {
        StartCoroutine(attack.Launch(player, boss, aimPosition, duration));
    }

    public void ChangeSprite(Sprite sprite) {
        _spriteRenderer.sprite = sprite;
    }

    public void ChangeAnimatorController(RuntimeAnimatorController animatorController) {
        _animator.runtimeAnimatorController = animatorController;
    }
}
