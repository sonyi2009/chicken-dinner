using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2D : MonoBehaviour
{
    MiniBag miniBag;
    BackBag backBag;
    //物品id 物品种类 物品数量 重量 捡起，丢弃，使用的声音
    protected int id;
    public int Id
    {
        get
        {
            return id;
        }
    }
    protected string itemName;
    public string ItemName
    {
        get
        {
            return itemName;
        }
    }
    protected ItemType type;
    public ItemType Type
    {
        get
        {
            return type;
        }
    }
    protected int count;
    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
        }
    }
    protected float weight;
    public float Weight
    {
        get
        {
            return weight;
        }
    }
    protected AudioClip pickClip;
    public AudioClip PickClip
    {
        get
        {
            return pickClip;
        }
    }
    protected AudioClip dropClip;
    protected AudioClip DropClip
    {
        get
        {
            return dropClip;
        }
    }
    protected UISprite png;
    protected UILabel itemNameUI;
    protected UILabel shortInfoUI;
    protected UILabel countUI;

    protected virtual void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        miniBag = player.GetComponent<MiniBag>();
        backBag = player.GetComponent<BackBag>();
        png = transform.Find("ItemPng").gameObject.GetComponent<UISprite>();
        itemNameUI = transform.Find("Describe/ItemName").gameObject.GetComponent<UILabel>();
        shortInfoUI = transform.Find("Describe/ItemInfo").gameObject.GetComponent<UILabel>();
        countUI = transform.Find("Num").gameObject.GetComponent<UILabel>();
    }
    public void SetCount(int t)
    {
        count = t;
        countUI.text = "" + count;
        
    }
    public void AddCount(int t = 1)
    {
        count += t;
        countUI.text = "" + count;
        transform.parent.gameObject.GetComponent<UIGrid>().enabled = true;
    }
    public void DelCount(int t = 1)
    {
        count -= t;
        countUI.text = "" + count;
        transform.parent.gameObject.GetComponent<UIGrid>().enabled = true;
        if(count == 0)
        {
            Destroy(this.gameObject);
        }
    }
    public virtual void SetItem(ItemParent item)
    {
        id = item.id;
        itemName = item.itemName;
        count = item.count;
        weight = item.weight;
        type = item.type;
        pickClip = item.pickClip;
        dropClip = item.dropClip;
        png.spriteName = item.uiName;
        itemNameUI.text = item.itemName;
        shortInfoUI.text = item.shortInfo;
        countUI.text = "" + item.count;
        transform.parent.gameObject.GetComponent<UIGrid>().enabled = true;
        GetComponentInChildren<DragItem>().id = item.id;
        GetComponentInChildren<DragItem>().weight = item.weight;
    }
    public virtual void OnClickedItem()
    {
        if (transform.parent.name == "MiniBagGrid")
        {
            miniBag.PickItem(this);
        }
    }
}
