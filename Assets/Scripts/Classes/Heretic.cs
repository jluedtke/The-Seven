using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heretic : MonoBehaviour {

    [HideInInspector]
    //Base Stats at LV 1
    public int baseHP = 12;
    public string armorType = "Heavy";
    public string weaponType = "Archaic";
    public bool isRanged = false;
    public int baseRS = 3;

    public int STR = 1;
    public int DEX = 0;
    public int CON = 0;
    public int INT = 0;
    public int WIS = 0;
    public int CHA = 1;


}
