using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextInteraction : MonoBehaviour {
    public Text textBox;
    public string currentText;
    public string currentString;
    public string[] words;
    public Regex rx;
    public string wordToFind;

    public int charIndex;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        rx = new Regex(@"<color=green>(.*?)</color>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //ink call fungus block and save
        //fungus block get word then change color + clickable
        //fungus block onclick jump to knot
        //load ink save
    }

    public void SetCurrentString(string str) {
        currentString = str;
    }

    public void SetUpWord(string word) {
        Match match = rx.Match(currentText);
        Debug.Log(match);
    }

    public void SetText(Text text) {
        textBox = text;
        currentText = text.text;
        words = currentText.Split();
    }

    public void PrintPos() {
        if (charIndex >= currentText.Length)
            return;

        TextGenerator textGen = new TextGenerator(currentText.Length);
        Vector2 extents = textBox.gameObject.GetComponent<RectTransform>().rect.size;
        textGen.Populate(currentText, textBox.GetGenerationSettings(extents));

        int newLine = currentText.Substring(0, charIndex).Split('\n').Length - 1;
        int indexOfTextQuad = (charIndex * 4) + (newLine * 4) - 4;
        if (indexOfTextQuad < textGen.vertexCount) {
            Vector3 avgPos = (textGen.verts[indexOfTextQuad].position +
                textGen.verts[indexOfTextQuad + 1].position +
                textGen.verts[indexOfTextQuad + 2].position +
                textGen.verts[indexOfTextQuad + 3].position) / 4f;

            print(avgPos);
            PrintWorldPos(avgPos);
        } else {
            Debug.LogError("Out of text bound");
        }
    }

    public void PrintWorldPos(Vector3 testPoint) {
        Vector3 worldPos = textBox.transform.TransformPoint(testPoint);
        print(worldPos);
        new GameObject("point").transform.position = worldPos;
        Debug.DrawRay(worldPos, Vector3.up, Color.red, 50f);
    }

    public void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 100, 80), "Test")) {
            PrintPos();
        }
    }
}
