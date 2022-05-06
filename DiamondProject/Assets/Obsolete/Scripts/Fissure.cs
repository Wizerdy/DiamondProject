//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Fissure : BossAction, IAction {
//    [SerializeField] public float _fissureSpeed = 1f;
//    [SerializeField] public float _sizeToReach = 1f;

//    [Header("For Prog: ")]
//    [SerializeField] Shockwave shockwave = null;
//    [SerializeField] Shelter shelter = null;
//    [SerializeField] BossBodyReference _body;
//    public override void StartAction() {
//        OnCast?.Invoke();
//        _boss.Instance.ChangeState(GetState());
//        Abime();
//    }
//    void Abime() {
//        Shockwave newShockWave = Instantiate(shockwave.gameObject, _body.Instance.transform.position, Quaternion.identity).GetComponent<Shockwave>();
//        newShockWave.fissure = this;
//    }

//    public override Boss.State GetState() {
//        return Boss.State.FISSURE;
//    }
//}
