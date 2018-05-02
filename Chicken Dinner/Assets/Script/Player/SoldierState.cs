using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
    Idle,
    isWalk_w,
    isWalk_a,
    isWalk_s,
    isWalk_d,
    isRun,
    isJump,
    isCured,
    isDead
}
public enum HandBehavior
{
    none,
    isAttack,
    isAttacked,
    isReLoad,
    isThrow,
    isPick,
}
public enum HandThing {
    handKnife,
    handGun,
    handGrenade
}
public enum Poise
{
    isStand,
    isSquat,
    isFall
}

public class SoldierState : MonoBehaviour
{
    Animator animator;
    BackBag bag;
    public State soldierState = State.Idle;
    public HandThing soldierHand = HandThing.handKnife;
    public Poise soldierPoise = Poise.isStand;
    public HandBehavior handBehavior = HandBehavior.none;
    //血量 当前血量 血量UI
    public int hp;
    public int currenthp;
    public UISlider hpUI;
    void Start() {
        animator = GetComponent<Animator>();
        bag = GetComponent<BackBag>();
    }
    public void SetState(string state)
    {
        soldierState = (State)Enum.Parse(typeof(State), state);
        foreach (State s in Enum.GetValues(typeof(State)))
        {
            if (soldierState == State.isDead)
            {
                GetComponent<SoldierMover>().enabled = false;
            }
            animator.SetBool(Enum.GetName(typeof(State), s), false);
            if (soldierState == s)
            {
                animator.SetBool(Enum.GetName(typeof(State), s), true);
            }
        }
    }
    public void SetHand(string hand) {
        animator.SetTrigger("ff");
        soldierHand = (HandThing)Enum.Parse(typeof(HandThing), hand);
        foreach (HandThing s in Enum.GetValues(typeof(HandThing)))
        {
            animator.SetBool(Enum.GetName(typeof(HandThing), s), false);
            if (soldierHand == s)
            {
                animator.SetBool(Enum.GetName(typeof(HandThing), s), true);
            }
        }
    }
    public void SetPoise(string poise)
    {
        soldierPoise = (Poise)Enum.Parse(typeof(Poise), poise);
        foreach (Poise s in Enum.GetValues(typeof(Poise)))
        {
            animator.SetBool(Enum.GetName(typeof(Poise), s), false);
            if (soldierPoise == s)
            {
                animator.SetTrigger(Enum.GetName(typeof(Poise), s).Substring(2));
                animator.SetBool(Enum.GetName(typeof(Poise), s), true);
            }
        }
    }
    public void SetHandBehavior(string behavior)
    {
        handBehavior = (HandBehavior)Enum.Parse(typeof(HandBehavior), behavior);
        foreach (HandBehavior s in Enum.GetValues(typeof(HandBehavior)))
        {
            animator.SetBool(Enum.GetName(typeof(HandBehavior), s), false);
            if (handBehavior == s)
            {
                animator.SetBool(Enum.GetName(typeof(HandBehavior), s), true);
            }
        }
    }
    #region 通过吃东西回复血量
    Item2DFood food = null;
    public UISlider energy;//能量条
    public UISprite cricle;
    float timer = 0f;
    //点了食物
    public void EatFood(Item2DFood food)
    {
        if(this.food != null)
        {
            CancelEat();
        }
        this.food = food;
        food.Sprite.gameObject.SetActive(true);
        cricle.transform.parent.gameObject.SetActive(true);
        cricle.transform.Find("Label").GetComponent<UILabel>().text = "正在使用" + food.ItemName;
        InvokeRepeating("EatingFood", 0, 0.01f);
    }
    //一直吃东西
    void EatingFood()
    {
        if (food.Sprite.value > 0.99f)
        {
            //吃成功了
            cricle.fillAmount = 0;
            food.Sprite.value = 0;
            cricle.transform.parent.gameObject.SetActive(false);
            food.DelCount();
            bag.DelNowCapcity(food.Weight);
            AddHp(food);
            CancelInvoke("EatingFood");
        }
        cricle.fillAmount += 0.01f / food.Timer;
        food.Sprite.value += 0.01f / food.Timer;
        cricle.transform.Find("Timer").GetComponent<UILabel>().text = "" + String.Format("{0:N1} ", (1f - cricle.fillAmount) * food.Timer);
    }
    //取消吃东西
    public void CancelEat()
    {
        cricle.fillAmount = 0;
        food.Sprite.value = 0;
        cricle.transform.parent.gameObject.SetActive(false);
        CancelInvoke("EatingFood");
        //food.transform.parent.parent.localPosition = new Vector3(18, 0, 0);
    }
    #endregion
    public void AddHp(int t)
    {
        if(currenthp +t < hp)
        {
            currenthp += t;
            hpUI.value = currenthp / (float)hp;
        }else
        {
            currenthp = 100;
            hpUI.value = 1;
        }
    }
    public void DelHp(int t)
    {
        if (currenthp - t > 0)
        {
            currenthp -= t;
            hpUI.value = currenthp / (float)hp;
        }else
        {
            currenthp = 0;
            hpUI.value = 0f;
            //死了
        }
    }
    public void AddHp(Item2DFood food)
    {
        if (currenthp + food.Hp < hp)
        {
            currenthp += food.Hp;
            hpUI.value = currenthp / (float)hp;
        }
        if ((energy.value + 0.01f * food.Energy) < 1)
        {
            energy.value += 0.01f * food.Energy;
        }
    }
    void Update()
    {
        if (energy.value > 0)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                energy.value -= 0.01f;
                AddHp(1);
                timer = 0f;
            }
        }
        if (Input.GetKeyDown("x"))
        {
            DelHp(10);
        }
    }
}
