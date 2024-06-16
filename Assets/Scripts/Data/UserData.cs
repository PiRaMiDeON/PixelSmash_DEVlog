using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "User Data", menuName = "Create User Data")]
public class UserData : ScriptableObject
{
    public ClickWeapon ClickWeapon;

    private List<ClickWeapon> _unlockedClickWeapons;

    private List<Location> _unlockedLocations; 

    public void UnlockLocation(Location unlockingLocation)
    {
        _unlockedLocations.Add(unlockingLocation);
    }
    
    public void UnlockWeapon(ClickWeapon unlockingWeapon)
    {
        _unlockedClickWeapons.Add(unlockingWeapon);
    }
}
