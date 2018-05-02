using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : ItemParent
{
    //治疗时间，回复量,恢复能量,ui,数量
    public float timer;
    public int hp;
    public int energy;
    public foodType foodType;
    public AudioClip useClip;
}
