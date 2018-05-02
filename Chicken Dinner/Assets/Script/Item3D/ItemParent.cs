using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParent : MonoBehaviour {
    //所有3d物体的名称 ui图片 信息 重量 数量 类型 id（唯一标识）捡起来的音效 丢下去的声音
    public string itemName;
    public string uiName;
    public string shortInfo;
    public float weight;
    public int count = 1;
    public ItemType type;
    public int id;
    public AudioClip pickClip;
    public AudioClip dropClip;
    void OnTriggerEnter(Collider hit)
    {
        if(hit.tag == "Player")
        {
            hit.GetComponent<MiniBag>().AddItem(this);
        }
    }
    //移除的物品移除字典
    void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player")
        {
            hit.GetComponent<MiniBag>().DelItem(this);
        }
    }
}
