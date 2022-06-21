using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryTitle : MonoBehaviour
{
    [SerializeField] private PosterityObject posterity;
    [SerializeField] private string hidingText = "???";
    [Header("Fall")]
    [SerializeField] private string unlockedTextFall = "Forme automne";
    [SerializeField] private TextMeshProUGUI firstPageTextFall;
    [SerializeField] private GameObject bossImageFall;
    [SerializeField] private GameObject bossAttackImageFall;
    [Header("Winter")]
    [SerializeField] private string unlockedTextWinter = "Forme hiver";
    [SerializeField] private TextMeshProUGUI firstPageTextWinter;
    [SerializeField] private GameObject bossImageWinter;
    [SerializeField] private GameObject bossAttackImageWinter;
    // Start is called before the first frame update
    void Start()
    {
        if (!posterity.sawFallForm) {
            firstPageTextFall.text = hidingText;
            bossImageFall?.SetActive(false);
            bossAttackImageFall?.SetActive(false);
        } else {
            firstPageTextFall.text = unlockedTextFall;
            bossImageFall?.SetActive(true);
            bossAttackImageFall?.SetActive(true);
        }

        if (!posterity.sawWinterForm) {
            firstPageTextWinter.text = hidingText;
            bossImageWinter?.SetActive(false);
            bossAttackImageWinter?.SetActive(false);
        } else {
            firstPageTextWinter.text = unlockedTextWinter;
            bossImageWinter?.SetActive(true);
            bossAttackImageWinter?.SetActive(true);
        }
    }

    private void OnEnable() {
        if (!posterity.sawFallForm) {
            firstPageTextFall.text = hidingText;
            bossImageFall?.SetActive(false);
            bossAttackImageFall?.SetActive(false);
        } else {
            firstPageTextFall.text = unlockedTextFall;
            bossImageFall?.SetActive(true);
            bossAttackImageFall?.SetActive(true);
        }

        if (!posterity.sawWinterForm) {
            firstPageTextWinter.text = hidingText;
            bossImageWinter?.SetActive(false);
            bossAttackImageWinter?.SetActive(false);
        } else {
            firstPageTextWinter.text = unlockedTextWinter;
            bossImageWinter?.SetActive(true);
            bossAttackImageWinter?.SetActive(true);
        }
    }
}
