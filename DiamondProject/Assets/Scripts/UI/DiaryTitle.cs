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
            if (firstPageTextFall)
                firstPageTextFall.text = hidingText;
            if (bossImageFall)
                bossImageFall.SetActive(false);
            if (bossAttackImageFall)
                bossAttackImageFall.SetActive(false);
        } else {
            if (firstPageTextFall)
                firstPageTextFall.text = unlockedTextFall;
            if (bossImageFall)
                bossImageFall.SetActive(true);
            if (bossAttackImageFall)
                bossAttackImageFall.SetActive(true);
        }

        if (!posterity.sawWinterForm) {
            if (firstPageTextWinter)
                firstPageTextWinter.text = hidingText;
            if (bossImageWinter)
                bossImageWinter.SetActive(false);
            if (bossAttackImageWinter)
                bossAttackImageWinter.SetActive(false);
        } else {
            if (firstPageTextWinter)
                firstPageTextWinter.text = unlockedTextWinter;
            if (bossImageWinter)
                bossImageWinter.SetActive(true);
            if (bossAttackImageWinter)
                bossAttackImageWinter.SetActive(true);
        }
    }

    private void OnEnable() {
        if (!posterity.sawFallForm) {
            if (firstPageTextFall)
                firstPageTextFall.text = hidingText;
            if (bossImageFall)
                bossImageFall.SetActive(false);
            if (bossAttackImageFall)
                bossAttackImageFall.SetActive(false);
        } else {
            if (firstPageTextFall)
                firstPageTextFall.text = unlockedTextFall;
            if (bossImageFall)
                bossImageFall.SetActive(true);
            if (bossAttackImageFall)
                bossAttackImageFall.SetActive(true);
        }

        if (!posterity.sawWinterForm) {
            if (firstPageTextWinter)
                firstPageTextWinter.text = hidingText;
            if (bossImageWinter)
                bossImageWinter.SetActive(false);
            if (bossAttackImageWinter)
                bossAttackImageWinter.SetActive(false);
        } else {
            if (firstPageTextWinter)
                firstPageTextWinter.text = unlockedTextWinter;
            if (bossImageWinter)
                bossImageWinter.SetActive(true);
            if (bossAttackImageWinter)
                bossAttackImageWinter.SetActive(true);
        }
    }
}
