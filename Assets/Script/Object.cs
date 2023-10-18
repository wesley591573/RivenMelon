using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading;


public class Object : MonoBehaviour
{
    public bool isCollision = false ;
    public bool isCombine = false ;

    private List<GameObject> prefabList = new List<GameObject>() ;
    private List<string> tagList = new List<string>() ;

    public GameObject prefab_XUN; // 在Unity中指定要生成的範本
    public GameObject prefab_HUANG; // 在Unity中指定要生成的範本
    public GameObject prefab_YEE; // 在Unity中指定要生成的範本

    public static Mutex mut = new Mutex();



    // Start is called before the first frame update
    void Start()
    {
        isCollision = false ;

        tagList.Add( "XUN" ) ;
        tagList.Add( "HUANG" ) ;
        tagList.Add( "YEE" ) ;

        prefabList.Add(prefab_XUN) ;
        prefabList.Add(prefab_HUANG) ;
        prefabList.Add(prefab_YEE) ;


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // 撞到一個gameObject的處理方式
    // other.gameObject只有一個gameObject

    void OnCollisionEnter2D(Collision2D other) 
    {
        

        isCollision = true ;
        // Debug.Log("勛撞到囉") ;

        GameObject objectPrefab ;

        //進入互斥區，搶得發言權，其他人只得等待
        mut.WaitOne();

        if ( other.gameObject.tag == gameObject.tag && !other.gameObject.GetComponent<Object>().isCombine && !isCombine ) 
        {
            isCombine = true ;
            other.gameObject.GetComponent<Object>().isCombine = true ;


            int index = tagList.IndexOf( gameObject.tag ) ;

            index++ ;

            Debug.Log("跟自己對撞") ;


            if ( index != tagList.Count ) {
                objectPrefab = prefabList[index] ;


                Instantiate(objectPrefab, other.contacts[0].point, Quaternion.identity);
                

            } // if()


            Destroy( other.gameObject );
            Destroy( gameObject );

        }

        //離開互斥區，放棄發言權，下一個等待的人可以準備發言了
        mut.ReleaseMutex();


    } // OnCollisionEnter2D()
}
