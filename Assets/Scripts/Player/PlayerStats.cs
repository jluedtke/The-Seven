using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    
    [Header("Attributes")]
    public int STR;
    public int DEX;
    public int CON;
    public int INT;
    public int WIS;
    public int CHA;

    private int STRmod;
    private int DEXmod;
    private int CONmod;
    private int INTmod;
    private int WISmod;
    private int CHAmod;

    [Header("Stats")]
    public int init;
    public int moveRange;
    public int AC;
    public int maxHP;
    public int currentHP;
    public int maxRS;
    public int currentRS;

    [Header("Class")]
    public Heretic _className;
    //public Gunslinger _className;
    //public RoboMancer _className;
    //public Shadow _className;
    //public Officer _className;
    //public EXOSpecialist _className;

    private CharacterStats charStats;
    private string chosenClass = "Heretic"; //Grab from Game Manager later


    void Start()
    {
        charStats = GameObject.Find("GameManager").GetComponent<CharacterStats>();
        //Priority: Class >> Equipment >> Stats
        GetClass(chosenClass);
        GetEquipment();
        GetStats();

        GetComponent<PlayerMovement>().moveRange = moveRange;
    }

    void GetStats()
    {
        STR += _className.STR;
        DEX += _className.DEX;
        CON += _className.CON;
        INT += _className.INT;
        WIS += _className.WIS;
        CHA += _className.CHA;

        List<int> statMods = charStats.GetModifiers(STR, DEX, CON, INT, WIS, CHA);
        STRmod += statMods[0];
        DEXmod += statMods[1];
        CONmod += statMods[2];
        INTmod += statMods[3];
        WISmod += statMods[4];
        CHAmod += statMods[5];

        AC += charStats.GetAC();
        maxHP += _className.baseHP + charStats.GetMaxHP();
        currentHP = maxHP;
        init += charStats.GetInitiative();
        moveRange += 4 + charStats.GetMoveRange();

        maxRS += _className.baseRS + charStats.GetMaxRS();
        currentRS = maxRS;

    }

    void GetClass(string chosenClass)
    {
        if (chosenClass == "Heretic")
            _className = GameObject.Find("GameManager").GetComponent<Heretic>();
        //if (chosenClass == "Gunslinger")
        //    _className = GetComponent<Gunslinger>();
        //if (chosenClass == "Robomancer")
        //    _className = GetComponent<Robomancer>();
        //if (chosenClass == "Officer")
        //    _className = GetComponent<Officer>();
        //if (chosenClass == "Shadow")
        //    _className = GetComponent<Shadow>();
        //if (chosenClass == "EXO Specialist")
        //    _className = GetComponent<EXOSpecialist>();
    }

    void GetEquipment()
    {
        //Does nothing for now
    }
}
