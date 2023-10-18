using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using System;

public class Object : MonoBehaviour
{
    public bool isCollision = false ;

    private List<GameObject> prefabList = new List<GameObject>() ;
    private List<string> tagList = new List<string>() ;

    public GameObject prefab_XUN; // 在Unity中指定要生成的範本
    public GameObject prefab_HUANG;
    public GameObject prefab_YEE;
    public GameObject prefab_BANANA; 
    public GameObject prefab_SMILE; 
    public GameObject prefab_PUSSY; 
    public GameObject prefab_MASK; 
    public GameObject prefab_CHUO; 
    public GameObject prefab_YO; 
    public GameObject prefab_KAI; 
    public GameObject prefab_GAY; 

    public Text scoreText ;

    private bool isAddScore = false ; // 是否有加過分數
    public bool isCombine = false ;


    public static Mutex mut = new Mutex();



    // Start is called before the first frame update
    void Start()
    {
        isCollision = false ;

        tagList.Add( "XUN" ) ;
        tagList.Add( "HUANG" ) ;
        tagList.Add( "YEE" ) ;
        tagList.Add( "BANANA" ) ;
        tagList.Add( "SMILE" ) ;
        tagList.Add( "PUSSY" ) ;
        tagList.Add( "MASK" ) ;
        tagList.Add( "CHUO" ) ;
        tagList.Add( "YO" ) ;
        tagList.Add( "KAI" ) ;
        tagList.Add( "GAY" ) ;


        prefabList.Add(prefab_XUN) ;
        prefabList.Add(prefab_HUANG) ;
        prefabList.Add(prefab_YEE) ;
        prefabList.Add(prefab_BANANA) ;
        prefabList.Add(prefab_SMILE) ;
        prefabList.Add(prefab_PUSSY) ;
        prefabList.Add(prefab_MASK) ;
        prefabList.Add(prefab_CHUO) ;
        prefabList.Add(prefab_YO) ;
        prefabList.Add(prefab_KAI) ;
        prefabList.Add(prefab_GAY) ;


    }

    // Update is called once per frame
    void Update()
    {
        if ( isCollision && gameObject.transform.position.y >= Manager.lossHeight ) {
            Manager.lossGame = true ;

        } // if()
        
    } // Update()


    // 撞到一個gameObject的處理方式
    // other.gameObject只有一個gameObject
    void OnCollisionEnter2D(Collision2D other) 
    {
        if ( other.gameObject.tag == "Wall" ) {
            // do nothing

        } // if()

        else {
            isCollision = true ;

            GameObject objectPrefab ;

            //進入互斥區，搶得發言權，其他人只得等待
            mut.WaitOne();

            scoreManagement( gameObject, false ) ;

            // 跟自己對撞，把舊的兩個物件刪掉，產生新物件
            if ( other.gameObject.tag == gameObject.tag && !isCombine && !other.gameObject.GetComponent<Object>().isCombine ) 
            {
                isCombine = true ;
                other.gameObject.GetComponent<Object>().isCombine = true ;

                int index = tagList.IndexOf( gameObject.tag ) ;

                index++ ;

                if ( index < tagList.Count ) {
                    GameObject temp ;

                    objectPrefab = prefabList[index] ;

                    scoreManagement( objectPrefab, true ) ;

                    temp = Instantiate(objectPrefab, other.contacts[0].point, Quaternion.identity);

                    temp.gameObject.GetComponent<Object>().isAddScore = true ;

                } // if()


                Destroy( other.gameObject );
                Destroy( gameObject );

            } // if()


            //離開互斥區，放棄發言權，下一個等待的人可以準備發言了
            mut.ReleaseMutex();


        } // else



    } // OnCollisionEnter2D()

    void scoreManagement( GameObject gameObject, bool doubleSign ) {
        int score ;
        int addScore = 0 ;


        score = Convert.ToInt32( scoreText.text );


        if ( gameObject.tag == "XUN" ) {
            addScore = 1 ;

        } // if()

        else if ( gameObject.tag == "HUANG" ) {
            addScore = 2 ;

        } // else if()

        else if ( gameObject.tag == "YEE" ) {
            addScore = 3 ;

        } // else if()

        else if ( gameObject.tag == "BANANA" ) {
            addScore = 4 ;

        } // else if()

        else if ( gameObject.tag == "SMILE" ) {
            addScore = 5 ;

        } // else if()

        else if ( gameObject.tag == "PUSSY" ) {
            addScore = 6 ;

        } // else if()

        else if ( gameObject.tag == "MASK" ) {
            addScore = 7 ;

        } // else if()

        else if ( gameObject.tag == "CHUO" ) {
            addScore = 8 ;

        } // else if()

        else if ( gameObject.tag == "YO" ) {
            addScore = 9 ;

        } // else if()

        else if ( gameObject.tag == "KAI" ) {
            addScore = 10 ;

        } // else if()

        else if ( gameObject.tag == "GAY" ) {
            addScore = 11 ;

        } // else if()


        // 生成
        if ( doubleSign ) {
            isAddScore = true ;

            addScore = addScore * 2 ;

            score = score + addScore ;

            scoreText.text = score.ToString();

        } // if()

        // 碰撞
        else if ( !isAddScore ) {
            isAddScore = true ;

            score = score + addScore ;

            scoreText.text = score.ToString();

        } // else if()

        

    } // score()

    public void setIsAddScore( bool sign ) {
        isAddScore = sign ;

    } // setIsAddScore()

    
}
