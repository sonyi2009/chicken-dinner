using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : ItemParent
{
    //武器种类 子弹种类
    public WeaponType weaponType;
    public BulletType bulletType;
    //瞄准镜头，枪口，前握把，弹夹，枪托
    public sniperType sniperType;
    public part1Type part1Type;
    public part2Type part2Type;
    public part3Type part3Type;
    public part4Type part4Type;
    //换弹速度 攻击频率 子弹数量 能否连发 子弹伤害
    public float reLoadTime;
    public float rateTime;
    public int bulletCount = 0;
    public int bulletCapcity;
    public bool isBurst = false;
    public float damage = 10f;
    //一次射出的子弹 抖动 子弹距离 武器声音
    public int oneShotCount = 1;
    public float shake_v;
    public float shake_h;
    public float distance;
    public Vector3 fire = new Vector3(0.2f,0.2f,0.2f);
    public AudioClip clip;
    //默认放大的方式
    public int sniperMultiple = -1;
}
