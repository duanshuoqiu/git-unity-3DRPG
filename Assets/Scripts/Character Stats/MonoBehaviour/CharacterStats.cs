using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CharacterStats : MonoBehaviour
{
    public CharacterDate_SO characterDate;
    public AttackData_SO attackData;

    [HideInInspector]
    public bool isCritical;
    
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

    #region Character Combat

    public void TakeDamage(CharacterStats attacker,CharacterStats defender)//视频中少了d
    {
        int damage = Mathf.Max(attacker.CurrentDamage() - defender.CurrenDefence,0);
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        
        //TODO:update UI
        //TODO:经验update
    }


    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);

        if (isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
            Debug.Log("暴击!"+coreDamage);
        }
        return (int) coreDamage;
    }
    #endregion
}
