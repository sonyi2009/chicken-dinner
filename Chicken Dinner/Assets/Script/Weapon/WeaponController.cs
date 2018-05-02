using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public string uiname;//这里的name是ui名称
    public Transform back;
    public int weaponID;
    //是否有枪 子弹容量 子弹数量 打击伤害 武器声音 抖动v，h 最远子弹距离  声音大小，火焰 一次射出的子弹
    public bool containGun = false;
    int count;
    int num;
    int capcity;
    //float damage;
    AudioClip clip;
    public AudioClip Clip
    {
        get
        {
            return clip;
        }
    }
    BulletType bulletType;
    public float shake_v;
    public float shake_h;
    public float distance;
    public float volum = 1f;
    public Vector3 fire = Vector3.zero;
    public float timer = 1f;
    float rate;
    int oneShotCount;
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
    public float Volum
    {
        get
        {
            return volum;
        }
    }
    public float Timer
    {
        get
        {
            return timer;
        }
    }
    public Vector3 Fire
    {
        get
        {
            return fire;
        }
    }
    public float Rate
    {
        get
        {
            return rate;
        }
    }
    public int OneShotCount
    {
        get
        {
            return oneShotCount;
        }
    }
    public BulletType BulletType1
    {
        get
        {
            return bulletType;
        }
    }
    public BackBag bag;
    //背包内容
    UISprite png;
    sniperType sniperType1;
    part1Type part1Type1;
    part2Type part2Type2;
    part3Type part3Type3;
    part4Type part4Type4;
    UISprite sniper;
    UISprite part1;
    UISprite part2;
    UISprite part3;
    UISprite part4;
    UILabel sc;
    public UILabel Sc
    {
        get
        {
            return sc;
        }
    }
    UILabel nameUI;
    UILabel bulletUI;
    UILabel countUI;
    UILabel numUI;
    UILabel line;

    //界面内的枪械
    public GameObject weapon;
    UISprite png1;
    UILabel count1;
    UILabel line1;
    UILabel num1;
    UISprite frame;
    public UISprite Frame
    {
        get
        {
            return frame;
        }
    }
    public sniperType SniperType1
    {
        get
        {
            return sniperType1;
        }
    }
    public part1Type Part1Type1
    {
        get
        {
            return part1Type1;
        }
    }
    public part2Type Part2Type2
    {
        get
        {
            return part2Type2;
        }
    }
    public part3Type Part3Type3
    {
        get
        {
            return part3Type3;
        }
    }
    public part4Type Part4Type4
    {
        get
        {
            return part4Type4;
        }
    }
    GameObject shot;
    GameObject brust;
    public int sniperMultiple = -1;
    public int sniper_Now = -1;
    private void Start()
    {
        png = transform.Find("GunImage").GetComponent<UISprite>();
        sniper = transform.Find("parts/sniper").GetComponent<UISprite>();
        part1 = transform.Find("parts/part1").GetComponent<UISprite>();
        part2 = transform.Find("parts/part2").GetComponent<UISprite>();
        part3 = transform.Find("parts/part3").GetComponent<UISprite>();
        part4 = transform.Find("parts/part4").GetComponent<UISprite>();
        sc = transform.Find("sc").GetComponent<UILabel>();
        nameUI = transform.Find("Sprite/name").GetComponent<UILabel>();
        bulletUI = transform.Find("Sprite/bullet").GetComponent<UILabel>();
        countUI = transform.Find("Sprite/count").GetComponent<UILabel>();
        numUI = transform.Find("Sprite/num").GetComponent<UILabel>();
        line = transform.Find("Sprite/line").GetComponent<UILabel>();

        png1 = weapon.transform.Find("png").GetComponent<UISprite>();
        count1 = weapon.transform.Find("count").GetComponent<UILabel>();
        line1 = weapon.transform.Find("Label").GetComponent<UILabel>();
        num1 = weapon.transform.Find("num").GetComponent<UILabel>();
        frame = weapon.transform.Find("Frame").GetComponent<UISprite>();
        shot = weapon.transform.Find("Shot").gameObject;
        brust = weapon.transform.Find("Brust").gameObject;
    }
    public void SetWeapon(Item2DWeapon weapon)
    {
        containGun = true;
        uiname = weapon.UiName;
        bulletType = weapon.BulletType1;
        png.spriteName = weapon.UiName;
        clip = weapon.Clip;
        if (weapon.SniperType != sniperType.none)
        {
            sniperType1 = weapon.SniperType;
            sniper.gameObject.SetActive(true);
        }
        if (weapon.Part1Type != part1Type.none)
        {
            part1Type1 = weapon.Part1Type;
            part1.gameObject.SetActive(true);
        }
        if (weapon.Part2Type != part2Type.none)
        {
            part2Type2 = weapon.Part2Type;
            part2.gameObject.SetActive(true);
        }
        if (weapon.Part3Type != part3Type.none)
        {
            part3Type3 = weapon.Part3Type;
            part3.gameObject.SetActive(true);
        }
        if (weapon.Part4Type != part4Type.none)
        {
            part4Type4 = weapon.Part4Type;
            part4.gameObject.SetActive(true);
        }
        nameUI.text = weapon.ItemName;
        bulletUI.text = weapon.BulletName;
        count = weapon.BulletCount;
        capcity = weapon.Capcity;
        countUI.text = weapon.BulletCount + "";
        line.gameObject.SetActive(true);

        png1.spriteName = weapon.UiName.Substring(7);
        count1.text = weapon.BulletCount + "";
        line1.gameObject.SetActive(true);
        bag.SetBullet();
        if (weapon.IsBurst)
        {
            brust.SetActive(false);
        }
        SetBullet(num);
        shake_v = weapon.Shake_v;
        shake_h = weapon.Shake_h;
        distance = weapon.Distance;
        timer = weapon.Timer;
        fire = weapon.Fire;
        oneShotCount = weapon.OneShotCount;
        rate = weapon.Rate;
        sniperMultiple = weapon.SniperMultiple;
        sniper_Now = -1;
        foreach(Transform t in back)
        {
            if(t.name == uiname)
            {
                weaponNow = t.gameObject;
                weaponNow.SetActive(true);
            }
        }
    }
    public void SetBullet(int t)
    {
        num = t;
        numUI.text = num + "";
        num1.text = num + "";
    }
    public bool DelCount(int t = 1)
    {
        if (IsInvoking("ReLoad"))
            return false;
        if (count == 0)
        {
            if (bag.bulletPool.ContainsKey(BulletType1))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<SoldierState>().SetHandBehavior("isReLoad");
                GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().speed = 5f / timer;
                Invoke("ReLoad", timer);
            }
            return false;
        }
        count -= t;
        countUI.text = count + "";
        count1.text = count + "";
        return true;
    }
    void ReLoad()
    {
        if (capcity >= (count + bag.bulletPool[bulletType].Count))
        {
            count += bag.bulletPool[bulletType].Count;
            bag.DelNowCapcity(bag.bulletPool[bulletType].Count * bag.bulletPool[bulletType].Weight);
            bag.bulletPool[bulletType].SetCount(0);
            SetBullet(0);
            Destroy(bag.bulletPool[bulletType].gameObject);
            bag.bulletPool.Remove(bulletType);
            bag.grid.enabled = true;
        }
        else
        {
            print(count + "!!!!" + bag.bulletPool[bulletType].Count);
            bag.bulletPool[bulletType].SetCount(bag.bulletPool[bulletType].Count + count - capcity);
            bag.DelNowCapcity(num * bag.bulletPool[bulletType].Weight);
            count = capcity;
        }
        countUI.text = count + "";
        count1.text = count + "";
        bag.SetBullet();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().speed = 1f;
    }
    public DragPart dragPart1 = null;
    public DragPart dragPart2 = null;
    public DragPart dragPart3 = null;
    public DragPart dragPart4 = null;
    public DragSniper dragSniper1 = null;
    public GameObject DropPart1()
    {
        GameObject gb = null;
        DragPart i = dragPart1;
        if (i != null)
        {
            shake_h *= i.move_h;
            shake_v *= i.move_v;
            volum *= i.volum;
            timer *= i.timer;
            capcity -= i.capcity;
            fire += i.fire;
            bool flag =  bag.AddNowCapcity(i.weight);
            if (flag)
            gb = GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem2D(i.id);
            else
            GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem(i.id);
            Destroy(i.gameObject);
            dragPart1 = null;
        }
        return gb;
    }
    public void AttachPart1(DragPart item)
    {
        //删除自己的东西
        DropPart1();
        shake_h /= item.move_h;
        shake_v /= item.move_v;
        volum /= item.volum;
        timer /= item.timer;
        capcity += item.capcity;
        fire -= item.fire;
        GameObject gb1 = part1.transform.gameObject.AddChild(item.gameObject);
        gb1.GetComponent<DragPart>().SetPart(item);
        gb1.GetComponent<DragPart>().now_c = this;
        dragPart1 = gb1.GetComponent<DragPart>();
        gb1.GetComponent<BoxCollider>().enabled = true;
        gb1.GetComponent<UISprite>().enabled = true;
        gb1.GetComponent<DragPart>().enabled = true;
        bag.DelNowCapcity(item.weight);
    }
    public GameObject DropPart2()
    {
        GameObject gb = null;
        DragPart i = dragPart2;
        if (i != null)
        {
            shake_h *= i.move_h;
            shake_v *= i.move_v;
            volum *= i.volum;
            timer *= i.timer;
            capcity -= i.capcity;
            fire += i.fire;
            bool flag = bag.AddNowCapcity(i.weight);
            if (flag)
                gb = GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem2D(i.id);
            else
                GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem(i.id);
            Destroy(i.gameObject);
            dragPart2 = null;
        }
        return gb;
    }
    public void AttachPart2(DragPart item)
    {
        //删除自己的东西
        DropPart2();
        shake_h /= item.move_h;
        shake_v /= item.move_v;
        volum /= item.volum;
        timer /= item.timer;
        capcity += item.capcity;
        fire -= item.fire;
        GameObject gb1 = part2.transform.gameObject.AddChild(item.gameObject);
        gb1.GetComponent<DragPart>().SetPart(item);
        gb1.GetComponent<DragPart>().now_c = this;
        dragPart2 = gb1.GetComponent<DragPart>();
        gb1.GetComponent<BoxCollider>().enabled = true;
        gb1.GetComponent<UISprite>().enabled = true;
        gb1.GetComponent<DragPart>().enabled = true;
        bag.DelNowCapcity(item.weight);
    }
    public GameObject DropPart3()
    {
        GameObject gb = null;
        DragPart i = dragPart3;
        if (i != null)
        {
            shake_h *= i.move_h;
            shake_v *= i.move_v;
            volum *= i.volum;
            timer *= i.timer;
            capcity -= i.capcity;
            fire += i.fire;
            bool flag = bag.AddNowCapcity(i.weight);
            if (flag)
                gb = GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem2D(i.id);
            else
                GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem(i.id);
            Destroy(i.gameObject);
            dragPart3 = null;
        }
        return gb;
    }
    public void AttachPart3(DragPart item)
    {
        //删除自己的东西
        DropPart3();
        shake_h /= item.move_h;
        shake_v /= item.move_v;
        volum /= item.volum;
        timer /= item.timer;
        capcity += item.capcity;
        fire -= item.fire;
        GameObject gb1 = part3.transform.gameObject.AddChild(item.gameObject);
        gb1.GetComponent<DragPart>().SetPart(item);
        gb1.GetComponent<DragPart>().now_c = this;
        dragPart3 = gb1.GetComponent<DragPart>();
        gb1.GetComponent<BoxCollider>().enabled = true;
        gb1.GetComponent<UISprite>().enabled = true;
        gb1.GetComponent<DragPart>().enabled = true;
        bag.DelNowCapcity(item.weight);
    }
    public GameObject DropPart4()
    {
        GameObject gb = null;
        DragPart i = dragPart4;
        if (i != null)
        {
            shake_h *= i.move_h;
            shake_v *= i.move_v;
            volum *= i.volum;
            timer *= i.timer;
            capcity -= i.capcity;
            fire += i.fire;
            bool flag = bag.AddNowCapcity(i.weight);
            if (flag)
                gb = GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem2D(i.id);
            else
                GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem(i.id);
            Destroy(i.gameObject);
            dragPart4 = null;
        }
        return gb;
    }
    public void AttachPart4(DragPart item)
    {
        //删除自己的东西
        DropPart4();
        shake_h /= item.move_h;
        shake_v /= item.move_v;
        volum /= item.volum;
        timer /= item.timer;
        capcity += item.capcity;
        fire -= item.fire;
        GameObject gb1 = part4.transform.gameObject.AddChild(item.gameObject);
        gb1.GetComponent<DragPart>().SetPart(item);
        gb1.GetComponent<DragPart>().now_c = this;
        dragPart4 = gb1.GetComponent<DragPart>();
        gb1.GetComponent<BoxCollider>().enabled = true;
        gb1.GetComponent<UISprite>().enabled = true;
        gb1.GetComponent<DragPart>().enabled = true;
        bag.DelNowCapcity(item.weight);
    }

    public GameObject DropSniper()
    {
        GameObject gb = null;
        sniper_Now = -1;
        DragSniper i = dragSniper1;
        if(i != null)
        {
            bool flag = bag.AddNowCapcity(i.weight);
            if (flag)
               gb = GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem2D(i.id);
            else
                GameObject.FindGameObjectWithTag("ItemWarehouse").GetComponent<ItemWarehouse>().NewItem(i.id);
            Destroy(i.gameObject);
            dragSniper1 = null;
        }
        foreach(Transform t in weaponNow.transform )
        {
            t.gameObject.SetActive(false);
        }
        return gb;
    }
    public void AttachSniper(DragSniper item)
    {
        DropSniper();
        if (sniperMultiple >= item.sniperMultiple)
        {
            sniper_Now = item.sniperMultiple;
            GameObject gb1 = sniper.transform.gameObject.AddChild(item.gameObject);
            dragSniper1 = gb1.GetComponent<DragSniper>();
            dragSniper1.sniperMultiple = item.sniperMultiple;
            dragSniper1.now_c = this;
            Destroy(item.gameObject);
            gb1.GetComponent<BoxCollider>().enabled = true;
            gb1.GetComponent<UISprite>().enabled = true;
            gb1.GetComponent<DragSniper>().enabled = true;
            bag.DelNowCapcity(item.weight);
            weaponNow.transform.Find(item.sniperMultiple + "").gameObject.SetActive(true);
        }
    }
    public GameObject weaponNow = null;
    //哪怕丢东西换东西，都要先放背后再说
    public void SetBack()
    {
        if (weaponNow != null)
        {
            weaponNow.transform.parent = back;
            weaponNow.transform.localPosition = Vector3.zero;
            weaponNow.transform.localRotation = Quaternion.identity;
            Sc.gameObject.SetActive(false);
            Frame.gameObject.SetActive(false);
        }
    }
    public void SetHand()
    {
        if (weaponNow != null)
        {
            weaponNow.transform.parent = bag.back;
            weaponNow.transform.localPosition = Vector3.zero;
            weaponNow.transform.localRotation = Quaternion.identity;
            Sc.gameObject.SetActive(true);
            Frame.gameObject.SetActive(true);
        }
    }
}
