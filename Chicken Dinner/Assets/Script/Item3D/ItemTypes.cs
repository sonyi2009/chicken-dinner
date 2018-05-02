using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//武器类型
public enum WeaponType
{
    shotgun,
    sniper_rifle,
    rifle,
    submachine_gun,
    handgun,
}
//食物种类
public enum foodType
{
    none,
    food,
    water,
}
//子弹类型
public enum BulletType
{
    none,
    mm4,
    mm5,
    mm7,
    mm9,
    mm12,
}
//瞄准镜类型
public enum sniperType
{
    //全息 红点 2倍 4倍 8倍
    none,
    sniper_0,
    sniper_1,
    sniper_2,
    sniper_4,
    sniper_8,
}
public enum part1Type
{
    none,
    //狙击补偿，消焰，消声器
    sniper_rifle_1,
    //步枪补偿，消焰，消声器
    rifle_1,
    //手枪补偿，消焰，消声器
    handgun_1,
    //冲锋枪补偿，消焰，消声器
    submachine_gun_1,
    //霰弹枪补偿
    shotgun_1,
}
public enum part2Type
{
    //三角 垂直
    none,
    triangle_grip,
    vertical_grip,
}
public enum part3Type
{
    none,
    //狙击枪快速 扩容 快扩弹夹
    sniper_rifle_1,
    //步枪快速 扩容 快扩弹夹
    rifle_1,
    //手枪快速 扩容 快扩弹夹
    handgun_1,
    //冲锋枪快速 扩容 快扩弹夹
    submachine_gun_1,
    //特殊武器如98k

}
public enum part4Type
{
    none,
    sniper_rifle_1,
}
//物品类型 武器 食物 子弹 防具 武器配件 手榴弹 冷武器
public enum ItemType
{
    weapon,
    food,
    bullet,
    sniper,
    part1,
    part2,
    part3,
    part4,
    grenade,
}

public class ItemTypes : MonoBehaviour {
    public ItemType type = ItemType.weapon;
}
