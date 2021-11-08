using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemDictionary))]
[CustomPropertyDrawer(typeof(EquipmentDictionary))]
public class SerializableDictionary : SerializableDictionaryPropertyDrawer
{

}
