using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {
    public Transform t;
    //手机端需要修改
    void RotateView()
    {
        float moveXZ = Input.GetAxis("Mouse X");
        transform.RotateAround(transform.position, Vector3.up, moveXZ);
        float moveYZ = Input.GetAxis("Mouse Y");
        t.RotateAround(transform.position, -1 * transform.right, moveYZ);
    }
    public void RotateView(float moveXZ,float moveYZ)
    {
        transform.RotateAround(transform.position, Vector3.up, moveXZ);
        t.RotateAround(transform.position, -1 * transform.right, moveYZ);
    }
    
    void RotateViewByAlt()
    {
        
    }
    // Update is called once per frame
    void Update () {
        RotateView();
	}
}
