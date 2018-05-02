using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public AudioSource music;
    public SoldierState soldier;
    public AudioClip[] audioclips;
    // Update is called once per frame
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 2f);
    }
    //获取当前角色站在的地方材质
    string GetNowTerrain()
    {
        Terrain terrain = Terrain.activeTerrain;
        TerrainData td = terrain.terrainData;
        SplatPrototype[] sp = td.splatPrototypes;
        int index = TerrainSurface.GetMainTexture(transform.position);
        return sp[index].texture.name;
    }
    void SetAudio(float speed)
    {
        string tx = GetNowTerrain();
        switch (tx)
        {
            case "ground01":
                music.pitch = 1f * speed;
                music.clip = audioclips[0];
                break;
            case "ground03":
                music.pitch = 0.7f * speed;
                music.clip = audioclips[1];
                break;
        }
        music.Play();
    }
    void Update (){
        //草地和沙地是地形贴图材质的名称
        State state = soldier.soldierState;
        switch (state)
        {
            case State.isWalk_a:
            case State.isWalk_d:
            case State.isWalk_s:
            case State.isWalk_w:
                if (IsGrounded() && !music.isPlaying)
                {
                    SetAudio(1f);
                }
                break;
            case State.isRun:
                if (IsGrounded() && !music.isPlaying)
                {
                    SetAudio(1.4f);
                }
                break;
            case State.isJump:
                if (!IsGrounded())
                {
                    music.pitch = 1.5f;
                    music.clip = audioclips[2];
                    music.Play();
                }
                break;
        }
	}
}
