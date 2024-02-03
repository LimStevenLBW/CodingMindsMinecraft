using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour, Enemy
{
    public float speed;

    public SkinnedMeshRenderer mesh;
    public Material deadMat;
    private Rigidbody body;
    private Vector3 direction;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];

        StartCoroutine(UpdateTarget());
    }
    IEnumerator UpdateTarget()
    {
        while (true)
        {
            FindTarget();
            yield return new WaitForSeconds(2);
        }
    }
    void FindTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) direction = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        Move();
        RotateTowardsPlayer();
    }

    void RotateTowardsPlayer(){
        Vector3 rotation = Quaternion.LookRotation(player.transform.position - transform.position).eulerAngles;
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = Quaternion.Euler(rotation);
    }

     void Move(){   
        transform.position += direction.normalized * speed * Time.deltaTime;
     }

    public void TakeDamage(int damage, Vector3 direction)
    {
        direction.y += 0.3f;
        body.AddForce(direction.normalized * 3.5f);
        StopAllCoroutines();
        StartCoroutine(Die());
    }

    IEnumerator Die() {
        mesh.material = deadMat;
        yield return new WaitForSeconds(0.5f);

        Score.Instance.AddScore(1);
        Destroy(gameObject);
    }
}
