using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2DPart4 : Item2DPart
{
    public part4Type part4Type;
    public override void SetItem(ItemParent item)
    {
        base.Start();
        base.SetItem(item);
        Part4Attachment p = (Part4Attachment)item;
        part4Type = p.part4Type;
    }
    public override void OnClickedItem()
    {
        base.OnClickedItem();
    }
}
