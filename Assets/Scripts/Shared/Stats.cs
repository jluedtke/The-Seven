﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
    
    [Header("Attributes")]
    public int STR;
    public int DEX;
    public int CON;
    public int INT;
    public int WIS;
    public int CHA;

    public int STRmod;
    public int DEXmod;
    public int CONmod;
    public int INTmod;
    public int WISmod;
    public int CHAmod;

    [Header("Stats")]
    public int init;
    public int moveRange;
    public int AC;
    public int maxHP;
    public int currentHP;
    public int maxRS;
    public int currentRS;
    public int DMG;

    [Header("Class")]
    public Heretic _className;
    //public Gunslinger _className;
    //public RoboMancer _className;
    //public Shadow _className;
    //public Officer _className;
    //public EXOSpecialist _className;

    private CharacterStats charStats;
    private string chosenClass = "Heretic"; //Grab from Game Manager later
    public GameObject gameManager;


    void Start()
    {
        charStats = GameObject.Find("GameManager").GetComponent<CharacterStats>();
        //Priority: Class >> Equipment >> Stats
        GetClass(chosenClass);
        GetEquipment();
        GetStats();

        if (GetComponent<PlayerMovement>())
        {
            GetComponent<PlayerMovement>().moveRange = moveRange;
        }
        else
        {
            GetComponent<EnemyMovement>().moveRange = moveRange;
        }
    }

    private void Update()
    {
        if (currentHP <= 0)
        {
            DestroyObject(gameObject);
        }
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
        DMG += STRmod;

        if (DMG < 0)
            DMG = 1;


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
