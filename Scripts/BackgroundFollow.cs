using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform target; 
    float mapSizeX = 2000;
    float mapSizeZ = 2000;
    // Update is called once per frame
    void LateUpdate()
    {
        float background_x =  transform.position.x;
        float background_y =  transform.position.y;
        float background_z =  transform.position.z;

        float rateX =  target.position.x / mapSizeX;
        float rateZ=  target.position.z / mapSizeZ;

        float backgroundSizeX = GetComponent<SpriteRenderer>().bounds.size.x ;
        float backgroundSizeZ = GetComponent<SpriteRenderer>().bounds.size.z   ;

        float targetPosXinBackgorund = backgroundSizeX * rateX;
        float targetPosZinBackgorund = backgroundSizeZ * rateZ;

        background_x = target.position.x - (targetPosXinBackgorund - (backgroundSizeX / 2));
        background_z = target.position.z - (targetPosZinBackgorund - (backgroundSizeZ / 2));
        background_z = background_z - target.GetComponent<CameraFollow>().CameraOffset.z;

         
        transform.position = new Vector3(background_x, background_y, background_z);
         
        //transform.eulerAngles=Vector3.back*player.rotation.eulerAngles.y+Vector3.right*90;
    }
}
