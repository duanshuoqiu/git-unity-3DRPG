using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Date",menuName = "Character Stats/Date")]
public class CharacterDate_SO : ScriptableObject
{
    [Header("Stats Info")] 
    public int maxHealth;

    public int currentHealth;
    public int baseDefence;
    public int currentDefence;
}
