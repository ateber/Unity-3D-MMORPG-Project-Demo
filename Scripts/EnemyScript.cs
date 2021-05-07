using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject NameText{set;get;}
    

    float textRotate = 70;
    public HealthSystem HealthSystem { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        HealthSystem = new HealthSystem(100);
        CreateEnemyTextPosObject();
        //GameEvents.Instance.onDestroyObject += DestroyEnemy;
    }
    
    void CreateEnemyTextPosObject()
    {
        GameObject objectTextPos=new GameObject("TextPos"); 
        objectTextPos.transform.SetParent(transform);

        BoxCollider renderers = this.transform.gameObject.GetComponentInChildren<BoxCollider>(); 
        float objectMaxSize = Mathf.Max(renderers.bounds.size.z, renderers.bounds.size.x);
        objectMaxSize = objectMaxSize / 2 + 2f + renderers.bounds.size.y * 0.1f;  
        objectTextPos.transform.localPosition = new Vector3(0, objectMaxSize, 0); 
    }

    void DestroyEnemy() {   
        Destroy(NameText);
        Destroy(gameObject);
    }

    public void TakeDamage (int damageAmount) {

        HealthSystem.TakeDamage(damageAmount);
        GameEvents.Instance.EnemyHealthChanged();
        if (HealthSystem.isAlive() == false)
        {
            DestroyEnemy(); 
            GameEvents.Instance.EnemyDestroy();
        }
    }

    public void Heal(int healAmount)
    {
        HealthSystem.Heal(healAmount);
        GameEvents.Instance.EnemyHealthChanged(); 
    }



    // Update is called once per frame
    void Update()
    { 
        //Debug.Log(NameText.name);
    }

    private void OnDestroy()
    {
        //GameEvents.Instance.onDestroyObject -= DestroyEnemy;
    }
}
