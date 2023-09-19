using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Minion")]
public class Card : ScriptableObject
{
    public ItemType ItemType;
    public new string name;
    public int numberAds;
    public Sprite charactor;
    public SkeletonDataAsset skeletonDataAsset;
    public string GetItem(ItemType ItemType)
    {
        switch (ItemType)
        {
            case ItemType.Hero:
                return "hero";
            case ItemType.Monster:
                return "monster";
            default:
                return null;
        }
    }
}
