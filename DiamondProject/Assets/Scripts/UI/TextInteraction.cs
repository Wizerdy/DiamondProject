using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using System;

public class TextInteraction : MonoBehaviour {
    private Regex rx;
    public TextMeshProUGUI tmpText;

    private string currentText;
    private string clickedWord;
    private string[] wordsToMatch;

    void Start()
    {
        rx = new Regex(@"<color=green>(.*?)</color>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }

    public void OnClickEvent() {
        if (wordsToMatch.Length > 0) {
            var wordIndex = TMP_TextUtilities.FindIntersectingWord(tmpText, Input.mousePosition, null);

            if (wordIndex != -1) {
                clickedWord = tmpText.textInfo.wordInfo[wordIndex].GetWord();

                if (Array.Exists(wordsToMatch, element => element == clickedWord))
                    Fungus.Flowchart.BroadcastFungusMessage("StartTextInteraction");
            }
        }

    }

    public void GetWordToClick() {
        Match match = rx.Match(currentText);

        wordsToMatch = match.ToString().Split();

        wordsToMatch[0] = wordsToMatch[0].Replace("<color=green>", "");
        wordsToMatch[wordsToMatch.Length - 1] = wordsToMatch[wordsToMatch.Length - 1].Replace("</color>", "");
    }

    public void GetText() {
        currentText = tmpText.text;
        wordsToMatch = null;
    }
}
