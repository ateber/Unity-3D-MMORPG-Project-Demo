 
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float highestValue = 0.1f;
    float speed = 5f;
    public Vector3 CameraOffset { get; private set; }
    public AnimationCurve animationCurve = AnimationCurve.Linear(0, 1, 1, 0);
     
    void Start()
    {
        CameraOffset = new Vector3(0,transform.position.y-target.position.y, transform.position.z - target.position.z);
    }
    void FixedUpdate()
    {

        Vector3 desiredPosition = target.position + CameraOffset;
        transform.position = desiredPosition;
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
        //float percente = 0;
        //float distance = Vector3.Distance(transform.position, desiredPosition) / highestValue;
        //percente = -distance + 1;
        //Debug.Log(percente);
        //Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, speed * Time.deltaTime);
        //Vector3 smoothedPosition = Vector3.Slerp (transform.position,desiredPosition,animationCurve.Evaluate(percente)* speed* Time.deltaTime ); 
        //transform.position = smoothedPosition;
        //transform.LookAt(target.position ); 
    }
    //public Transform target;
    //public Transform cameraConsist;
    //float highestValue = 0.01f;
    //float speed =5f;
    //public Vector3 offset;

    //void Start(){

    //}
    //void FixedUpdate()
    //{ 
    //    transform.position = Vector3.Slerp (transform.position,cameraConsist.position,  speed *Time.deltaTime ); 
    //    transform.LookAt(target.position); 
    //}
}
