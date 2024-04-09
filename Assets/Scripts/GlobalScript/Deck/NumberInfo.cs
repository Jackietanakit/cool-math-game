using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberInfo : MonoBehaviour
{
    // TODO: Create the logic to give player information
    public string Name;
    public int Value;
    public List<string> infos;

    public NumberInfo(string name, int value, List<string> infos)
    {
        Name = name;
        Value = value;
        this.infos = infos;
    }
}
