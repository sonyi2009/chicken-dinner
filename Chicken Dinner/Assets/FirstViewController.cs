using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstViewController : MonoBehaviour {
    public GameObject[] view;
    Camera c;
    private void Start()
    {
        c = GetComponent<Camera>();
    }
    public void SetView(int index)
    {
        switch (index)
        {
            case -1:
                c.fieldOfView = 45f;
                break;
            case 0:
                c.fieldOfView = 40f;
                break;
            case 1:
                c.fieldOfView = 40f;
                break;
            case 2:
                c.fieldOfView = 20f;
                break;
            case 4:
                c.fieldOfView = 10f;
                break;
            case 8:
                c.fieldOfView = 5f;
                break;
        }
        foreach(GameObject gb in view)
        {
            gb.SetActive(false);
        }
        if (index == 4) view[0].SetActive(true);
        else if(index ==8) view[1].SetActive(true);
    }
    public void SetShow()
    {
        c.depth *= -1;
    }
}
