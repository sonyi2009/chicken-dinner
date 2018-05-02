using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBag : MonoBehaviour
{
    //便于数据修改 背包 音乐
    BackBag backBag;
    public AudioSource music;
    //用于创建物体 物体grid  保存物体　
    public Item2D[] prefabItem;
    //0 1 2 3... 对应weapon food bullet sniper part1,2,3,4
    public UIGrid grid;
    public List<ItemParent> itemPool = new List<ItemParent>();
    //直接清空ui然后刷新方法不好，会造成肉眼可见的物品变化
    // 这两个方法用于检测到的物体添加到UI中
    public void AddItem(ItemParent item)
    {
        grid.transform.parent.parent.gameObject.SetActive(true);
        //查找所有子对象
        foreach (Transform child in grid.transform)
        {
            Item2D item2d = child.GetComponent<Item2D>();
            if (item2d.Id == item.id && item.type != ItemType.weapon)
            {
                //找到一样的则修改数量
                itemPool.Add(item);
                item2d.AddCount(item.count);
                return;
            }
        }
        switch (item.type)
        {
            case ItemType.weapon:
                GameObject gb = NGUITools.AddChild(grid.gameObject, prefabItem[0].gameObject);
                gb.GetComponent<Item2DWeapon>().SetItem(item);
                break;
            case ItemType.food:
                gb = NGUITools.AddChild(grid.gameObject, prefabItem[1].gameObject);
                gb.GetComponent<Item2DFood>().SetItem(item);
                break;
            case ItemType.bullet:
                gb = NGUITools.AddChild(grid.gameObject, prefabItem[2].gameObject);
                gb.GetComponent<Item2DBullet>().SetItem(item);
                break;
            case ItemType.part1:
                gb = NGUITools.AddChild(grid.gameObject, prefabItem[3].gameObject);
                gb.GetComponent<Item2DPart1>().SetItem(item);
                break;
            case ItemType.part2:
                gb = NGUITools.AddChild(grid.gameObject, prefabItem[4].gameObject);
                gb.GetComponent<Item2DPart2>().SetItem(item);
                break;
            case ItemType.part3:
                gb = NGUITools.AddChild(grid.gameObject, prefabItem[5].gameObject);
                gb.GetComponent<Item2DPart3>().SetItem(item);
                break;
            case ItemType.part4:
                gb = NGUITools.AddChild(grid.gameObject, prefabItem[6].gameObject);
                gb.GetComponent<Item2DPart4>().SetItem(item);
                break;
            case ItemType.sniper:
                gb = NGUITools.AddChild(grid.gameObject, prefabItem[7].gameObject);
                gb.GetComponent<Item2DSniper>().SetItem(item);
                break;
        }
        itemPool.Add(item);
        grid.transform.localPosition = new Vector3(38, 55, 0);
    }
    public void DelItem(ItemParent item)
    {
        foreach (Transform child in grid.transform)
        {
            Item2D item2d = child.GetComponent<Item2D>();
            if (item2d.Id == item.id)
            {
                //找到一样的则修改数量
                item2d.DelCount(item.count);
                if (grid.transform.childCount == 0)
                {
                    grid.transform.parent.parent.gameObject.SetActive(false);
                }
                return;
            }
        }
        if (grid.transform.childCount == 0)
        {
            grid.transform.parent.parent.gameObject.SetActive(false);
        }
        itemPool.Remove(item);
        grid.transform.localPosition = new Vector3(38, 55, 0);
    }
    //当物品捡起来的时候，item2d脚本调用的方法
    public void PickItem(Item2D item)
    {
        music.PlayOneShot(item.PickClip,0.2f);
        backBag.AddItem(item);
        grid.enabled = true;
        GetComponent<SoldierState>().SetHandBehavior("isPick");
        GetComponent<Animator>().SetTrigger("onePick");
        if (grid.transform.childCount == 0) grid.transform.parent.parent.gameObject.SetActive(false);
        grid.transform.localPosition = new Vector3(38, 55, 0);
    }
    void Start()
    {
        backBag = GetComponent<BackBag>();
    }
}