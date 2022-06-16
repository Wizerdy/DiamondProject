using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VnButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject firstButtonDialog;
    [SerializeField] private GameObject firstButtonDialogHub;
    [SerializeField] private GameObject firstButtonDialogNorna;
    [SerializeField] private GameObject firstButtonDiary;
    [SerializeField] private GameObject firstButtonResponseDiary;

    public void SetFirstButton(GameObject button) {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }
}
