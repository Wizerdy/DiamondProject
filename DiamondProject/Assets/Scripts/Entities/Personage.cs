using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Personage", menuName = "Entities/Personage")]
public class Personage : ScriptableObject
{
    public string surname;
    public string name;
    public Sprite picture;
    public Color color;
    public string description;
}
