using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Item/ItemObject", order = 1)]
public class ItemTemplate : ScriptableObject
{
    public float Attack;
    public float Life;

    public Sprite sprite;
}
