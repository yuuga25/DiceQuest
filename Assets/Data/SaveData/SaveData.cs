using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class SaveData : object
{
    //publicデータ
    public string name;
    public int rank;
    public int hp;
    public int movePower;
    public int money;
    public int magicStone;
    public int armorSize;

    //staticデータ
    static int stamina;

    //privateデータ
    private float power;
    private float defense;
}
