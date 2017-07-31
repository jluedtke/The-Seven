using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour {

    public Stats pStats;
    public PlayerMovement pMovement;

    [Header("Overall Stats")]
    public Text hp;
    public Text energy;
    public Text movement;

    [Header("Character Stats")]
    public Text _str;
    public Text _dex;
    public Text _con;
    public Text _int;
    public Text _wis;
    public Text _cha;

    private void Start()
    {
        InvokeRepeating("UpdateAllStats", 0f, .1f);
    }

    void UpdateAllStats()
    {
        float currentMovement = pStats.moveRange - pMovement.counter;

        //Overall Stats
        hp.text = pStats.currentHP.ToString() + "/" + pStats.maxHP.ToString();
        energy.text = pStats.currentRS.ToString() + "/" + pStats.maxRS.ToString();
        movement.text = currentMovement.ToString() + "/" + pStats.moveRange.ToString();

        //Character Stats
        _str.text = pStats.STR.ToString();
        _dex.text = pStats.DEX.ToString();
        _con.text = pStats.CON.ToString();
        _int.text = pStats.INT.ToString();
        _wis.text = pStats.WIS.ToString();
        _cha.text = pStats.CHA.ToString();

    }
}
