using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "AllyStatus", menuName = "CreateAllyStatus")]
public class AllyStatus : CharacterStatus
{
    //獲得経験値
    [SerializeField]
    private int earnedExperience = 0;
    //装備１
    [SerializeField]
    private EquipmentData equipment1 = null;
    //装備２
    [SerializeField]
    private EquipmentData equipment2 = null;
    //装備３
    [SerializeField]
    private EquipmentData equipment3 = null;
    //アイテムの個数のDictionary
    [SerializeField]
    private ItemDictionary itemDictionary = null;
    //装備と個数のDictionary
    [SerializeField]
    private EquipmentDictionary equipmentDictionary = null;

    public void SetEarnedExperience(int earnedExperience)
    {
        this.earnedExperience = earnedExperience;
    }
    public int GetEarnedExperience()
    {
        return earnedExperience;
    }
    public void SetEquipment1(EquipmentData equipment1)
    {
        this.equipment1 = equipment1;
    }
    public EquipmentData GetEquipment1()
    {
        return equipment1;
    }
    public void SetEquipment2(EquipmentData equipment2)
    {
        this.equipment2 = equipment2;
    }
    public EquipmentData GetEquipment2()
    {
        return equipment2;
    }
    public void SetEquipment3(EquipmentData equipment3)
    {
        this.equipment3 = equipment3;
    }
    public EquipmentData GetEquipment3()
    {
        return equipment3;
    }
    public void CreateItemDictionary(ItemDictionary itemDictionary)
    {
        this.itemDictionary = itemDictionary;
    }
    public void SetItemDictionary(ItemData item, int num = 0)
    {
        itemDictionary.Add(item, num);
    }
    public void CreateEquipmentDictionary(EquipmentDictionary equipmentDictionary)
    {
        this.equipmentDictionary = equipmentDictionary;
    }
    public void SetEquipmentDictionary(EquipmentData equipment, int num = 0)
    {
        equipmentDictionary.Add(equipment, num);
    }


    public ItemDictionary GetItemDictionary()//アイテムが登録された順番のItemDictionaryを返す
    {
        return itemDictionary;
    }
    public IOrderedEnumerable<KeyValuePair<ItemData, int>> GetSortItemDictionary()//平仮名の名前でソートしたItemDictionaryを返す
    {
        return itemDictionary.OrderBy(ItemData => ItemData.Key.GetHiraganaName());
    }
    public int SetItemNum(ItemData tempItem, int num)
    {
        return itemDictionary[tempItem] = num;
    }
    public int GetItemNum(ItemData item)//アイテムの個数を返す
    {
        return itemDictionary[item];
    }


    public EquipmentDictionary GetEquipmentDictionary()//装備が登録された順番のEquipmentDictionaryを返す
    {
        return equipmentDictionary;
    }
    public IOrderedEnumerable<KeyValuePair<EquipmentData, int>> GetSortEquipmentDictionary()//平仮名の名前でソートしたEquipmentDictionaryを返す
    {
        return equipmentDictionary.OrderBy(EquipmentData => EquipmentData.Key.GetHiraganaName());
    }
    public int SetEquipmentNum(EquipmentData tempEquipment, int num)
    {
        return equipmentDictionary[tempEquipment] = num;
    }
    public int GetEquipmentNum(EquipmentData equipment)//装備の個数を返す
    {
        return equipmentDictionary[equipment];
    }
}
