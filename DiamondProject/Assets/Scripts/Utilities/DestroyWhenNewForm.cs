using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyWhenNewForm : MonoBehaviour {

    public static UnityEvent Triggered;

    UnityAction newForm;
    private void Start() {
        if(Triggered == null)
        Triggered = new UnityEvent();
        newForm += NewForm;
        Triggered.AddListener(newForm);
    }

    public static void Activate() {
        Triggered?.Invoke();
    }
    void NewForm() {
        Destroy(gameObject);
    }
}
