using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats {

    private int STR;
    private int DEX;
    private int CON;
    private int INT;
    private int WIS;
    private int CHA;

    private int STRmod;
    private int DEXmod;
    private int CONmod;
    private int INTmod;
    private int WISmod;
    private int CHAmod;

    private string _class;


    // Use this for initialization
    void Start () {
        PopulateStats();
        GetModifiers();
        GetStats();
	}

    public void PopulateStats()
    {
        List<int> stats = new List<int> { STR, DEX, CON, INT, WIS, CHA };
        for (int i = 0; i < stats.Count; i++)
        {
            stats[i] = 12; //change later
        }
    }

    void GetModifiers()
    {
        List<int> stats = new List<int> { STR, DEX, CON, INT, WIS, CHA };
        List<int> statMods = new List<int> { STRmod, DEXmod, CONmod, INTmod, WISmod, CHAmod };

        for (int i = 0; i < stats.Count; i++)
        {
            statMods[i] = Mathf.FloorToInt(stats[i] / 2 - 5);
        }
    }

    void GetStats()
    {
        GetAC();
        GetMaxHP();
        GetInitiative();
        GetMoveRange();
    }

    public int GetAC()
    {
        return 10 + DEXmod;
    }

    public int GetMaxHP()
    {
        // use bonus' on actual enemy class
        return CONmod;
    }

    public int GetInitiative()
    {
        if (DEXmod >= INTmod)
            return DEXmod;
        return INTmod;
    }

    public int GetMoveRange()
    {
        return DEXmod;
    }

}
