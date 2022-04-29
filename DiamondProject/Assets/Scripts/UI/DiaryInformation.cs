using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Season {
    winter,
    fall,
    summer,
    spring

}
[System.Serializable]
public struct DiaryEntry {
    public int entryNumber;
    public TextMesh informationTxtComponent;
    public TextMesh buttonTxtComponent;

    [Header("Text")]
    public string informationText;
    public string buttonMatricule;

    public GameObject buttonGameObj;
    public GameObject informationGameObj;
}
public class DiaryInformation : MonoBehaviour
{
    [SerializeField] private PosterityObject posterity;
    public List<DiaryEntry> diaryEntries = new List<DiaryEntry>();
    private int currentEntryDisplayed = 0;

    public void OnButtonClick(int entryNum) {
        if (currentEntryDisplayed == 0) {
            for (int i = 0; i < diaryEntries.Count; i++) {
                if (diaryEntries[i].entryNumber == entryNum) {
                   // diaryEntries[i].butt
                }
            }
        }
    }

    public void DisplayEntry() {
        
    }
}
