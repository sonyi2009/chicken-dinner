using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2DBullet : Item2D
{

    BulletType bulletType;
    public BulletType BulletType1
    {
        get
        {
            return bulletType;
        }
    }
    public override void SetItem(ItemParent item)
    {
        base.Start();
        base.SetItem(item);
        Bullet b = (Bullet)item;
        bulletType = b.bulletType;
    }
    public override void OnClickedItem()
    {
        base.OnClickedItem();
    }
}
