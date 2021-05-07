using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameNamesTextManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
 
    // Update is called once per frame
    void LateUpdate()
    {
        foreach ( GameObject player in GameObject.FindGameObjectsWithTag("Player") ){
            CreateOrUpdateNamePosition(player,"Player");
        }

        foreach ( GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy") ){
            CreateOrUpdateNamePosition(enemy,"Enemy");
        } 

        foreach ( GameObject friend in GameObject.FindGameObjectsWithTag("Friend") ){
            CreateOrUpdateNamePosition(friend,"Friend");
        } 

        foreach ( GameObject monster in GameObject.FindGameObjectsWithTag("Monster") ){
            CreateOrUpdateNamePosition(monster,"Monster");
        } 
         
    }


    void CreateOrUpdateNamePosition(GameObject gameObject,string tag){
        if(gameObject.transform.Find("TextPos")!=null){
            Transform textPosConstant=gameObject.transform.Find("TextPos").transform;
            Transform objectNameText=transform.Find(gameObject.name);
            if(objectNameText==null ){ 
                GameObject objectNameInstance = GetTextPrefab(tag); 
                objectNameText = Instantiate(objectNameInstance, Vector3.zero, Quaternion.identity, transform).transform;  
                SetTextObject(gameObject,objectNameText.gameObject);
                objectNameText.name=gameObject.name; 
                objectNameText.GetComponent<TextMeshProUGUI>().text = gameObject.name;
            }  
            Vector3 namePosWithScreen=Camera.main.WorldToScreenPoint(textPosConstant.position);
            objectNameText.position=namePosWithScreen; 
        }
    }

    void SetTextObject(GameObject gameObject, GameObject gameObjectText){ 
        if( gameObject.GetComponent<PlayerManager>()!=null) {
            gameObject.GetComponent<PlayerManager>().NameText=gameObjectText;  
        } 
        else if( gameObject.GetComponent<EnemyScript>()!=null) { 
            gameObject.GetComponent<EnemyScript>().NameText=gameObjectText; 
        } 
        else if( gameObject.GetComponent<FriendScript>()!=null) 
            gameObject.GetComponent<FriendScript>().NameText=gameObjectText;
        else if( gameObject.GetComponent<MonsterScript>()!=null) 
            gameObject.GetComponent<MonsterScript>().NameText=gameObjectText; 
    } 
    GameObject GetTextPrefab(string tag){
        tag=tag.ToLower();
        if(tag.Equals("player"))
            return (GameObject)Resources.Load("Prefabs/TextPrefabs/PlayerNameText", typeof(GameObject));
        else if(tag.Equals("enemy"))
            return (GameObject)Resources.Load("Prefabs/TextPrefabs/EnemyNameText", typeof(GameObject));
        else if(tag.Equals("friend"))
            return (GameObject)Resources.Load("Prefabs/TextPrefabs/FriendNameText", typeof(GameObject));
        else if(tag.Equals("monster"))
            return (GameObject)Resources.Load("Prefabs/TextPrefabs/MonsterNameText", typeof(GameObject));  
        return null;          
    }
}
