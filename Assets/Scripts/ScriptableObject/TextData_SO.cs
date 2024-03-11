using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TextData", menuName = "DataCreate/Text Data/Text")]
public class TextData_SO : ScriptableObject
{
    public List<KeyValueItem<string, string>> items;
}
