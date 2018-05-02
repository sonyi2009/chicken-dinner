using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2DPart2 : Item2DPart
{
    public part2Type part2Type;
    public override void SetItem(ItemParent item)
    {
        base.Start();
        base.SetItem(item);
        Part2Attachment p = (Part2Attachment)item;
        part2Type = p.part2Type;
    }
    public override void OnClickedItem()
    {
        base.OnClickedItem();
    }
}
