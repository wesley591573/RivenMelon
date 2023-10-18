using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Manager : MonoBehaviour
{
    public GameObject objectPrefab; // 在Unity中指定要生成的範本

    private GameObject objectGenerate ; // 依照範本生成的物件
    private bool newObject = false ;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 建立新物件
        objectGenerate = Instantiate(objectPrefab, mousePosition, Quaternion.identity);

        objectGenerate.gameObject.transform.position = mousePosition ;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if ( !newObject ) {
            objectGenerate.gameObject.transform.position = mousePosition ;

        } // if()


        // 左鍵觸發
        if (Input.GetMouseButtonDown(0)) 
        {

            // 手上的掉下去
            newObject = true ;

        } // if()

        
        // 撞到東西
        if ( objectGenerate.gameObject == null || objectGenerate.gameObject.GetComponent<Object>().isCollision && newObject ) 
        {
            // 建立新物件
            objectGenerate = Instantiate(objectPrefab, mousePosition, Quaternion.identity);

            newObject = false ;

        } // if()
        

    } // Update()

    

}
