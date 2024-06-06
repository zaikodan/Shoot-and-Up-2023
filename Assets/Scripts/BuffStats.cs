using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuffStats
{
    public static Stat speed = new Stat(1, 0.2f, "Buff Speed");
    public static Stat fireRate = new Stat(1, 0.2f, "Buff Fire Rate");
    public static Stat damage = new Stat(1, 0.2f, "Buff Damage");
    public static Stat heal = new Stat(30, 10f, "Buff Heal");
    //Level 1 = 1.2
    //Level 2 = 1.4
    public static Stat duration = new Stat(5, 1, "Buff Duration");
}
