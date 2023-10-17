using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Random = System.Random;
using System.IO;

public class Manager : MonoBehaviour
{

    private GameObject objectPrefab ;
    private GameObject nextObjectPrefab ; // 下一個產生的物體
    public GameObject prefab_XUN; // 在Unity中指定要生成的範本
    public GameObject prefab_HUANG;
    public GameObject prefab_YEE;
    public GameObject prefab_BANANA; 
    public GameObject prefab_SMILE;


    private List<GameObject> prefabList = new List<GameObject>() ; 

    private GameObject objectGenerate ; // 依照範本生成的物件

    private bool falling = false ;

    public UnityEngine.UI.Image nextHead ;



    // Start is called before the first frame update
    void Start()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.y = 6.3f ;

        prefabList.Add(prefab_XUN) ;
        prefabList.Add(prefab_HUANG) ;
        prefabList.Add(prefab_YEE) ;
        prefabList.Add(prefab_BANANA) ;
        prefabList.Add(prefab_SMILE) ;

        objectPrefab = randomPrefab() ;
        nextObjectPrefab = randomPrefab() ;

        updateNextHead() ;

        // 建立新物件
        objectGenerate = Instantiate(objectPrefab, mousePosition, Quaternion.identity);

        objectGenerate.gameObject.transform.position = mousePosition ;

        
    } // Start()

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.y = 6.3f ;

        if ( !falling ) {
            objectGenerate.gameObject.transform.position = mousePosition ;

        } // if()


        // 左鍵觸發，手上的物體掉下去
        if (Input.GetMouseButtonDown(0)) 
        {

            // 手上的掉下去
            falling = true ;

        } // if()

        
        // 掉下去的物體撞到東西，產生新物件
        if ( objectGenerate.gameObject == null || objectGenerate.gameObject.GetComponent<Object>().isCollision && falling ) 
        {
            objectPrefab = nextObjectPrefab ;

            nextObjectPrefab = randomPrefab() ;

            updateNextHead() ;

            // 建立新物件
            objectGenerate = Instantiate(objectPrefab, mousePosition, Quaternion.identity);

            falling = false ;

        } // if()
        

    } // Update()

    GameObject randomPrefab() {
        var random = new Random() ;

        int index = random.Next(prefabList.Count);

        return prefabList[index] ;

    } // randomPrefab()

    public void updateNextHead() {


        if ( nextObjectPrefab.tag == "XUN" ) {

            nextHead.sprite = prefab_XUN.GetComponent<SpriteRenderer>().sprite ;

        } // if()

        else if ( nextObjectPrefab.tag == "HUANG" ) {

            nextHead.sprite = prefab_HUANG.GetComponent<SpriteRenderer>().sprite ;

        } // else if()

        else if ( nextObjectPrefab.tag == "YEE" ) {

            nextHead.sprite = prefab_YEE.GetComponent<SpriteRenderer>().sprite ;

        } // else if()

        else if ( nextObjectPrefab.tag == "BANANA" ) {

            nextHead.sprite = prefab_BANANA.GetComponent<SpriteRenderer>().sprite ;

        } // else if()

        else if ( nextObjectPrefab.tag == "SMILE" ) {

            nextHead.sprite = prefab_SMILE.GetComponent<SpriteRenderer>().sprite ;

        } // else if()

    } // GameObject()

} 
