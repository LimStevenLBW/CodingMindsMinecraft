using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATACK : MonoBehaviour
{
    public Transform playerTransform;
    public float deactivateTime;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            Vector3 direction = other.gameObject.transform.position - playerTransform.position;


            enemy.TakeDamage(10, direction);
        }
    }

    void OnEnable()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(deactivateTime);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
