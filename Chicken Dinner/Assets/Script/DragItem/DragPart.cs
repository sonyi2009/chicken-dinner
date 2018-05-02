using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有的物体png都可以移动
//当物品为配件特殊考虑 在png上的info为空，因为已destroy
public class DragPart : DragItem
{
    public float move_h, move_v, timer, volum;
    public int capcity;
    public Vector3 fire;

    //设置属性方法
    public void SetPart(DragPart t)
    {
        this.id = t.id;
        this.info = t.info;
        this.move_h = t.move_h;
        this.move_v = t.move_v;
        this.timer = t.timer;
        this.volum = t.volum;
        this.capcity = t.capcity;
        this.fire = t.fire;
    }
    public void SetPart(int id, Item2D info, float move_h, float move_v, float timer, float volum, int capcity, Vector3 fire)
    {
        this.id = id;
        this.info = info;
        this.move_h = move_h;
        this.move_v = move_v;
        this.timer = timer;
        this.volum = volum;
        this.capcity = capcity;
        this.fire = fire;
    }
    public WeaponController now_c;//now_c为空则说明已经放上去了
    public ItemType type;
    protected override void OnDragDropRelease(GameObject surface)
    {
        switch (surface.name)
        {
            case "Item(Clone)":
            case "Item1(Clone)":
            case "Item2(Clone)":
            case "Item3(Clone)":
            case "Item4(Clone)":
            case "Item5(Clone)":
            case "Item6(Clone)":
            case "Item7(Clone)":
            case "ItemPng":
            case "BackBag":
                if (info == null)
                {
                    switch (type)
                    {
                        case ItemType.part1:
                            now_c.DropPart1();
                            break;
                        case ItemType.part2:
                            now_c.DropPart2();
                            break;
                        case ItemType.part3:
                            now_c.DropPart3();
                            break;
                        case ItemType.part4:
                            now_c.DropPart4();
                            break;
                    }
                }
                break;
            case "UI Root":
            case "camera":
            case "droplogo":
                if (info == null)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BackBag>().DelNowCapcity(weight);
                    switch (type) 
                    {
                        case ItemType.part1:
                            info = now_c.DropPart1().GetComponent<Item2D>();
                            break;
                        case ItemType.part2:
                            info = now_c.DropPart2().GetComponent<Item2D>();
                            break;
                        case ItemType.part3:
                            info = now_c.DropPart3().GetComponent<Item2D>();
                            break;
                        case ItemType.part4:
                            info = now_c.DropPart4().GetComponent<Item2D>();
                            break;
                    }
                }
                GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem(id);
                Destroy(info.gameObject);
                break;
            case "GunImage":
                //如果是在武器上面的则先卸下来
                //还要把武器上面对应的卸载下来
                if (now_c != null)
                {
                    switch (type)
                    {
                        case ItemType.part1:
                            info = now_c.DropPart1().GetComponent<Item2D>();
                            surface.transform.parent.GetComponent<WeaponController>().DropPart1();
                            break;
                        case ItemType.part2:
                            info = now_c.DropPart2().GetComponent<Item2D>();
                            surface.transform.parent.GetComponent<WeaponController>().DropPart2();
                            break;
                        case ItemType.part3:
                            info = now_c.DropPart3().GetComponent<Item2D>();
                            surface.transform.parent.GetComponent<WeaponController>().DropPart3();
                            break;
                        case ItemType.part4:
                            info = now_c.DropPart4().GetComponent<Item2D>();
                            surface.transform.parent.GetComponent<WeaponController>().DropPart4();
                            break;
                    }
                }
                if (info.Type == ItemType.part1 && surface.transform.parent.GetComponent<WeaponController>().Part1Type1 == ((Item2DPart1)info).part1Type)
                {
                    surface.transform.parent.GetComponent<WeaponController>().AttachPart1(this);
                    Destroy(info.gameObject);
                }
                else if (info.Type == ItemType.part2 && surface.transform.parent.GetComponent<WeaponController>().Part2Type2 == ((Item2DPart2)info).part2Type)
                {
                    surface.transform.parent.GetComponent<WeaponController>().AttachPart2(this);
                    Destroy(info.gameObject);
                }
                else if (info.Type == ItemType.part3 && surface.transform.parent.GetComponent<WeaponController>().Part3Type3 == ((Item2DPart3)info).part3Type)
                {
                    surface.transform.parent.GetComponent<WeaponController>().AttachPart3(this);
                    Destroy(info.gameObject);
                }
                else if (info.Type == ItemType.part4 && surface.transform.parent.GetComponent<WeaponController>().Part4Type4 == ((Item2DPart4)info).part4Type)
                {
                    surface.transform.parent.GetComponent<WeaponController>().AttachPart4(this);
                    Destroy(info.gameObject);
                }
                break;
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<BackBag>().grid.enabled = true;
        Destroy(mTrans.gameObject);
    }
}
