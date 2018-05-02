using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2DSniper : Item2D
{
    public sniperType sniperType;
    public int sniperMultiple = -1;
    public override void SetItem(ItemParent item)
    {
        base.Start();
        base.SetItem(item);
        Sniper s = (Sniper)item;
        sniperMultiple = s.sniperMultiple;
        sniperType = s.sniperType;
        GetComponentInChildren<DragSniper>().sniperMultiple = sniperMultiple;
    }
    public override void OnClickedItem()
    {
        base.OnClickedItem();
    }
}
