using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterDate_SO characterDate;

    #region Read from Data_SO
    public int MaxHealth
    { get { if (characterDate!=null) { return characterDate.maxHealth; }else return 0; }
        set { characterDate.maxHealth = value; }
    }
    
    public int CurrentHealth
    { get { if (characterDate!=null) { return characterDate.currentHealth; }else return 0; } 
        set { characterDate.currentHealth = value; }
    }
    
    public int BaseDefence
    { get { if (characterDate!=null) { return characterDate.baseDefence; }else return 0; } 
        set { characterDate.baseDefence = value; }
    }
    
    public int CurrenDefence
    {
        get { if (characterDate!=null) { return characterDate.currentDefence; }else return 0; }
        set { characterDate.currentDefence = value; }
    }
    #endregion
}
