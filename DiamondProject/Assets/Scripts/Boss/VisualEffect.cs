using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffect : MonoBehaviour {
    struct SpriteColor {
        public Color color;
        public float weight;
        public int id;
        public SpriteColor(Color color, float weight, int id) {
            this.color = color;
            this.weight = weight;
            this.id = id;
        }
    }
    SpriteRenderer sr;
    float damageVisualEffectTime = 0.5f;
    List<SpriteColor> colors = new List<SpriteColor>();


    public void AddColor(Color color, float weight, int id) {
        SpriteColor colorSprite = new SpriteColor(color, weight, 0);
        colors.Add(colorSprite);
        UpdateColor();
    }

    public void AddColor(Color color, float weight, float duration) {
        StartCoroutine(AddTempColor(color, weight, duration));
    }

    public void RemoveColor(int id) {
        for (int i = 0; i < colors.Count; i++) {
            if (colors[i].id == id) { 
                colors.RemoveAt(i);
                i--;
            }
        }
        UpdateColor();
    }
    IEnumerator AddTempColor(Color color, float weight, float duration = 1) { 
        SpriteColor colorSprite = new SpriteColor(color, weight, 0);
        colors.Add(colorSprite);
        UpdateColor();
        yield return new WaitForSeconds(duration);
        colors.Remove(colorSprite);
        UpdateColor();
    }

    void UpdateColor() {
        colors.Sort(BiggerWeightFirst);
        sr.color = colors[0].color;
    }
    int BiggerWeightFirst(SpriteColor a, SpriteColor b) {
        if (a.weight > b.weight) {
            return -1;
        } else {
            return 0;
        }
    }

}
