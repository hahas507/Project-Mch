using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipments", menuName = "Equipments", order = 1)]
public class Equipments : ScriptableObject
{
    public EquipmentType type;
}

public enum EquipmentType
{
    None,
    Weapon,
    Armor,
    Accessory
}