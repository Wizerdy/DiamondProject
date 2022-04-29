using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class DiaryManager : MonoBehaviour
{
    [SerializeField] private GameObject diary;

    [SerializeField] private GameObject btnHintTrigger;
    [SerializeField] private GameObject btnHintBoss;
    [SerializeField] private GameObject btnHintCharacter;
    [SerializeField] private GameObject btnCloseDiary;

    [SerializeField] private GameObject hintTrigger;
    [SerializeField] private GameObject hintBoss;
    [SerializeField] private GameObject hintCharacter;

    [SerializeField] private Flowchart flowchart;


    public void EnableDiary() {
        diary.SetActive(true);
    }

    public void DisableDiary() {
        hintTrigger.SetActive(false);
        hintBoss.SetActive(false);
        hintCharacter.SetActive(false);

        btnHintTrigger.SetActive(true);
        btnHintBoss.SetActive(true);
        btnHintCharacter.SetActive(true);

        diary.SetActive(false);
    }

    public void HintTriggerOnClick() {
        if (hintTrigger.activeSelf) {
            hintTrigger.SetActive(false);

            btnHintTrigger.SetActive(true);
            btnHintBoss.SetActive(true);
            btnHintCharacter.SetActive(true);
        }
        else {
            hintTrigger.SetActive(true);

            btnHintTrigger.SetActive(false);
            btnHintBoss.SetActive(false);
            btnHintCharacter.SetActive(false);
        }
    }

    public void HintBosseOnClick() {
        if (hintBoss.activeSelf) {
            hintBoss.SetActive(false);

            btnHintTrigger.SetActive(true);
            btnHintBoss.SetActive(true);
            btnHintCharacter.SetActive(true);
        }
        else {
            hintBoss.SetActive(true);

            btnHintTrigger.SetActive(false);
            btnHintBoss.SetActive(false);
            btnHintCharacter.SetActive(false);
        }
    }

    public void HintCharacterOnClick() {
        if (hintCharacter.activeSelf) {
            hintCharacter.SetActive(false);

            btnHintTrigger.SetActive(true);
            btnHintBoss.SetActive(true);
            btnHintCharacter.SetActive(true);
        }
        else {
            hintCharacter.SetActive(true);

            btnHintTrigger.SetActive(false);
            btnHintBoss.SetActive(false);
            btnHintCharacter.SetActive(false);
        }
    }

    public void GoToHub() {
        flowchart.SendMessage("GoToHub");
    }
}