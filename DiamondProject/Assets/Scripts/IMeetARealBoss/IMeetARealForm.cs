using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMeetARealForm : MonoBehaviour {
    [SerializeField] List<BaseAttack> _attacks;
    [SerializeField] Sprite _sprite;
    [SerializeField] RuntimeAnimatorController _animatorController;
    public Sprite Sprite { get { return _sprite; } }    
    public RuntimeAnimatorController AnimatorController { get { return _animatorController; } }    
}
