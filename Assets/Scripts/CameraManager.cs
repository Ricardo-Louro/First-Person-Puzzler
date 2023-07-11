using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera cam;
    private GameObject player;
    
    [SerializeField]
    private GameObject holdPos;
    private GameObject heldObj;

	[SerializeField]
    private float pickupForce;

    private Rigidbody targetRB;

    [SerializeField]
    private float mouseXLimit = 88f;
    
    private float mouseX;
    private float mouseY;

    private float rotationX;
    private float rotationY;

    private float mouseSensitivityX = 100f;
    private float mouseSensitivityY = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.Find("Player");
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position;
        
        mouseX = mouseSensitivityX * Input.GetAxis("Mouse X") * Time.deltaTime;
        mouseY = mouseSensitivityY * Input.GetAxis("Mouse Y") * Time.deltaTime;

        rotationX += mouseY;
        rotationY += mouseX;

        rotationX = Mathf.Clamp(rotationX, -mouseXLimit, mouseXLimit);

        transform.rotation = Quaternion.Euler(-rotationX, rotationY, 0);
        player.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        
        //Handle Aiming
        Vector3 middleScreen = cam.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, cam.nearClipPlane));
        
        RaycastHit hit;
        if (Physics.Raycast(middleScreen, transform.forward, out hit, 5000f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            
            if (hit.collider.gameObject.GetComponent<InteractibleObject>() != null)
            {
				InteractibleObject target = hit.collider.gameObject.GetComponent<InteractibleObject>();

				//LEFT MOUSE BUTTON
                if (Input.GetMouseButtonDown(0))
                { 
                    if (target.holdingScale == false)
					{
                        target.HoldScale(player);
					}
                    else
					{
						target.ReleaseScale();
					}
				}

				//RIGHT MOUSE BUTTON
				if (Input.GetMouseButtonDown(1))
				{
					if(heldObj == null)
						if (target.CalculateDistance(player) <= 2f)
							if (target.transform.localScale.x <= 0.8f)
							{
								targetRB = target.GetComponent<Rigidbody>();
								targetRB.useGravity = false;
								targetRB.drag = 10;
								targetRB.constraints = RigidbodyConstraints.FreezeRotation;

								targetRB.transform.parent = holdPos.transform;
								heldObj = hit.collider.gameObject;

							}
							else
								Debug.Log("Too big!");
						else
							Debug.Log("Out of range!");
					else
					{
						targetRB.useGravity = true;
						targetRB.drag = 1;
						targetRB.constraints = RigidbodyConstraints.None;

						targetRB.transform.parent = null;
						heldObj = null;
					}
				}

				if (heldObj != null)
				{
					if (Vector3.Distance(heldObj.transform.position, holdPos.transform.position) > 0.1f)
					{
						Vector3 moveDirection = (holdPos.transform.position - heldObj.transform.position);
						targetRB.AddForce(moveDirection * pickupForce);
					}
				}

				//MIDDLE MOUSE BUTTON
                if (Input.GetMouseButtonDown(2))
                {
					target.ResetScale();
                }
            }
        }
    }
}