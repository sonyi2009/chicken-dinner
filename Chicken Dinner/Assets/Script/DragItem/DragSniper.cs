using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有的物体png都可以移动
//当物品为配件特殊考虑 在png上的info为空，因为已destroy
public class DragSniper : DragItem
{
    public int sniperMultiple = -1;
    public WeaponController now_c;//now_c为空则说明已经放上去了
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
                if (now_c != null)
                    now_c.DropSniper();
                break;
            case "UI Root":
            case "camera":
            case "droplogo":
                if (now_c != null)
                now_c.DropSniper();
                break;
            case "GunImage":
                surface.transform.parent.GetComponent<WeaponController>().AttachSniper(this);
                if (info != null) Destroy(info.gameObject);
                break;
        }
        Destroy(mTrans.gameObject);
    }
}
