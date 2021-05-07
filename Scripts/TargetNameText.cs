using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetNameText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Instance.onSelectEnemy += ShowEnemyNameText;
        GameEvents.Instance.onEnemyDestroy += HideTargetNameText;

        GameEvents.Instance.onSelectMonster += ShowMonsterNameText; 
        GameEvents.Instance.onMonsterDestroy += HideTargetNameText;

        GameEvents.Instance.onSelectFriend += ShowFriendNameText;
        GameEvents.Instance.onFriendDestroy += HideTargetNameText;

        GameEvents.Instance.onExitTarget += HideTargetNameText;
    }
    void ShowEnemyNameText()
    { 
        this.GetComponent<TextMeshProUGUI>().color = new Color32(150, 0, 0, 255); ;
        this.GetComponent<TextMeshProUGUI>().text=GameManager.Instance.Target.GetComponent<EnemyScript>().name; 
        gameObject.SetActive(true);    
    }

    //void ShowPlayerNameText()
    //{
    //    this.GetComponent<TextMeshProUGUI>().color = new Color32(0, 150, 0, 255); ;
    //    this.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Target.GetComponent<PlayerManager>().name;
    //    gameObject.SetActive(true);
    //}

    void ShowMonsterNameText()
    {
        this.GetComponent<TextMeshProUGUI>().color = new Color32(200, 60, 0, 255); ;
        this.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Target.GetComponent<MonsterScript>().name;
        gameObject.SetActive(true);
    }

    void ShowFriendNameText()
    {
        this.GetComponent<TextMeshProUGUI>().color = new Color32(0, 125, 200, 255); ;
        this.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Target.GetComponent<FriendScript>().name;
        gameObject.SetActive(true);
    }

    void HideTargetNameText()
    {
        this.GetComponent<TextMeshProUGUI>().color = Color.white;
        gameObject.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
