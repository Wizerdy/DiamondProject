using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    public List<Personage> personagesInDiary= new List<Personage>();
    public DiaryParts diaryLines;
    public DiaryParts diarySheets;
    public GameObject diaryBody;

    private void Start() {
        UpdateDiaryLines();
    }

    private void UpdateDiaryLines() {
        for (int i = 0; i < personagesInDiary.Count; i++) {
            if (diaryBody.transform.childCount <= i) {
                Instantiate(diaryLines, diaryBody.transform);
            }
            DiaryParts newDirayLines = diaryBody.transform.GetChild(i).gameObject.GetComponent<DiaryParts>();
            newDirayLines.UpdateDiaryPart(personagesInDiary[i]);
            newDirayLines.diary = this;
        }
    }

    public void UpdateDiarySheets(Personage personage) {
        diarySheets.UpdateDiaryPart(personage);
    }

    public void ChangeState() {
        if (diaryBody.activeSelf) {
            diaryBody.SetActive(false);
        } else {
            diaryBody.SetActive(true);
        }
    }

}
