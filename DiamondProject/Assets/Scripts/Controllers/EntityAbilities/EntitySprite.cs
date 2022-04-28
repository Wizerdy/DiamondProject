using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class EntitySprite : MonoBehaviour {
    [System.Serializable]
    public struct SpriteUsingAngles {
        public Sprite sprite;
        public Vector2 angleBound;
        public bool flipped;
    }

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<SpriteUsingAngles> _sprites;

    void Reset() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void LookAt(Vector2 direction) {
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        angle = Tools.PositiveAngle(angle);
        //Debug.Log(angle);
        for (int i = 0; i < _sprites.Count; i++) {
            if (_sprites[i].angleBound.x <= angle && _sprites[i].angleBound.y >= angle) {
                ChangeSprite(_sprites[i].sprite, _sprites[i].flipped);
                return;
            }
        }
        Debug.LogWarning("/!\\ No Sprite at " + angle);
    }

    public void ChangeSprite(Sprite sprite, bool flipped = false) {
        if (_spriteRenderer == null) { return; }
        _spriteRenderer.sprite = sprite;
        _spriteRenderer.flipX = flipped;
    }
}
