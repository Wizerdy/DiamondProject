using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape { NEUTRAL, SPRING, SUMMER, FALL, WINTER }

[CreateAssetMenu(menuName = "Boss/Shape")]
public class BossShape : ScriptableObject {
    [SerializeField] Shape _shape = Shape.NEUTRAL;
    [SerializeField] bool _colorSwap = false;
    [SerializeField] Sprite _sprite;
    [SerializeField] Color _red = Color.red;
    [SerializeField] Color _green = Color.green;
    [SerializeField] Color _blue = Color.blue;
    [SerializeField] RuntimeAnimatorController _animator;

    #region Properties

    public Sprite Sprite => _sprite;
    public RuntimeAnimatorController Animator => _animator;
    public Shape Type => _shape;
    public bool ColorSwap => _colorSwap;
    public Color Red => _red;
    public Color Green => _green;
    public Color Blue => _blue;

    #endregion
}
