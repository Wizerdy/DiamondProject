using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class IMeetARealBoss : MonoBehaviour {
    [SerializeField] Health _health;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _animator;

    [SerializeField] UnityEvent<int> _onInvincibility;
    [SerializeField] UnityEvent _onDeath;

    #region Properties

    public Sprite Sprite { get => _spriteRenderer?.sprite; set => ChangeSprite(value); }
    public RuntimeAnimatorController Animator { get => _animator.runtimeAnimatorController; set => ChangeAnimatorController(value); }

    public event UnityAction<int> OnInvincibility { add => _onInvincibility.AddListener(value); remove => _onInvincibility.RemoveListener(value); }
    public event UnityAction OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }

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
        //gameObject.SetActive(false);
    }

    public void ChangeColor(Color color, float time = 0f) {
        Color startColor = _spriteRenderer.color;
        _spriteRenderer.color = color;
        if (time <= 0f) { return; }
        StartCoroutine(Tools.Delay(() => _spriteRenderer.color = startColor, time));
    }

    public Coroutine MoveTo(Vector2 position, float time) {
        return StartCoroutine(IMoveTo(position, time));

        IEnumerator IMoveTo(Vector2 position, float time) {
            if (time == 0f) { transform.position = transform.Position2D(position); yield break; }
            Vector2 startPosition = transform.position;
            float timePassed = 0f;
            while (timePassed < time) {
                yield return new WaitForEndOfFrame();
                timePassed += Time.deltaTime;
                Vector2 vector = Vector2.Lerp(startPosition, position, timePassed / time);
                //Debug.Log(vector + " .. " + timePassed / time);
                transform.position = transform.position.Override(vector, Axis.X, Axis.Y);
            }
        }
    }

    public void SetAnimatorTrigger(string trigger) {
        if (trigger == "") { return; }
        _animator?.SetTrigger(trigger);
    }
}
