using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class TargetScript : MonoBehaviour
{
    public float rotate = 1;
    public float speed = 500;
    public float coordZ = 0f;

    ParticleSystem particleSystem;

    

    private void Awake()
    {
        particleSystem = this.GetComponent<ParticleSystem>();
        GameEvents.Instance.onEnemyDestroy += HideTarget;
        GameEvents.Instance.onMonsterDestroy += HideTarget;
        HideTarget();
    } 

    // Update is called once per frame
    void Update()
    { 
        transform.Rotate(new Vector3(0,0,speed*rotate)*Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             
            if (Physics.Raycast(ray, out hit, 100.0f) )
            {
                Transform gameObject = hit.transform.root;
                if (isTarget(gameObject) && GameManager.Instance.Target != gameObject)
                {
 
                    GameManager.Instance.Target = gameObject;
                    CalculateTarget(gameObject);
                    HandleTargetEventsWithTag(gameObject.tag);  
                }
               
            } 
            else 
            {  
                HideTarget();
                GameEvents.Instance.ExitTarget();
            }
        }
        
    }

    void ShowTarget()
    {
        GetComponent<Renderer>().enabled = true;
    }

    void HideTarget()
    {

        GameManager.Instance.Target = null;
        GetComponent<Renderer>().enabled = false; 
    }

     

    bool isTarget(Transform transform)
    {
        return transform.tag == "Enemy" || transform.tag == "Monster" || transform.tag == "Friend";
    }

    void HandleTargetEventsWithTag(string tag)
    {
        if (tag == "Monster")
        {
            TrailModule tm = this.GetComponent<ParticleSystem>().trails;
            tm.colorOverTrail = new MinMaxGradient(new Color32(200, 60, 0, 255));
            GameEvents.Instance.SelectMonster();
        }
        else if (tag == "Enemy")
        {
            TrailModule tm = this.GetComponent<ParticleSystem>().trails;
            tm.colorOverTrail = new MinMaxGradient(new Color32(255, 10, 10, 255)); 
            GameEvents.Instance.SelectEnemy();
        }
        else if (tag == "Friend")
        { 
            TrailModule tm= this.GetComponent<ParticleSystem>().trails;
            tm.colorOverTrail=new MinMaxGradient(new Color32(0,80,180,255));
            GameEvents.Instance.SelectFriend();
        }
    }

    void CalculateTarget(Transform target)
    { 
        this.transform.position = new Vector3(target.position.x, target.position.y + coordZ, target.position.z);

        BoxCollider renderers = target.gameObject.GetComponentInChildren<BoxCollider>();
        Bounds bounds = renderers.bounds; 
        float diameter = 0;
        diameter = bounds.size.x / 2 + 1f;

        ShapeModule particalSystemShapeModule = particleSystem.shape;
        particalSystemShapeModule.radius = diameter;

        particleSystem.Clear();
        particleSystem.Simulate(1.6f);
        //this.GetComponent<ParticleSystem>().Play( ); 

        ShowTarget();
    }

    
}
