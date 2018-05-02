using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2DPart3 : Item2DPart
{
    public part3Type part3Type;
    public override void SetItem(ItemParent item)
    {
        base.Start();
        base.SetItem(item);
        Part3Attachment p = (Part3Attachment)item;
        part3Type = p.part3Type;
    }
    public override void OnClickedItem()
    {
        base.OnClickedItem();
    }
}
