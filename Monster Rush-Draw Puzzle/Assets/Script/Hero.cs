using Sirenix.OdinInspector;
using System;
using UnityEngine;

public enum ItemType
{
    Hero, Monster
}
[CreateAssetMenu(fileName = "Item Shop", menuName = "Item Shop")]
[Serializable]
public class Hero : ScriptableObject
{
    public ItemType type;
    public int id;
    public int adToUnlock;
    public string itemName;
    [PreviewField]
    public Sprite preview;
    private static string AdWatched = "AdWatched";
    public static bool IsUnlocked(Hero item)
    {
        if (item.id <= 0) return true;
        if (PlayerPrefs.GetInt(AdWatched + item.type.ToString() + item.id, 0) < item.adToUnlock)
        {
            return false;
        }
        else return true;
    }
    public static int GetAdWatched(Hero item)
    {
        return PlayerPrefs.GetInt(AdWatched + item.type.ToString() + item.id, 0);
    }
    public static void IncreaseAdWatched(Hero item)
    {
        PlayerPrefs.SetInt(AdWatched + item.type.ToString() + item.id, GetAdWatched(item) + 1);
    }

    public static string IdSelected = "IdSelected";
    public static int GetIdSelected(ItemType type)
    {
        return PlayerPrefs.GetInt(IdSelected + type.ToString(), 0);
    }
    public static void SetIdSelected(Hero item)
    {
        PlayerPrefs.SetInt(IdSelected + item.type.ToString(), item.id);
    }

    public static void Unlock(Hero item)
    {
        PlayerPrefs.SetInt(AdWatched + item.type.ToString() + item.id, item.adToUnlock);
        SetIdSelected(item);
    }

    public static bool IsUnlockAll(ItemType type)
    {
        switch (type)
        {
            case ItemType.Hero:
                foreach (var x in GameManager.heroes)
                {
                    if (!IsUnlocked(x))
                        return false;
                }
                break;
            case ItemType.Monster:
                foreach (var x in GameManager.monster)
                {
                    if (!IsUnlocked(x))
                        return false;
                }
                break;
            default:
                break;
        }
        return true;
    }
    public static Hero GetRandomItem(ItemType type)
    {
        if (IsUnlockAll(type)) return null;

        Hero[] items = GameManager.GetListItem(type);
        Hero selectedItem = null;
        do
        {
            selectedItem = items[UnityEngine.Random.Range(0, items.Length)];
        } while (IsUnlocked(selectedItem));

        return selectedItem;
    }
}
