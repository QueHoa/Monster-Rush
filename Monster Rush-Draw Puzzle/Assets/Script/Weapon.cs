using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTypeString
{
    public static string LongGun = "Long gun";
    public static string ShortMelee = "Short melee";
    public static string LongMelee = "Long melee";
    public static string Handgun = "Handgun";
}
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public WeaponType type;
    public string weaponName;
    [PreviewField]
    public Sprite blue, red, yellow;


    public string WeaponName()
    {
        return weaponName;
    }
    public string GetFullSkinPath(GameColor color)
    {
        return $"Weapon/{GetStrType(type)}/{name}/{name}_{color.ToString().ToLower()}";
    }
    public string GetAttackAnimationName()
    {
        if (type == WeaponType.Handgun)
        {
            return $"attack_handgun";
        }
        if (type == WeaponType.LongMelee)
        {
            return $"attack_longmelee";
        }
        if (type == WeaponType.ShortMelee)
        {
            return $"attack_shortmelee";
        }
        return $"attack_longgun_{weaponName}";
    }
    public float TimeAttack()
    {
        if (type == WeaponType.Handgun || type == WeaponType.LongMelee)
            return 1f;
        return 2.3f;
    }
    public Sprite GetSprite(GameColor color)
    {
        switch (color)
        {
            case GameColor.Blue:
                return blue;
            case GameColor.Red:
                return red;
            case GameColor.Yellow:
                return yellow;
            default:
                return null;
        }
    }
    private static string GetStrType(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.LongGun:
                return WeaponTypeString.LongGun;
            case WeaponType.ShortMelee:
                return WeaponTypeString.ShortMelee;
            case WeaponType.LongMelee:
                return WeaponTypeString.LongMelee;
            case WeaponType.Handgun:
                return WeaponTypeString.Handgun;
            default:
                return null;
        }
    }
}
