using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    private int STRmod;
    private int DEXmod;
    private int CONmod;
    private int INTmod;
    private int WISmod;
    private int CHAmod;

   
    
    public List<int> GetModifiers(int STR, int DEX,int CON, int INT, int WIS, int CHA)
    {
        List<int> stats = new List<int> { STR, DEX, CON, INT, WIS, CHA };
        List<int> statMods = new List<int> { STRmod, DEXmod, CONmod, INTmod, WISmod, CHAmod };

        for (int i = 0; i < stats.Count; i++)
        {
            statMods[i] = Mathf.FloorToInt(stats[i] / 2 - 5);
        }
        STRmod = statMods[0];
        DEXmod = statMods[1];
        CONmod = statMods[2];
        INTmod = statMods[3];
        WISmod = statMods[4];
        CHAmod = statMods[5];
        return statMods;
    }

    public int GetAC()
    {
        return 10 + DEXmod;
    }

    public int GetMaxHP()
    {
        return CONmod;
    }

    public int GetMaxRS()
    {
        return WISmod;
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
