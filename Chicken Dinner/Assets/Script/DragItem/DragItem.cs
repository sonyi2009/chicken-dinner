using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有的物体png都可以移动
//当物品为配件特殊考虑 在png上的info为空，因为已destroy
public class DragItem : UIDragDropItem
{
    public int id;
    public float weight;
    public Item2D info;
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        switch (surface.name)
        {
            case "UI Root":
            case "camera":

                break;
            case "droplogo":
                GameObject gb = GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem(id);
                GameObject.FindGameObjectWithTag("Player").GetComponent<BackBag>().DelNowCapcity(weight);
                gb.GetComponent<ItemParent>().count = info.Count;
                break;
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<BackBag>().grid.enabled = true;
        Destroy(mTrans.gameObject);
    }
    protected override void Start()
    {
        base.Start();
        info = GetComponentInParent<Item2D>();
    }
    protected override void OnDragStart()
    {
        if (transform.parent.parent.name == "MiniBagGrid") return;
        base.OnDragStart();
    }
    protected override void OnDragDropStart()
    {
        if (transform.parent.parent.name != "parts")
        {
            GameObject gb = GameObject.Instantiate(gameObject, transform.parent);
            gb.transform.position = transform.position;
            gb.GetComponent<DragItem>().id = id;
        }
        mTrans.parent = transform.root;
        base.OnDragDropStart();
    }
}
