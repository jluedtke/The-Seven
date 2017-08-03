using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUnitStats : MonoBehaviour {

    public Text hp;
    public Text ac;
    public Text dmg;
    public Text _name;

    private Stats eStats;
    public GameObject panelStats;

    private SpriteRenderer spriteColor;

    private void Awake()
    {
        spriteColor = GetComponentInChildren<SpriteRenderer>();

        hp = GameObject.Find("Interface/EnemyStats/StatInfo/HP").GetComponent<Text>();
        ac = GameObject.Find("Interface/EnemyStats/StatInfo/AC").GetComponent<Text>();
        dmg = GameObject.Find("Interface/EnemyStats/StatInfo/DMG").GetComponent<Text>();
        _name = GameObject.Find("Interface/EnemyStats/EnemyName").GetComponent<Text>();

        panelStats = GameObject.Find("Interface/EnemyStats");
        
    }

    private void Start()
    {
        panelStats.SetActive(false);
    }

    private void OnMouseEnter()
    {
        panelStats.SetActive(true);

        eStats = GetComponent<Stats>();
        hp.text = eStats.currentHP.ToString();
        ac.text = eStats.AC.ToString();
        dmg.text = eStats.DMG.ToString();
        _name.text = "Prime Soldier";
    }

    private void OnMouseExit()
    {
        panelStats.SetActive(false);
    }
}
