using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    //射出射线的摄像机 子弹爆炸 弹孔  武器1，2控制器  武器火焰 丢下的子弹
    public Camera firstView;
    public Camera thirdView;
    public GameObject crossFire;
    public Camera c;
    public GameObject[] bulletExp;
    public GameObject[] bulletHole;
    public AudioSource music;
    public WeaponController c1;
    public WeaponController c2;
    public ParticleSystem fire;
    public GameObject bullet;
    public Transform t;
    public void OnPressButton(int index)
    {
        if (index == 1 && c1.DelCount())
        {
            music.PlayOneShot(c1.Clip, c1.Volum);
            fire.transform.localScale = c1.Fire;
            fire.Play();
            GameObject gb = GameObject.Instantiate(bullet, t);
            gb.transform.localPosition = new Vector3(0.7f, 0.05f, -0.05f);
            gb.GetComponent<Rigidbody>().AddForce(new Vector3(0, -20f, -100f));
            Destroy(gb, 2f);
            for (int i = 0; i < c1.OneShotCount; i++)
            {
                ShotOneBullet(c1.Shake_v, c1.Shake_h, c1.Distance);
            }
        }
        else if (index == 2 && c2.DelCount())
        {
            music.PlayOneShot(c2.Clip, c2.Volum);
            fire.transform.localScale = c2.Fire;
            fire.Play();
            GameObject gb = GameObject.Instantiate(bullet, t);
            gb.transform.localPosition = new Vector3(0.7f, 0.05f, -0.05f);
            gb.GetComponent<Rigidbody>().AddForce(new Vector3(0, -20f, -100f));
            Destroy(gb, 2f);
            for (int i = 0; i < c1.OneShotCount; i++)
            {
                ShotOneBullet(c2.Shake_v, c2.Shake_h, c2.Distance);
            }
        }

    }
    //射出一个子弹
    public void ShotOneBullet(float shake_v, float shake_h, float distance)
    {
        Vector3 rayOrigin = c.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, c.transform.forward, out hit, distance))
        {
            // 检测被射中的对象是否存在rigidbody组件
            Vector3 pos = hit.point;//撞击点
            //pos.y += 0.5f;//向上移动，防止与物体平面重叠
            GameObject exp = GameObject.Instantiate(bulletExp[0]) as GameObject;
            GameObject hole = GameObject.Instantiate(bulletHole[0]) as GameObject;
            exp.transform.position = hit.point;
            hole.transform.position = hit.point;
            //调整贴图的方向
            hole.transform.rotation = player.transform.rotation;
            Destroy(exp, 2f);
            Destroy(hole, 2f);
        }
        float i = Random.Range(-0.3f, 0.6f) > 0 ? shake_h : -shake_h;
        player.GetComponent<ViewController>().RotateView(i, shake_v);
        player.GetComponent<SoldierState>().SetHandBehavior("isAttack");
        player.GetComponent<Animator>().SetTrigger("oneShot");
    }
    float timer = 1f;
    public void CloseAim()
    {
        firstView.depth = -1;
        c = thirdView;
        crossFire.SetActive(true);
        firstView.GetComponent<FirstViewController>().SetView(-1);
    }
    public void OnClickAim()
    {
        firstView.depth = 1;
        c = firstView;
        crossFire.SetActive(false);
        if (player.GetComponent<BackBag>().handIndex == 1)
        {
            firstView.GetComponent<FirstViewController>().SetView(player.GetComponent<BackBag>().controller1.sniper_Now);
        }else if(player.GetComponent<BackBag>().handIndex == 2)
        {
            firstView.GetComponent<FirstViewController>().SetView(player.GetComponent<BackBag>().controller2.sniper_Now);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && player.GetComponent<BackBag>().handIndex == 1)
        {
            timer += Time.deltaTime;
            if (timer > c1.Rate)
            {
                OnPressButton(1);
                timer = 0f;
            }
        }
        else if (Input.GetMouseButton(0) && player.GetComponent<BackBag>().handIndex == 2)
        {
            timer += Time.deltaTime;
            if (timer > c2.Rate)
            {
                OnPressButton(2);
                timer = 0f;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (firstView.depth < 0)
                OnClickAim();
            else
                CloseAim();
        }
    }
}
