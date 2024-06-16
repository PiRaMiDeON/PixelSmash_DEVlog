using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Click Weapon", menuName = "Create New Click Weapon")]
public class ClickWeapon : ScriptableObject
{
    public int _clickDamage;
    public int _protectDamage;
    public float _clickReloading;

    public DamageType DamageType;
}

[Flags]
public enum DamageType
{
    Flaming = 1,
    Freezing = 2,
    Poison = 4,
    Lightning = 8,
    Slashing = 16,
    Crushing = 32,
    Stunning = 64,
    Physical = Slashing | Crushing | Stunning,
    Magicial = Flaming | Freezing | Poison | Lightning
};
