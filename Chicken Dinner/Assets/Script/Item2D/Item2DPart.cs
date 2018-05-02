using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2DPart : Item2D
{
    //抖动 容量 声音 火焰 时间
    float move_h = 0;
    float move_v = 0;
    int capcity = 0;
    float volum = 1f;
    Vector3 fire = Vector3.zero;
    float timer = 1f;
    public float Timer
    {
        get
        {
            return timer;
        }
    }
    public float Volum
    {
        get
        {
            return volum;
        }
    }
    public Vector3 Fire
    {
        get
        {
            return fire;
        }
    }
    public float Move_h
    {
        get
        {
            return move_h;
        }
    }
    public float Move_v
    {
        get
        {
            return move_v;
        }
    }
    public int Capcity
    {
        get
        {
            return capcity;
        }
    }
    public override void SetItem(ItemParent item)
    {
        base.Start();
        base.SetItem(item);
        PartAttachment p = (PartAttachment)item;
        move_h = p.move_h;
        move_v = p.move_v;
        capcity = p.capcity;
        volum = p.volum;
        fire = p.fire;
        timer = p.timer;
        GetComponentInChildren<DragPart>().SetPart(id,this,move_h, move_v,timer, volum, capcity, fire);
        GetComponentInChildren<DragPart>().type = type;
    }
    public override void OnClickedItem()
    {
        base.OnClickedItem();
    }
}
