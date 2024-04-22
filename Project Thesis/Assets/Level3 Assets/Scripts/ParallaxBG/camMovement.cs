using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour
{
    public float camSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(camSpeed * Time.deltaTime, 0, 0);
    }

    public void AdjustCamSpeed()
   {
        camSpeed = 15;
   }
}
