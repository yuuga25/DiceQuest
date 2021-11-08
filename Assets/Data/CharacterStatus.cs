using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public abstract class CharacterStatus : ScriptableObject
{
    //プレイヤー名
    [SerializeField]
    private string characterName = "";

    //毒状態確認
    [SerializeField]
    private bool isPoisonState = false;

    //マヒ状態確認
    [SerializeField]
    private bool isNumbnessState = false;

    //火炎状態確認
    [SerializeField]
    private bool isFlameState = false;

    //氷結状態確認
    [SerializeField]
    private bool isFreezingState = false;

    //睡眠状態確認
    [SerializeField]
    private bool isSleepState = false;

    //プレイヤーランク
    [SerializeField]
    private int rank = 1;

    //プレイヤー最大HP
    [SerializeField]
    private int maxHp = 100;

    //プレイヤーHP
    [SerializeField]
    private int hp = 100;

    //攻撃力
    [SerializeField]
    private int power = 10;

    //防御力
    [SerializeField]
    private int defense = 10;

    //行動力
    [SerializeField]
    private int movePower = 0;

    //プレイヤースタミナ
    [SerializeField]
    private int stamina = 30;

    //プレイヤー最大スタミナ
    [SerializeField]
    private int staminaMax = 30;

    //所持金
    [SerializeField]
    private int money = 0;

    //魔法石
    [SerializeField]
    private int magicStone = 0;

    //装備サイズ
    [SerializeField]
    private int armorSize = 3;

    public void SetCharacterName(string characterName)//名前
    {
        this.characterName = characterName;
    }
    public string GetCharacterName()
    {
        return characterName;
    }
    public void SetPoisonState(bool poisonFlag)//毒状態
    {
        isPoisonState = poisonFlag;
    }
    public bool IsPoisonState()
    {
        return isPoisonState;
    }
    public void SetNumbness(bool numbnessFlag)//麻痺状態
    {
        isNumbnessState = numbnessFlag;
    }
    public bool IsNumbnessState()
    {
        return isNumbnessState;
    }
    public void SetFlame(bool flameFlag)//火炎状態
    {
        isFlameState = flameFlag;
    }
    public bool IsFlameState()
    {
        return isFlameState;
    }
    public void SetFreezing(bool freezingFlag)//氷結状態
    {
        isFreezingState = freezingFlag;
    }
    public bool IsFreezeingState()
    {
        return isFreezingState;
    }
    public void SetSleep(bool sleepFlag)//睡眠状態
    {
        isSleepState = sleepFlag;
    }
    public bool IsSleepState()
    {
        return isSleepState;
    }
    public void SetRank(int rank)//ランク
    {
        this.rank = rank;
    }
    public int GetRank()
    {
        return rank;
    }
    public void SetMaxHp(int hp)//最大HP
    {
        this.maxHp = hp;
    }
    public int GetMaxHp()
    {
        return maxHp;
    }
    public void SetHp(int hp)//HP
    {
        this.hp = Mathf.Max(0, Mathf.Min(GetMaxHp(), hp));
    }
    public int GetHp()
    {
        return hp;
    }
    public void SetPower(int power)//攻撃力
    {
        this.power = power;
    }
    public int GetPower()
    {
        return power;
    }
    public void SetDefense(int defense)//防御力
    {
        this.defense = defense;
    }
    public int GetDefense()
    {
        return defense;
    }
    public void SetMovePower(int movePower)//行動力
    {
        this.movePower = movePower;
    }
    public int GetMovePower()
    {
        return movePower;
    }
    public void SetStaminaMax(int stamina)//最大スタミナ
    {
        this.staminaMax = stamina;
    }
    public int GetStaminaMax()
    {
        return staminaMax;
    }
    public void SetStamina(int stamina)//スタミナ
    {
        this.stamina = Mathf.Max(0, Mathf.Min(GetStaminaMax(), stamina));
    }
    public int GetStamina()
    {
        return stamina;
    }
    public void SetMoney(int money)//所持金
    {
        this.money = money;
    }
    public int GetMoney()
    {
        return money;
    }
    public void SetMagicStone(int MagicStone)//魔法石
    {
        this.magicStone = MagicStone;
    }
    public int GetMagicStone()
    {
        return magicStone;
    }
    public void SetArmorSize(int armorSize)//装備サイズ
    {
        this.armorSize = armorSize;
    }
    public int GetArmorSize()
    {
        return armorSize;
    }
}
