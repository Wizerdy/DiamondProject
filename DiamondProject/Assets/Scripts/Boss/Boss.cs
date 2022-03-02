using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public enum State { TRANSITION, FORMSWITCH, TELEPORT, ROCKFALL, FIREMISSILE, FIREBALL, FIREBOTH, EXPLOSIVROCKFALL, DISMANTLE, FISSURE }
    public enum Form { PASSIVE = 0, FIRST = 1, SECOND = 2, DEAD = 3 }

    private State currentState = State.TRANSITION;
    public Form form = Form.FIRST;

    [SerializeField] Object _attackSelectorFirstFormDummy;
    IAction _attackSelectorFirstForm;
    [SerializeField] Object _attackSelectorSecondFormDummy;
    IAction _attackSelectorSecondForm;
    [SerializeField] List<IAction> _bannedAction = new List<IAction>();
    [SerializeField] BossAction _transition;
    [SerializeField] IAction _formSwitch;
    [SerializeField] IAction _teleport;
    public State CurrentState { get { return currentState; } }
    private void OnValidate() {
        //if(_attackSelectorFirstFormDummy is IAction) {
        //    _attackSelectorFirstForm = (IAction) _attackSelectorFirstFormDummy;
        //} else {
        //    _attackSelectorFirstForm = null;
        //}
        //if (_attackSelectorSecondFormDummy is IAction) {
        //    _attackSelectorSecondForm = (IAction)_attackSelectorSecondFormDummy;
        //} else {
        //    _attackSelectorSecondForm = null;
        //}
        SerializeInterface(ref _attackSelectorFirstForm, ref _attackSelectorFirstFormDummy);
        SerializeInterface(ref _attackSelectorSecondForm, ref _attackSelectorSecondFormDummy);
    }

    private static void SerializeInterface<T1, T2>(ref T2 input, ref T1 dummy) where T1 : class where T2 : class {
        if (dummy is T2) {
            input = dummy as T2;
        } else {
            dummy = null;
        }
    }
    private void Start() {
        NewState();
    }

    #region State
    public void Teleport() {
        if (currentState == State.TELEPORT || currentState == State.FORMSWITCH) { return; }
        _teleport.StartAction();
    }
    public void NewState() {
        switch (form) {
            case Form.FIRST:
                StartCoroutine(_attackSelectorFirstForm.StartAction());
                break;
            case Form.SECOND:
                StartCoroutine(_attackSelectorSecondForm.StartAction());
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

    public void AddBannedAction(BossAction action) {

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