using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class ItemData : ScriptableObject
{
    public enum Type
    {
        HpRecovery,
        PoisonRecovery,
        NumbnessRecovery,
        FlameRecovery,
        FreezingRecovery,
        SleepRecovery,
        AllRecovery,
        PowerUp,
        DefenseUp,
        MovePowerUp,
        PowerDown,
        DefenseDown,
        Damage
    }

    //アイテムの種類
    [SerializeField]
    public Type itemType = Type.HpRecovery;
    //アイテムの漢字名
    [SerializeField]
    private string kanjiName = "";
    //アイテムの平仮名名
    [SerializeField]
    private string hiraganaName = "";
    //アイテム情報
    [SerializeField]
    private string information = "";
    //アイテムのパラメータ
    [SerializeField]
    private int amount = 0;

    
    public Type GetItemType()//アイテムの種類を返す
    {
        return itemType;
    }
    public string GetKanjiName()//アイテムの名前を返す
    {
        return kanjiName;
    }
    public string GetHiraganaName()//アイテムの平仮名の名前を返す
    {
        return hiraganaName;
    }
    public string GetInformation()//アイテム情報を返す
    {
        return information;
    }
    public int GetAmount()//アイテムの強さを返す
    {
        return amount;
    }
}
