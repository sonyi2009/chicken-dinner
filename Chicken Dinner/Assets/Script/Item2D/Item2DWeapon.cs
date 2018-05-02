using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2DWeapon : Item2D
{
    string uiName;
    public string UiName
    {
        get
        {
            return uiName;
        }
    }
    //武器种类 子弹种类
    WeaponType weaponType;
    BulletType bulletType;
    public BulletType BulletType1
    {
        get
        {
            return bulletType;
        }
    }
    public string BulletName
    {
        get
        {
            switch (bulletType)
            {
                case BulletType.mm4:
                    return ".45口径子弹";
                case BulletType.mm5:
                    return "5.56毫米子弹";
                case BulletType.mm7:
                    return "7.62毫米子弹";
                case BulletType.mm9:
                    return "9毫米子弹";
                case BulletType.mm12:
                    return "12口径子弹";
            }
            return null;
        }
    }
    //瞄准镜头，枪口，前握把，弹夹，枪托
    sniperType sniperType;
    public sniperType SniperType
    {
        get
        {
            return sniperType;
        }
    }
    part1Type part1Type;
    public part1Type Part1Type
    {
        get
        {
            return part1Type;
        }
    }
    part2Type part2Type;
    public part2Type Part2Type
    {
        get
        {
            return part2Type;
        }
    }
    part3Type part3Type;
    public part3Type Part3Type
    {
        get
        {
            return part3Type;
        }
    }
    part4Type part4Type;
    public part4Type Part4Type
    {
        get
        {
            return part4Type;
        }
    }
    //换弹速度  后备 攻击频率 子弹数量 能否连发 子弹伤害 抖动v，h 最远子弹距离 一次射出最多子弹 武器声音
    float reLoadTime;
    int capcity;
    public int Capcity
    {
        get
        {
            return capcity;
        }
    }
    float rateTime;
    int bulletCount = 0;
    public int BulletCount
    {
        get
        {
            return bulletCount;
        }
    }
    bool isBurst = false;
    public bool IsBurst
    {
        get
        {
            return isBurst;
        }
    }
    float damage = 10f;
    float shake_v;
    float shake_h;
    float distance;
    Vector3 fire;
    public Vector3 Fire
    {
        get
        {
            return fire;
        }
    }
    float timer;
    public float Timer
    {
        get
        {
            return timer;
        }
    }
    float rate;
    public float Rate
    {
        get
        {
            return rate;
        }
    }
    int oneShotCount;
    AudioClip clip;
    public float Shake_v
    {
        get
        {
            return shake_v;
        }
    }
    public float Shake_h
    {
        get
        {
            return shake_h;
        }
    }
    public float Distance
    {
        get
        {
            return distance;
        }
    }
    public int OneShotCount
    {
        get
        {
            return oneShotCount;
        }
    }
    public AudioClip Clip
    {
        get
        {
            return clip;
        }
    }
    int sniperMultiple = -1;
    public int SniperMultiple
    {
        get
        {
            return sniperMultiple;
        }
    }
    public override void SetItem(ItemParent item)
    {
        base.Start();
        base.SetItem(item);
        Weapon w = (Weapon)item;
        uiName = item.uiName;
        weaponType = w.weaponType;
        bulletType = w.bulletType;
        sniperType = w.sniperType;
        part1Type = w.part1Type;
        part2Type = w.part2Type;
        part3Type = w.part3Type;
        part4Type = w.part4Type;
        reLoadTime = w.reLoadTime;
        rateTime = w.rateTime;
        bulletCount = w.bulletCount;
        capcity = w.bulletCapcity;
        isBurst = w.isBurst;
        damage = w.damage;
        shake_v = w.shake_v;
        shake_h = w.shake_h;
        distance = w.distance;
        timer = w.reLoadTime;
        fire = w.fire;
        oneShotCount = w.oneShotCount;
        clip = w.clip;
        rate = w.rateTime;
        sniperMultiple = w.sniperMultiple;
    }
    public override void OnClickedItem()
    {
        base.OnClickedItem();
    }
}
