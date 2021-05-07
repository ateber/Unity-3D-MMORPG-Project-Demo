using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBar : MonoBehaviour
{
    float updateSpeedSeconds=1f;
    Object lockObject=new Object();
    public GameObject backgroundSlider;
    void Start()
    {
        GameEvents.Instance.onSelectEnemy += SelectEnemy;
        GameEvents.Instance.onEnemyDestroy += HideHealthBar;
        GameEvents.Instance.onEnemyHealthChanged += CalculateEnemyTargetBarValue;
  
        GameEvents.Instance.onSelectMonster += SelectMonster;        
        GameEvents.Instance.onMonsterDestroy += HideHealthBar;        
        GameEvents.Instance.onMonsterHealthChanged += CalculateMonsterTargetBarValue; 

        GameEvents.Instance.onSelectFriend += SelectFriend;
        GameEvents.Instance.onFriendDestroy += HideHealthBar;
        GameEvents.Instance.onFriendHealthChanged += CalculateFriendTargetBarValue; 

        GameEvents.Instance.onExitTarget += HideHealthBar; 
        backgroundSlider=transform.Find("BackgroundSlider").gameObject; 
    }
    List<Coroutine> coroutines=new List<Coroutine>();

    void execCoroutine(float pct){
        coroutines.Add(StartCoroutine(ChangeToPct(pct)));
    }

    void stopCoroutines(){
        foreach(Coroutine co in coroutines){
            StopCoroutine(co);
        }
    }
    private IEnumerator ChangeToPct(float pct){
        lock(lockObject){
            float preChangePct = backgroundSlider.GetComponent<Slider>().value;
            float elapsed=0;
            Transform target=GameManager.Instance.Target; 
            while(elapsed < updateSpeedSeconds ){ 
                elapsed+=Time.deltaTime;
                backgroundSlider.GetComponent<Slider>().value = Mathf.Lerp(preChangePct,pct,elapsed/updateSpeedSeconds) ;
                yield return null; 
            }
            backgroundSlider.GetComponent<Slider>().value=pct;
        }  
    }
     
    public void SelectEnemy(){ 
        float pct = GameManager.Instance.Target.GetComponent<EnemyScript>().HealthSystem.GetHealthPct();  
        GetComponent<Slider>().value = pct; 
        stopCoroutines();
        backgroundSlider.GetComponent<Slider>().value=pct;
        ShowHealthBar();
    }
    public void SelectMonster(){ 
        float pct = GameManager.Instance.Target.GetComponent<MonsterScript>().HealthSystem.GetHealthPct();  
        GetComponent<Slider>().value = pct; 
        stopCoroutines();
        backgroundSlider.GetComponent<Slider>().value=pct;
        ShowHealthBar();
    }
    public void SelectFriend(){ 
        float pct = GameManager.Instance.Target.GetComponent<FriendScript>().HealthSystem.GetHealthPct(); 
        GetComponent<Slider>().value = pct; 
        stopCoroutines();
        backgroundSlider.GetComponent<Slider>().value=pct;  
     }
    void CalculateEnemyTargetBarValue()
    { 
        float pct = GameManager.Instance.Target.GetComponent<EnemyScript>().HealthSystem.GetHealthPct(); 
        GetComponent<Slider>().value = pct; 
        execCoroutine(pct);   
    }

    void CalculateMonsterTargetBarValue()
    { 
        float pct = GameManager.Instance.Target.GetComponent<MonsterScript>().HealthSystem.GetHealthPct();
        this.GetComponent<Slider>().value = pct;
        execCoroutine(pct); 
    }

    void CalculateFriendTargetBarValue()
    {
        float pct = GameManager.Instance.Target.GetComponent<FriendScript>().HealthSystem.GetHealthPct();
        this.GetComponent<Slider>().value = pct;
        execCoroutine(pct);  
    }


    void ShowHealthBar()
    { 
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void HideHealthBar()
    { 
        for (int i=0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
     
 
    void Update()
    { 
    }


}
