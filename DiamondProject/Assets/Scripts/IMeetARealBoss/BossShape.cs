using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape { NEUTRAL, SPRING, SUMMER, FALL, WINTER }

[CreateAssetMenu(menuName = "Boss/Shape")]
public class BossShape : ScriptableObject {
    [SerializeField] public Shape _shape = Shape.NEUTRAL;
    [SerializeField] Sprite _sprite;
    [SerializeField] RuntimeAnimatorController _animator;

    #region Properties

    public Sprite Sprite => _sprite;
    public RuntimeAnimatorController Animator => _animator;

    #endregion
}
