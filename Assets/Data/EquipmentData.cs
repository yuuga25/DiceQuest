using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName = "Equipment", menuName = "CreateEquipment")]
public class EquipmentData : ScriptableObject
{
    public enum Type1//第一効果
    {
        PowerUp,//攻撃力上昇
        DefenseUp,//防御力上昇
        PowerDown,//攻撃力下降
        DefenseDown,//防御力下降
        MovePowerDescentResistance,//行動力下降耐性
        MovePowerUp,//移動力上昇
        MovePowerDown,//移動力下降
        HpUp,//HP上昇
        HpDown,//HP下降
        PoisonResistance,//毒耐性
        NumbnessResistance,//麻痺耐性
        FlameResistance,//火炎耐性
        FleezingResistance,//氷結耐性
        SleepResistance,//睡眠耐性
        none//なし
    }
    public enum Type2//第ニ効果
    {
        PowerUp,//攻撃力上昇
        DefenseUp,//防御力上昇
        PowerDown,//攻撃力下降
        DefenseDown,//防御力下降
        MovePowerDescentResistance,//行動力下降耐性
        MovePowerUp,//移動力上昇
        MovePowerDown,//移動力下降
        HpUp,//HP上昇
        HpDown,//HP下降
        PoisonResistance,//毒耐性
        NumbnessResistance,//麻痺耐性
        FlameResistance,//火炎耐性
        FleezingResistance,//氷結耐性
        SleepResistance,//睡眠耐性
        none//なし
    }
    public enum Type3//第三効果
    {
        PowerUp,//攻撃力上昇
        DefenseUp,//防御力上昇
        PowerDown,//攻撃力下降
        DefenseDown,//防御力下降
        MovePowerDescentResistance,//行動力下降耐性
        MovePowerUp,//移動力上昇
        MovePowerDown,//移動力下降
        HpUp,//HP上昇
        HpDown,//HP下降
        PoisonResistance,//毒耐性
        NumbnessResistance,//麻痺耐性
        FlameResistance,//火炎耐性
        FleezingResistance,//氷結耐性
        SleepResistance,//睡眠耐性
        none//なし
    }


    //装備効果の種類１
    [SerializeField]
    private Type1 equipment1 = Type1.PowerUp;
    //装備効果の種類２
    [SerializeField]
    private Type2 equipment2 = Type2.PowerUp;
    //装備効果の種類３
    [SerializeField]
    private Type3 equipment3 = Type3.PowerUp;


    //装備の漢字名
    [SerializeField]
    private string kanjiName = "";
    //装備の平仮名名
    [SerializeField]
    private string hiraganaName = "";
    //装備情報
    [SerializeField]
    private string information = "";
    //装備サイズ
    //[SerializeField]
    //private int EquipmentSize = 1;


    //装備のパラメータ１
    [SerializeField]
    private int amount1 = 0;
    //装備のパラメータ２
    [SerializeField]
    private int amount2 = 0;
    //装備のパラメータ３
    [SerializeField]
    private int amount3 = 0;
    

    public Type1 GetEquipmentType1()//装備の種類１を返す
    {
        return equipment1;
    }
    public Type2 GetEquipmentType2()//装備の種類２を返す
    {
        return equipment2;
    }
    public Type3 GetEquipmentType3()//装備の種類３を返す
    {
        return equipment3;
    }
    public string GetKanjiName()//装備の名前を返す
    {
        return kanjiName;
    }
    public string GetHiraganaName()//装備の平仮名の名前を返す
    {
        return hiraganaName;
    }
    public string GetInformation()//装備の情報を返す
    {
        return information;
    }
    public int GetAmout1()//装備のパラメータ１の強さを返す
    {
        return amount1;
    }
    public int GetAmout2()//装備のパラメータ２の強さを返す
    {
        return amount2;
    }
    public int GetAmout3()//装備のパラメータ３の強さを返す
    {
        return amount3;
    }
}
