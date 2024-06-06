using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public static class PlayerStats
{

    public static string playerName = PlayerPrefs.GetString("PlayerName");

    public static Stat health = new Stat(0, 10, "Health");
    //valueBase = 100
    //name = Health
    //PlayerPref = HealthLevel
    //level = 1
    //valueByLevel = 10
    //value = 110

    public static Stat damage = new Stat(2, 1, "Damage");
    public static Stat speed = new Stat(3, 1, "Speed");
    public static Stat fireRate = new Stat(2,2, "Fire Rate");

    
}

public struct Stat
{
    const int priceBase = 50, priceByLevel = 20;
    int price;

    float valueBase;
    string name;
    int level;
    float valueByLevel;
    float value;

    public Stat(float _valueBase, float _valueByLevel, string _name)
    {
        valueBase = _valueBase;
        name = _name;
        if (PlayerPrefs.HasKey(name + "Level"))
        {
            level = PlayerPrefs.GetInt(name + "Level");
        }
        else
        {
            level = 1;
        }
        valueByLevel = _valueByLevel;
        price = priceBase + level * priceByLevel;
        value = valueBase + level * valueByLevel;
    }

    public float Value { get => value; }
    public int Price { get => price; }
    public int Level { get => level;  }
    public string Name { get => name; }

    public void Upgrade()
    {
        level++;
        PlayerPrefs.SetInt(name + "Level", level);
        value = valueBase + level * valueByLevel;
        price = priceBase + level * priceByLevel;
    }
}
