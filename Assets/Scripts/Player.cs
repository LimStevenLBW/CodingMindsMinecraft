using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placing blocks
public class Player : MonoBehaviour
{
    public projectile problastPrefab;
    public Animator weaponAnimator;
    public ATACK atack;
    public Camera mainCamera;
    public Rigidbody body;

    public float speed;
    public float jumpSpeed;
    public float gravityBoost;
    public int jumpAmount;
    private Vector3 direction;
    private bool isGrounded;
    private int currentJumpCount;

    public List<GameObject> blockInventory;
    private GameObject selectedBlock;

    public bool touchDie;

    //Camera Variables
    private float yaw;
    private float pitch;
    public float mouseSensitivity;
    public Camera playerCamera;

    
    // Start is called before the first frame update
    void Start()
    {
        selectedBlock = blockInventory[0];
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();
        PlaceBlock();
        Move();
        Jump();
        Attack();
      
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    void FixedUpdate(){
        body.AddForce(Vector3.down * gravityBoost * body.mass);
    }

    public void OnCollisionEnter(Collision collision){

        if(collision.gameObject.tag == "enemy"){
            touchDie = true;

            Vector3 direction = transform.position - collision.gameObject.transform.position;
            body.AddForce(direction.normalized * 15);
        }

        if (collision.gameObject.tag == "floor")
        {
            isGrounded = true;
            currentJumpCount = 0;

        }
    }

    void Move(){
       direction = new Vector3(0, 0, 0);

      if(Input.GetKey("w")){
            //direction.z = 1 ;
            direction += transform.forward;
      }

      if(Input.GetKey("s")){
            direction += transform.forward * -1;
      }


      if(Input.GetKey("a")){
            direction += transform.right * -1;
        }

      if(Input.GetKey("d")){
            direction += transform.right;
      }

      //BLOCK SWITCHING
      if (Input.GetKeyDown("1")) selectedBlock = blockInventory[0];
      if (Input.GetKeyDown("2")) selectedBlock = blockInventory[1];
      if (Input.GetKeyDown("3")) selectedBlock = blockInventory[2];
      if (Input.GetKeyDown("4")) selectedBlock = blockInventory[3];
      if (Input.GetKeyDown("5")) selectedBlock = blockInventory[4];
      if (Input.GetKeyDown("6")) selectedBlock = blockInventory[5];
      if (Input.GetKeyDown("7")) selectedBlock = blockInventory[6];
      if (Input.GetKeyDown("8")) selectedBlock = blockInventory[7];
      if (Input.GetKeyDown("9")) selectedBlock = blockInventory[8];


    }

    void Jump(){
      if(Input.GetKeyDown(KeyCode.Space)){

        if(isGrounded){
          currentJumpCount++;
          isGrounded = false;
          body.velocity += new Vector3(0, jumpSpeed, 0);
        }
        else if(currentJumpCount < jumpAmount){
          currentJumpCount++;
          body.velocity += new Vector3(0, jumpSpeed, 0);
        }

      }
    }

    public void HitTheGround()
    {
        isGrounded = true;
    }

    void PlaceBlock(){
      Vector3 placePosition = playerCamera.transform.forward * 2 + transform.position;

      placePosition = new Vector3(
          Mathf.Round(placePosition.x), 
          Mathf.Round(placePosition.y), 
          Mathf.Round(placePosition.z)
     );

        placePosition.y += 1;
    
        if(mainCamera.transform.rotation.eulerAngles.x >= 20  && mainCamera.transform.rotation.eulerAngles.x < 180)
        {

        }
        else if (mainCamera.transform.rotation.eulerAngles.x > 180 && mainCamera.transform.rotation.eulerAngles.x < 360 - 20)
        {
            //placePosition.y += 1f;
        }
      //placePosition.z += 4;
      //placePosition.y -= 1f;

    //PLACE BLOCK IF BLOCK EXISTS
      if (Input.GetMouseButtonDown(1) && selectedBlock){
        Instantiate(selectedBlock, placePosition, Quaternion.identity);
      }
    }

    void CameraControl(){
        yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");

        // Clamp pitch between lookAngle
        pitch = Mathf.Clamp(pitch, -80, 80);

        transform.localEulerAngles = new Vector3(0, yaw, 0);
        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);

    }
    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && !atack.gameObject.activeSelf)
        {
          weaponAnimator.SetTrigger("triggerAttack");
          atack.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown("f")){

            Vector3 fireballers = mainCamera.transform.forward + mainCamera.transform.position;

            projectile blast = Instantiate(problastPrefab, fireballers, Quaternion.identity);
            Rigidbody blastBody = blast.GetComponent<Rigidbody>();
            blastBody.AddForce(mainCamera.transform.forward * 5000);

        }
    }
}
