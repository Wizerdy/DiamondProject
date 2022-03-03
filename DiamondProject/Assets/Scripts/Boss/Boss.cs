using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public enum State { TRANSITION, FORMSWITCH, TELEPORT, ROCKFALL, FIREMISSILE, FIREBALL, FIREBOTH, EXPLOSIVROCKFALL, DISMANTLE, FISSURE }
    public enum Form { PASSIVE = 0, FIRST = 1, SECOND = 2, DEAD = 3 }

    private State currentState = State.TRANSITION;
    public Form form = Form.FIRST;

   // [SerializeField] MonoObjectSelectorRandom<BossAction> _attackSelectorFirstFormDummy;
    [SerializeField] MonoBossActionSelectorRandom _attackSelectorFirstForm;
    [SerializeField] List<BossAction> _bannedAction = new List<BossAction>();
    [SerializeField] BossAction _transition;
    [SerializeField] BossAction _formSwitch;
    [SerializeField] BossAction _teleport;
    [SerializeField] List<BossAction> bossActionsCoroutines = new List<BossAction>();
    public State CurrentState { get { return currentState; } }
    //private void OnValidate() {
    //    //if(_attackSelectorFirstFormDummy is IAction) {
    //    //    _attackSelectorFirstForm = (IAction) _attackSelectorFirstFormDummy;
    //    //} else {
    //    //    _attackSelectorFirstForm = null;
    //    //}
    //    //if (_attackSelectorSecondFormDummy is IAction) {
    //    //    _attackSelectorSecondForm = (IAction)_attackSelectorSecondFormDummy;
    //    //} else {
    //    //    _attackSelectorSecondForm = null;
    //    //}
    //    if(_attackSelectorFirstFormDummy != null)
    //    Debug.Log((IAction)_attackSelectorFirstFormDummy + " " + _attackSelectorFirstFormDummy.name);
    //    SerializeInterface(ref _attackSelectorFirstForm, ref _attackSelectorFirstFormDummy);
    //    SerializeInterface(ref _attackSelectorSecondForm, ref _attackSelectorSecondFormDummy);
    //}

    //private static void SerializeInterface<T1, T2>(ref T2 input, ref T1 dummy) where T1 : class where T2 : class {
    //    if (dummy is T2) {
    //        input = dummy as T2;
    //    } else {
    //        dummy = null;
    //    }
    //}
    private void Start() {
        NewState();
    }

    #region State
    public void Teleport() {
        if (currentState == State.TELEPORT || currentState == State.FORMSWITCH) { return; }
        for (int i = 0; i < bossActionsCoroutines.Count; i++) {
            Debug.Log(bossActionsCoroutines[i]);
            bossActionsCoroutines[i].StopAllCoroutines();
        }
        bossActionsCoroutines.Clear();
        _teleport.StartAction();

    }
    public void NewState() {
        switch (form) {
            case Form.FIRST:
                BossAction action = _attackSelectorFirstForm.Get();
                if (action != null) {
                    action.StartAction();
                        bossActionsCoroutines.Add(action);
                } else {
                    _transition.StartAction();
                }
                break;
            case Form.SECOND:
                //StartCoroutine(_attackSelectorSecondForm.StartAction());
                break;
        }
    }

    public void ChangeState(State state) {
        currentState = state;
    }

    public void Transition(float transitionTime) {
        _transition.Duration = transitionTime;
        _transition.StartAction();
    }

    public void EndState(float timebeforeNewState) {
        Transition(timebeforeNewState);
    }

    public void NewWaightAction(BossAction action, float weight ) {
        _attackSelectorFirstForm.NewWeight(action, weight);
    }

    public void RemoveCoroutines(BossAction bossaction) {
        if (bossActionsCoroutines.Contains(bossaction))
        bossActionsCoroutines.Remove(bossaction);
    }

    #endregion

    #region Form
    public void NextForm() {
        if (form == Form.DEAD) { return; }
        form += 1;
        NewForm();
    }

    void NewForm() {

    }
    #endregion
}