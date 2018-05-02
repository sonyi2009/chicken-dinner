using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartAttachment : ItemParent {
    //抖动h v  弹夹容量（不是弹夹就为0） 1，2，3，4都是除法 5为加法 6为减法
    public float move_h = 1;
    public float move_v = 1;
    public float timer = 1;
    public float volum = 1;
    public int capcity = 0;
    public Vector3 fire = Vector3.zero;
}
