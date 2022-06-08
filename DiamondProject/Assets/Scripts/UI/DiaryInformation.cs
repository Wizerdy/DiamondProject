using TMPro;
using UnityEngine;

public enum hintType {
    Trigger, Boss, Character
}
public class DiaryInformation : MonoBehaviour
{
    [SerializeField] private int entryId;
    [SerializeField] private TextMeshProUGUI informationTxtComponent;
    [SerializeField] private PosterityObject posterity;
    [SerializeField] private hintType hintType;
    private string informationText;

    private void Start() {
        switch (hintType) {
            case hintType.Trigger:
                for (int i = 0; i < posterity.triggerHintList.Count; i++) {
                    if (posterity.triggerHintList[i].id == entryId) {
                        if (posterity.triggerHintList[i].isTrigger) 
                            informationText = posterity.triggerHintList[i].unlockedText;
                        else
                            informationText = posterity.triggerHintList[i].defaultText;

                        informationTxtComponent.text = informationText;
                        return;
                    }
                }
                break;
            case hintType.Boss:
                for (int i = 0; i < posterity.bossHintList.Count; i++) {
                    if (posterity.bossHintList[i].id == entryId) {
                        if (posterity.bossHintList[i].isTrigger)
                            informationText = posterity.bossHintList[i].unlockedText;
                        else
                            informationText = posterity.bossHintList[i].defaultText;

                        informationTxtComponent.text = informationText;
                        return;
                    }
                }
                break;
            case hintType.Character:
                for (int i = 0; i < posterity.characterHintList.Count; i++) {
                    if (posterity.characterHintList[i].id == entryId) {
                        if (posterity.characterHintList[i].isTrigger)
                            informationText = posterity.characterHintList[i].unlockedText;
                        else
                            informationText = posterity.characterHintList[i].defaultText;

                        informationTxtComponent.text = informationText;
                        return;
                    }
                }
                break;
            default:
                break;
        }
    }
}
