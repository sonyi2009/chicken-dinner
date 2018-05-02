using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMover : MonoBehaviour
{
    public float speed;
    Animator animator;
    SoldierState soldierState;
    AudioSource music;
    BackBag bag;
    bool flag = true;
    // 通过射线检测主角是否落在地面或者物体上
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 2f);
    }
    void Start()
    {
        music = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        soldierState = GetComponent<SoldierState>();
        bag = GetComponent<BackBag>();
    }
    #region 声音
    //声效控制
    public AudioSource music2;
    public AudioClip[] audioclips;
    //获取当前角色站在的地方材质
    string GetNowTerrain()
    {
        Terrain terrain = Terrain.activeTerrain;
        TerrainData td = terrain.terrainData;
        SplatPrototype[] sp = td.splatPrototypes;
        int index = TerrainSurface.GetMainTexture(transform.position);
        return sp[index].texture.name;
    }
    void PlayAudio(int index, float speed, float vol = 1)
    {
        music.volume = vol;
        music.clip = audioclips[index];
        music.pitch = speed;
        if (!music.isPlaying)
        {
            music.Play();
        }
    }
    void PlayAudio(string state)
    {
        switch (state)
        {
            case "walk":
                if (soldierState.soldierPoise == Poise.isFall)
                {
                    PlayAudio(3, 1f, 0.1f);
                }
                else if (IsGrounded() && GetNowTerrain() == "ground01")
                {
                    PlayAudio(0, 1f);
                }
                else if (IsGrounded() && GetNowTerrain() == "ground03")
                {
                    PlayAudio(1, 0.7f);
                }
                break;
            case "run":
                if (IsGrounded() && GetNowTerrain() == "ground01")
                {
                    PlayAudio(0, 1.4f);
                }
                else if (IsGrounded() && GetNowTerrain() == "ground03")
                {
                    PlayAudio(1, 1f);
                }
                break;
            case "jump":
                if (IsGrounded())
                {
                    music2.PlayOneShot(audioclips[2]);
                }
                else
                {
                    PlayAudio("jump");
                    return;
                }
                break;
        }

    }
    #endregion
    Vector3 weaponFall_Position = new Vector3(-0.082f, 0.0137f, 0.158f);
    Quaternion weaponFall_Rotation = Quaternion.Euler(-82f, 74f, 137f);
    Vector3 v;
    Quaternion q;
    public Transform _pos;
    void OnAnimatorIK(int layer)
    {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            animator.SetLookAtPosition(_pos.position);
            animator.SetLookAtWeight(0.5f, 0.6f, 0.5f, 0.5f, 0f);
    }
    void Update()
    {
        //前进后退判断
        #region 前后左右移动
        if (Input.GetKey("w"))
        {
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * speed + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0));
            soldierState.SetState("isWalk_w");
            PlayAudio("walk");
        }
        else if (Input.GetKey("s"))
        {
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.back * speed + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0));
            soldierState.SetState("isWalk_s");
            PlayAudio("walk");
        }
        if (Input.GetKey("a"))
        {
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.left * speed + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0));
            soldierState.SetState("isWalk_a");
            PlayAudio("walk");
        }
        else if (Input.GetKey("d"))
        {
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.right * speed + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0));
            soldierState.SetState("isWalk_d");
            PlayAudio("walk");
        }
        if (Input.GetKeyUp("d") || Input.GetKeyUp("w") || Input.GetKeyUp("s") || Input.GetKeyUp("a"))
        {
            soldierState.SetState("Idle");
        }
        #endregion
        #region 疾跑只能往前跑
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w") && soldierState.soldierPoise == Poise.isStand)
        {
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * speed * 1.5f + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0));
            soldierState.SetState("isRun");
            PlayAudio("run");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isRun", false);
        }
        #endregion
        #region 跳跃
        if (Input.GetKeyDown("space") && IsGrounded())
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * 16;
            soldierState.SetState("isJump");
            animator.SetTrigger("jump");
            PlayAudio("jump");
        }
        #endregion
        if (Input.GetKeyDown("z"))
        {
            v = bag.back.localPosition;
            q = bag.back.localRotation;
            bag.back.localPosition = weaponFall_Position;
            bag.back.localRotation = weaponFall_Rotation;
            soldierState.SetPoise("isFall");
            speed = speed / 3;
        }
        if (Input.GetKeyDown("x"))
        {
            if (soldierState.soldierPoise == Poise.isSquat)
            {
                speed = speed * 2;
            }
            else if (soldierState.soldierPoise == Poise.isFall)
            {
                bag.back.localPosition = v;
                bag.back.localRotation = q;
                speed = speed * 3;
            }
            soldierState.SetPoise("isStand");
        }
        if (Input.GetKeyDown("c"))
        {

            soldierState.SetPoise("isSquat");
            speed = speed / 2;
        }


    }
}
