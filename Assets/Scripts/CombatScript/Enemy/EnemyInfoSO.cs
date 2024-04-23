using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Info", menuName = "Enemy/Enemy Info")]
public class EnemyInfoSO : ScriptableObject
{
    public int minHealth;
    public int maxHealth;
    public Sprite sprite;
    public List<Requirement> requirements;
}

public enum Requirement
{
    Exact,
    Prime,
    Odd,
    Even,
    MultipleOf3, // may change to Multiple Of N
}
