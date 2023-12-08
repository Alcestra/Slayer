using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Movement : MonoBehaviour
{


    [SerializeField]public CharacterController controller;
    public Transform cam;
    public Animator anim;
    bool Attacking;

    public float speed = 6f;
    public float normalSpeed = 8f;
    public float turnSmoothTime  = 0.1f;
    float turnSmoothVelocity;
    [SerializeField] public float runMultiplyer = 2f;

    private void Awake()
    {
         anim = GetComponent<Animator>();
    }

    private void Update()
    {
        movement();
        test();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }




    void movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        //Camera smoothing and turning
        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * normalSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            normalSpeed = speed * runMultiplyer;

        }
        else
            normalSpeed = speed;

        //animation
        if(direction == Vector3.zero)
        {
            //idle
            anim.SetFloat("Speed", 0f);
        }
        //walking
        else        
            anim.SetFloat("Speed",0.1f);
        

    }



    void test()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("Attacking", true);
        }
        else
            anim.SetBool("Attacking", false);
    }
}
