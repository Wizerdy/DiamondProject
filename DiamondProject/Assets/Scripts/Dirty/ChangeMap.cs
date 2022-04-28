using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMap : MonoBehaviour {
    [System.Serializable]
    public struct SpriteByShape {
        public Shape shape;
        public Sprite sprite;
    }

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] List<SpriteByShape> shapes;

    public void ChangeSprite(BossShape shape) {
        ChangeSprite(shape.Type);
    }

    public void ChangeSprite(Shape shape) {
        for (int i = 0; i < shapes.Count; i++) {
            if (shapes[i].shape == shape) {
                spriteRenderer.sprite = shapes[i].sprite;
                return;
            }
        }
    }
}
