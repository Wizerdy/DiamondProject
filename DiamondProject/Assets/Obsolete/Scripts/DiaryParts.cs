using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryParts : MonoBehaviour
{
    public Diary diary;
    public TMP_Text surname;
    public TMP_Text name;
    public Image picture;
    public TMP_Text description;


    public void UpdateDiaryPart(Personage personage) {
        surname.text = personage.surname;
        name.text = personage.name;
        picture.sprite = personage.picture;
        picture.color = personage.color;
        if (description != null) {
            description.text = personage.description;
        }
    }

    public void ChangeDiarySheet() {
        diary.diarySheets.gameObject.SetActive(true);
        diary.UpdateDiarySheets(diary.personagesInDiary[transform.GetSiblingIndex()]);
    }
}
