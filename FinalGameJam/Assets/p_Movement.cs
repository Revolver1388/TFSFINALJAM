using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_Movement : MonoBehaviour
{

    Animator anim;
    p_Movement player;
    PlayerCamera cammy;
    Rigidbody rb;
    float horizontal;
    float vertical;
    float rotateSpeed = 3;
    float walkAccel = 14;
    public float maxWalkSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cammy = FindObjectOfType<PlayerCamera>();
        player = FindObjectOfType<p_Movement>();
        rb = GetComponent<Rigidbody>();
        cammy.wallCamChoice = PlayerCamera.WallCamChoice.Back;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        anim.SetInteger("StopMove", Mathf.CeilToInt(horizontal + vertical));
        if (horizontal > 0 || horizontal < 0) { if (player.rb.velocity.x <= maxWalkSpeed && player.rb.velocity.x >= -maxWalkSpeed) { Movement(walkAccel); } anim.SetTrigger("StartMove"); cammy.CamType = PlayerCamera.CameraType.SideScroll; }
        else
        if (Input.GetKeyDown(KeyCode.Space)) anim.SetTrigger("Melee"); 
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space)) { anim.SetTrigger("Attack"); cammy.CamType = PlayerCamera.CameraType.Orbit;  }
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
