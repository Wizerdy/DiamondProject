using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCharacter : MonoBehaviour
{
    [SerializeField] private Stage stage;
    [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    [SerializeField] private float moveValue = 1f;
    [SerializeField] private float scaleValue = 1f;
    [SerializeField] private float speed = 1f;

    private Vector3 startPos;
    private Vector3 endPos;

    private Vector2 startScale;
    private Vector2 endScale;

    private Character character;

    private float time = 0f;
    private bool isGrowing = true;
    public void GetCharacter() {
        character = stage.CharactersOnStage[0];
        startPos = character.State.holder.transform.localScale;
        Vector3 p = new Vector3(1, 1,1); 
        endPos = character.State.holder.transform.localScale + p;
    }

    // Update is called once per frame
    void Update() {
        if (character) {
            if (!character.State.dimmed) {
                if (isGrowing) {
                    time += Time.deltaTime;
                    float curveTime = time / speed;
                    Debug.Log(curveTime);
                    character.State.holder.transform.localScale = Vector3.Lerp(startPos, endPos, curve.Evaluate(curveTime));

                    if (curveTime > 1)
                        isGrowing = false;
                } else {
                    time -= Time.deltaTime;
                    float curveTime = time / speed;
                    Debug.Log(curveTime);

                    character.State.holder.transform.localScale = Vector3.Lerp(startPos, endPos, curve.Evaluate(curveTime));

                    if (curveTime <= 0.1)
                        isGrowing = true;
                }

            }
        }
    }
}
