using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class QTEManager : MonoBehaviour {
    public const byte WRONG_KEY = 0, TIME_OUT = 1;

    public List<KeyCode> keys;

    public QTE qte = null;

    private float timer;

    private string onGuiString = "";

    public static Tools.BasicDelegate<QTE, byte> OnFail;
    public static Tools.BasicDelegate<QTE> OnSuccess;

    public bool QTEIsValid {
        get { return qte != null && qte.IsValid; }
    }

    void Start() {

    }

    void Update() {
        UpdateQTE();
    }

    private void UpdateQTE() {
        if (!QTEIsValid) { return; }

        if (timer > 0) {
            if (QTEIsValid) {
                onGuiString = "Key : " + qte.Key;
            }

            timer -= Time.deltaTime;
            KeyCode key;
            for (int i = 0; i < keys.Count; i++) {
                key = keys[i];
                if (Input.GetKeyDown(key)) {
                    if (qte.IsRightKey(key)) {
                        Success();
                    } else {
                        Fail(WRONG_KEY);
                    }
                }
            }
        } else {
            Fail(TIME_OUT);
        }
    }

    public void Launch(QTE qte) {
        if (QTEIsValid) { Debug.Log(this.qte); Fail(TIME_OUT); }

        qte.Reset();
        this.qte = qte;
        timer = qte.timeToPress;
        Debug.Log("-- Start QTE --");
        Debug.Log(this.qte.Key);
    }

    public void Fail(byte cause = 0) {
        if (!QTEIsValid) { return; }

        if (OnFail != null) {
            OnFail(qte, cause);
        }
        qte.Fail(cause);
        qte.Reset();
        Debug.Log("QTE FAILED : " + (cause == WRONG_KEY ? "Wrong button" : "Time out"));

        qte = null;
        onGuiString = "";
    }

    public void Success() {
        if (!QTEIsValid) { return; }

        qte.keyIndex++;
        if (qte.HasEnded) {
            if (OnSuccess != null) {
                OnSuccess(qte);
            }
            qte.Success();
            qte.Reset();
            qte = null;
            Debug.Log("YOU WON !");
        } else {
            Debug.Log(qte.Key);
        }
        onGuiString = "";
    }

    private void OnGUI() {
        if (!onGuiString.Equals("")) {
            GUI.color = Color.red;
            GUILayout.Label(onGuiString);
        }
    }
}

[System.Serializable]
public class QTE {
    public List<KeyCode> keys;
    [HideInInspector] public int keyIndex;
    public float timeToPress;
    public Tools.BasicDelegate OnSuccess;
    public Tools.BasicDelegate<byte> OnFailure;

    public bool HasEnded {
        get { return keyIndex >= keys.Count; }
    }
    public KeyCode Key {
        get { return keys[keyIndex]; }
    }
    public bool IsValid {
        get { return keys != null && keys.Count >= 0 && keyIndex < keys.Count; }
    }

    public QTE(List<KeyCode> keys, float timeToPress) {
        keyIndex = 0;
        this.keys = keys;
        this.timeToPress = timeToPress;
    }

    public void Reset() {
        keyIndex = 0;
    }

    public bool IsRightKey(KeyCode key) {
        return keys[keyIndex] == key;
    }

    public void Success() {
        if (OnSuccess != null) {
            OnSuccess();
        }
    }

    public void Fail(byte cause) {
        if (OnFailure != null) {
            OnFailure(cause);
        }
    }

    public override string ToString() {
        string debug = "QTE:";
        if (keys.Count <= 0) { return debug += "null"; }
        for (int i = 0; i < keys.Count; i++) {
            debug += keys[i].ToString();
        }
        debug += " " + keyIndex;
        return debug;
    }

}