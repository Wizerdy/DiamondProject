using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BookCurlPro;
using UnityEngine.UI;
using Fungus;

public class VnButtonManager : MonoBehaviour
{
    [SerializeField] private DialogInput defaultSayDialog;
    [SerializeField] private DialogInput smallSayDialog;
    [SerializeField] private DialogInput flavorSayDialog;
    [SerializeField] private List<GameObject> listFirstButton = new List<GameObject>();
    private EventSystem eventSystem;

    private void Start() {
        eventSystem = EventSystem.current;
    }
    public void SetFirstButton(GameObject button) {
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(button);
    }

    public void SetBookFirstButton(BookPro book) {
        GameObject firstButton = book.FindFirstButtonDiary();
        if (firstButton) {
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(firstButton);
            var btn = firstButton.GetComponent<Button>();
            btn.Select();
            btn.OnSelect(null);
        }
    }

    public void DisableSayDialog() {
        defaultSayDialog.enabled = false;
        smallSayDialog.enabled = false;
        flavorSayDialog.enabled = false;
    }

    public void EnableSayDialog() {
        defaultSayDialog.enabled = true;
        smallSayDialog.enabled = true;
        flavorSayDialog.enabled = true;
    }

    public void CheckIfMenuActive() {
        for (int i = 0; i < listFirstButton.Count; i++) {
            if (listFirstButton[i].activeSelf) {
                SetFirstButton(listFirstButton[i]);
                return;
            }
        }
    }
}
