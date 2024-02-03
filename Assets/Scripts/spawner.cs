using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{ 
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){

        while(true){
            yield return new WaitForSeconds(3);
            Instantiate(enemy, transform.position, Quaternion.identity);
        }


    }

}
