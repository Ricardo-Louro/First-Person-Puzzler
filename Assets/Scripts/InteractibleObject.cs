using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool HoldScale { get; private set; }
    private GameObject player;
    private float originalScaleRatio;
    
    void Start()
    {
        this.HoldScale = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (HoldScale == true)
        {
            float distance = Calculate_Distance(player);

            float newScale = distance * originalScaleRatio;

            Vector3 newScaleVector = new Vector3(newScale, newScale, newScale);

            gameObject.transform.localScale = newScaleVector;
        }

    }

    public void Hold_Scale(GameObject player)
    {
        this.player = player;

        float distance = Calculate_Distance(player);

        this.originalScaleRatio = gameObject.transform.localScale.x / distance;

        this.HoldScale = true;
        Debug.Log("SCALE HAS BEEN LOCKED!"); 
    }
    
    public void Release_Scale()
    {
        this.HoldScale = false;
        Debug.Log("SCALE HAS BEEN LOCKED!"); 
    }

    private float Calculate_Distance(GameObject player)
    {
        float distance = Mathf.Sqrt(Mathf.Pow((player.transform.position.x - gameObject.transform.position.x),2) +
                                    Mathf.Pow((player.transform.position.y - gameObject.transform.position.y),2) +
                                    Mathf.Pow((player.transform.position.z - gameObject.transform.position.z),2));

        return distance;
    }
}
