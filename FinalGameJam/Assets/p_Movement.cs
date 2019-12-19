using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_Movement : MonoBehaviour
{

    Animator anim;
    p_Movement player;
    PlayerCamera cammy;
    Rigidbody rb;
    GameObject throwSpawn;
    float horizontal;
    float vertical;
    float rotateSpeed = 3;
    float walkAccel = 14;
    public GameObject caster;
    public float checkDist = 5;
    public float maxWalkSpeed = 4;
    RaycastHit thing;
    public LayerMask IgnoreMask;
    bool isThrowing = false;
    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cammy = FindObjectOfType<PlayerCamera>();
        player = FindObjectOfType<p_Movement>();
        rb = GetComponent<Rigidbody>();
        throwSpawn = GameObject.FindGameObjectWithTag("throw");
        cammy.wallCamChoice = PlayerCamera.WallCamChoice.Back;
    }

    // Update is called once per frame
    void Update()
    {
        CheckThrow();
        horizontal = Input.GetAxis("Horizontal");
        anim.SetInteger("StopMove", Mathf.CeilToInt(horizontal + vertical));
        if (horizontal > 0 || horizontal < 0) { if (player.rb.velocity.x <= maxWalkSpeed && player.rb.velocity.x >= -maxWalkSpeed) { Movement(walkAccel); } anim.SetTrigger("StartMove"); cammy.CamType = PlayerCamera.CameraType.SideScroll; }
        else
        if (Input.GetKeyDown(KeyCode.Space)) anim.SetTrigger("Melee");
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space) && CheckThrow())
        {
            temp = thing.collider.gameObject;
          anim.SetTrigger("Attack"); cammy.CamType = PlayerCamera.CameraType.Orbit; isThrowing = true;
        }
        if(isThrowing) temp.transform.position = throwSpawn.transform.position;
    }

    public bool CheckThrow()
    {
        Debug.DrawLine(caster.transform.position, new Vector3(caster.transform.position.x - checkDist, caster.transform.position.y, caster.transform.position.z), Color.black);
        return Physics.Linecast(caster.transform.position, new Vector3(caster.transform.position.x - checkDist, caster.transform.position.y, caster.transform.position.z), out thing, IgnoreMask);      
    }

    public void Movement(float speed)
    {
        Vector3 cammyRight = cammy.transform.TransformDirection(Vector3.right);
        Vector3 cammyFront = cammy.transform.TransformDirection(Vector3.forward);
        cammyRight.y = 0;
        cammyFront.y = 0;
        cammyRight.Normalize();
        cammyFront.Normalize();
        player.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(player.transform.forward, /*cammyFront * vertical +*/ cammyRight * horizontal, rotateSpeed * Time.fixedDeltaTime, 0.0f));
        player.rb.AddForce(player.transform.forward * speed, ForceMode.Force);
    }
}
