using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1000f;

    private Rigidbody rb;
    private GroundChecker groundChecker;
    
    private Vector3 moveDirection;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 jumpVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        groundChecker = gameObject.GetComponentInChildren<GroundChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT HANDLING
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        if(groundChecker.isGrounded)
            if(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput) < 0.8f)
            {
                rb.velocity = new Vector3(rb.velocity.x / 2,rb.velocity.y, rb.velocity.z /2);
            }

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * Time.deltaTime);

        //JUMP HANDLING
        if(groundChecker.isGrounded)
        {
            Physics.gravity = new Vector3(0, -9.8f, 0);

            if (Input.GetKeyDown("space"))
            {
                jumpVector = rb.velocity;
                jumpVector.y = 10f;
                rb.velocity = jumpVector;
            }
        }
        else
        {
            if(Input.GetKey("space"))
            {
                Physics.gravity = new Vector3(0, -15f, 0);
            }
            else
            {
                Physics.gravity = new Vector3(0, -20f, 0);
            }
        }
    }
}