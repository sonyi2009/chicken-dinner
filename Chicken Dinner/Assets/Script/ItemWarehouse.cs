using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWarehouse : MonoBehaviour
{
    public ItemParent[] items;
    public Item2D[] prefabItem;
    public Transform player;
    public BackBag bag;
   //生成地面物体
    public GameObject NewItem(int id)
    {
        GameObject gb = null;
        foreach (ItemParent i in items)
        {
            if (i.id == id)
            {
                gb = Instantiate(i.gameObject, player.position, Quaternion.identity);
            }
        }
        return gb;
    }
    //生成背包物体
    public GameObject NewItem2D(int id)
    {
        GameObject gb = null;
        foreach (ItemParent item in items)
        {
            if (item.id == id)
            {
                switch (item.type)
                {
                    case ItemType.weapon:
                        gb = NGUITools.AddChild(bag.grid.gameObject, prefabItem[0].gameObject);
                        gb.GetComponent<Item2DWeapon>().SetItem(item);
                        break;
                    case ItemType.food:
                        gb = NGUITools.AddChild(bag.grid.gameObject, prefabItem[1].gameObject);
                        gb.GetComponent<Item2DFood>().SetItem(item);
                        break;
                    case ItemType.bullet:
                        gb = NGUITools.AddChild(bag.grid.gameObject, prefabItem[2].gameObject);
                        gb.GetComponent<Item2DBullet>().SetItem(item);
                        break;
                    case ItemType.part1:
                        gb = NGUITools.AddChild(bag.grid.gameObject, prefabItem[3].gameObject);
                        gb.GetComponent<Item2DPart1>().SetItem(item);
                        break;
                    case ItemType.part2:
                        gb = NGUITools.AddChild(bag.grid.gameObject, prefabItem[4].gameObject);
                        gb.GetComponent<Item2DPart2>().SetItem(item);
                        break;
                    case ItemType.part3:
                        gb = NGUITools.AddChild(bag.grid.gameObject, prefabItem[5].gameObject);
                        gb.GetComponent<Item2DPart3>().SetItem(item);
                        break;
                    case ItemType.part4:
                        gb = NGUITools.AddChild(bag.grid.gameObject, prefabItem[6].gameObject);
                        gb.GetComponent<Item2DPart4>().SetItem(item);
                        break;
                    case ItemType.sniper:
                        gb = NGUITools.AddChild(bag.grid.gameObject, prefabItem[7].gameObject);
                        gb.GetComponent<Item2DSniper>().SetItem(item);
                        break;
                }
            }
        }
        return gb;
    }
}
