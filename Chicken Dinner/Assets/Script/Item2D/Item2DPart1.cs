using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2DPart1 : Item2DPart
{
    public part1Type part1Type;
    public override void SetItem(ItemParent item)
    {
        base.Start();
        base.SetItem(item);
        Part1Attachment p = (Part1Attachment)item;
        part1Type = p.part1Type;
    }
    public override void OnClickedItem()
    {
        base.OnClickedItem();
    }
}
