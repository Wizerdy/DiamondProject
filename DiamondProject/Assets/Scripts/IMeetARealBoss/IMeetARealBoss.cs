using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IMeetARealBoss : MonoBehaviour {
    [SerializeField] Health _health;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _animator;

    [SerializeField] UnityEvent<int> _onInvincibility;

    #region Properties

    public Sprite Sprite { get => _spriteRenderer?.sprite; set => ChangeSprite(value); }
    public RuntimeAnimatorController Animator { get => _animator.runtimeAnimatorController; set => ChangeAnimatorController(value); }

    public event UnityAction<int> OnInvincibility { add => _onInvincibility.AddListener(value); remove => _onInvincibility.RemoveListener(value); }

    #endregion

    public void ChangeSprite(Sprite sprite) {
        _spriteRenderer.sprite = sprite;
    }

    public void ColorSwap(Color red, Color green, Color blue) {
        Material mat = _spriteRenderer.material;
        mat.SetColor("_Red", red);
        mat.SetColor("_Green", green);
        mat.SetColor("_Blue", blue);
    }

    public void ChangeAnimatorController(RuntimeAnimatorController animatorController) {
        _animator.runtimeAnimatorController = animatorController;
    }

    public void Death() {
        gameObject.SetActive(false);
    }
}
