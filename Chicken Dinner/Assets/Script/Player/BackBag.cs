using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBag : MonoBehaviour
{
    SoldierState state;
    //背包容量
    public float capacity = 150;
    public UILabel capacityUi;//配套ui
    //背包当前容量
    public float nowCapacity = 0;
    public UILabel nowCapacityUi;//配套ui
    //下拉框 背包UI
    public UIGrid grid;
    public TweenPosition bagUI;
    //点击背包按钮的声音
    public AudioSource music;
    public AudioClip[] clips;// 0 1开关
    MiniBag miniBag;
    bool flag = false;
    public void OnClickBag()
    {
        if (flag)
        {
            bagUI.PlayReverse();
            music.PlayOneShot(clips[1], 0.08f);
        }
        else
        {
            bagUI.PlayForward();
            miniBag.grid.transform.parent.parent.gameObject.SetActive(false);
            grid.enabled = true;
            music.PlayOneShot(clips[0], 0.08f);
        }
        flag = !flag;
    }
    //捡起东西增大背包容量
    public bool AddNowCapcity(float t)
    {
        if (t + nowCapacity > capacity)
        {
            return false;
        }
        else
        {
            nowCapacity += t;
            nowCapacityUi.text = (int)nowCapacity + "";
        }
        return true;
    }
    //shiyong 
    public void DelNowCapcity(float t)
    {
        nowCapacity -= t;
        nowCapacityUi.text = (int)nowCapacity + "";
    }
    //增加物品
    public void AddItem(Item2D item)
    {
        switch (item.Type)
        {
            case ItemType.weapon:
                SetPickWeapon((Item2DWeapon)item);
                Destroy(item.gameObject);
                for (int j = 0; j < miniBag.itemPool.Count; j++)
                {
                    ItemParent item1 = miniBag.itemPool[j];
                    if (item1.id == item.Id&& ((Item2DWeapon)item).BulletCount == ((Weapon)item1).bulletCount)
                    {
                        Destroy(miniBag.itemPool[j].gameObject);
                        miniBag.itemPool.Remove(miniBag.itemPool[j]);
                        return;

                    }
                }
                break;
            default:
                for (int i = item.Count; i > 0; i--)
                {
                    if (AddNowCapcity(i * item.Weight))
                    {
                        item.DelCount(i);
                        GameObject gb = GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem2D(item.Id);
                        Item2D s = gb.GetComponent<Item2D>();
                        s.SetCount(i);
                        if(item.Type == ItemType.bullet)
                        {
                            if (bulletPool.ContainsKey(((Item2DBullet)s).BulletType1))
                            {
                                bulletPool[((Item2DBullet)s).BulletType1].Count += i;
                            }
                            else
                            {
                                bulletPool.Add(((Item2DBullet)s).BulletType1, (Item2DBullet)s);
                            }
                            SetBullet();
                        }
                        for (int j = 0; j < miniBag.itemPool.Count; j++)
                        {
                            ItemParent item1 = miniBag.itemPool[j];
                            if (item1.id == item.Id && i <= item1.count)
                            {
                                item1.count -= i;
                                if (item1.count == 0)
                                {
                                    Destroy(miniBag.itemPool[j].gameObject);
                                    miniBag.itemPool.Remove(miniBag.itemPool[j]);
                                }
                            }
                        }
                        return;
                    }
                }
                //showmessage

                break;
        }
        grid.enabled = true;
    }
    #region 捡东西
    public WeaponController controller1;
    public WeaponController controller2;
    public Dictionary<BulletType, Item2DBullet> bulletPool = new Dictionary<BulletType, Item2DBullet>();
    public void SetPickWeapon(Item2DWeapon weapon)
    {
        if (!controller1.containGun)
        {
            controller1.SetWeapon(weapon);
            controller1.SetBack();
            return;

        }
        else if (!controller2.containGun)
        {
            controller2.SetWeapon(weapon);
            controller2.SetBack();
            return;

        }
        else
        {
            controller1.SetWeapon(weapon);
            return;

        }
    }
    public Transform back;
    public int handIndex = 3;
    public AttackController c1;
    public void OnClickWeapon(WeaponController c = null)
    {
        c1.CloseAim();
        if (c.containGun)
        {
            if (c.weaponID == handIndex)
            {
                handIndex = 3;
                c.SetBack();
                state.SetHand("handKnife");
            }
            else
            {
                controller1.SetBack();
                controller2.SetBack();
                handIndex = c.weaponID;
                c.SetHand();
                state.SetHand("handGun");
            }
        }
    }
    //捡起子弹 武器 丢弃都需要设置子弹数量
    public void SetBullet()
    {
        foreach (BulletType t in bulletPool.Keys)
        {
            if (t == controller1.BulletType1)
            {
                controller1.SetBullet(bulletPool[t].Count);
            }
            if (t == controller2.BulletType1)
            {
                controller2.SetBullet(bulletPool[t].Count);
            }
        }
    }

    #endregion
    void Start()
    {
        miniBag = GetComponent<MiniBag>();
        state = GetComponent<SoldierState>();
    }
}
