using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class ChangeMap : MonoBehaviour {
    [System.Serializable]
    public struct SpriteByShape {
        public Shape shape;
        public Sprite sprite;
    }

    [SerializeField] BossShapeSystem _shapeSystem;
    [SerializeField] ChangeBossOnShape _changeShape;

    [Header("Renderer")]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<SpriteByShape> _shapes;

    [Header("Transition")]
    [SerializeField] SpriteRenderer _transitionRenderer;
    [SerializeField] float _transitionBound = 60f;
    [SerializeField] float _defaultTransitionTime = 1f;

    Coroutine routine_ChangeSprite;

    private void Awake() {
        //_shapeSystem.OnEnterShape += ChangeSprite;
        _changeShape.OnSpriteChange += ChangeSprite;
    }

    private void OnDestroy() {
        //_shapeSystem.OnEnterShape -= ChangeSprite;
        _changeShape.OnSpriteChange -= ChangeSprite;
    }

    public void ChangeSprite(BossShape shape) {
        ChangeSprite(shape.Type, _defaultTransitionTime);
    }

    public void ChangeSprite(Shape shape, float time) {
        if (routine_ChangeSprite != null) { StopCoroutine(routine_ChangeSprite); }
        routine_ChangeSprite = StartCoroutine(I_ChangeSprite(shape, time));

        IEnumerator I_ChangeSprite(Shape shape, float time) {
            float timer = 0f;
            _transitionRenderer.gameObject.SetActive(true);
            _transitionRenderer.sprite = GetSprite(shape) ?? _spriteRenderer.sprite;
            _transitionRenderer.material.SetFloat("_Radius", 0f);
            while (timer < time) {
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
                float radius = timer.Remap(0f, time, 0f, _transitionBound);
                _transitionRenderer.material.SetFloat("_Radius", radius);
            }
            _transitionRenderer.gameObject.SetActive(false);
            ChangeSprite(shape);
        }
    }

    public void ChangeSprite(Shape shape) {
        _spriteRenderer.sprite = GetSprite(shape) ?? _spriteRenderer.sprite;
    }

    Sprite GetSprite(Shape shape) {
        for (int i = 0; i < _shapes.Count; i++) {
            if (_shapes[i].shape == shape) {
                return _shapes[i].sprite;
            }
        }
        return null;
    }
}
