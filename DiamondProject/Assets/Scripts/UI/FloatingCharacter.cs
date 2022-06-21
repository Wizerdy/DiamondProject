using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCharacter : MonoBehaviour
{
    [SerializeField] private Stage stage;
    [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    [SerializeField] private float delay = 1f;
    [SerializeField] private Vector3 offSet = new Vector3(1, 1, 1);
    [SerializeField] private Transform rightPos;
    [SerializeField] private Transform middlePos;

    private Vector3 startPos;
    private Vector3 endPos;
    private Character character;

    private float time = 0f;
    private bool isGrowing = true;
    public void GetCharacter() {
        character = stage.CharactersOnStage[0];
        startPos = character.State.holder.transform.position;
        endPos = character.State.holder.transform.position + offSet;
    }

    public void GetRightPosition() {
        if (character) {
            startPos = rightPos.transform.position;
            endPos = rightPos.transform.position + offSet;
        }
    }

    public void GetMiddlePosition() {
        if (character) {
            startPos = middlePos.transform.position;
            endPos = middlePos.transform.position + offSet;
        }
    }

    // Update is called once per frame
    void Update() {
        if (character) {
            if (!character.State.dimmed) {
                if (isGrowing) {
                    time += Time.deltaTime;
                    float curveTime = time / delay;

                    character.State.holder.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(curveTime));

                    if (curveTime >= 0.9)
                        isGrowing = false;
                } else {
                    time -= Time.deltaTime;
                    float curveTime = time / delay;

                    character.State.holder.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(curveTime));

                    if (curveTime <= 0.1)
                        isGrowing = true;
                }
            }
        }
    }
}
