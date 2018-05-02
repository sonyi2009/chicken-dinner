using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2DFood : Item2D
{
    float timer;
    public float Timer
    {
        get
        {
            return timer;
        }
    }
    int hp;
    public int Hp
    {
        get
        {
            return hp;
        }
    }
    int energy;
    public int Energy
    {
        get
        {
            return energy;
        }
    }
    foodType foodType;
    AudioClip useClip;
    public AudioClip UseClip
    {
        get
        {
           return useClip;
        }
    }
    UISlider sprite;
    public UISlider Sprite
    {
        get
        {
            return sprite;
        }
    }
    protected override void Start()
    {
        sprite = transform.Find("Use").GetComponent<UISlider>();
    }
    public override void SetItem(ItemParent item)
    {
        base.Start();
        base.SetItem(item);
        Food f = (Food)item;
        timer = f.timer;
        hp = f.hp;
        energy = f.energy;
        foodType = f.foodType;
        useClip = f.useClip;
    }
    public override void OnClickedItem()
    {
        if (transform.parent.name == "BackBagGrid")
        {
            //sprite = transform.Find("Use").GetComponent<UISlider>();
            sprite.transform.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<SoldierState>().EatFood(this);
            transform.parent.parent.localPosition = new Vector3(18, 0, 0);
        }else if (transform.parent.name != "MiniBagGrid")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<SoldierState>().EatFood(this);
        }
        base.OnClickedItem();
    }

}
