using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{
    //VARIABLES AND PROPERTIES
    [SerializeField]
    private Material lockedMaterial;
    [SerializeField]
    private Material unlockedMaterial;
    
    public bool holdingScale { get; private set; }
    
	private Vector3 originalScale;
    private GameObject player;
    private float originalScaleRatio;
    private Renderer renderer;
    
    //START AND VOID
    void Start()
    {
        this.holdingScale = false;
        this.renderer = GetComponent<Renderer>();

		this.originalScale = new Vector3(gameObject.transform.localScale.x,
										gameObject.transform.localScale.y,
										gameObject.transform.localScale.z);
    }
    void Update()
    {
        if (this.holdingScale == true)
        {
            float distance = CalculateDistance(player);

            float newScale = distance * originalScaleRatio;
            

            Vector3 newScaleVector = new Vector3(newScale, newScale, newScale);

            gameObject.transform.localScale = newScaleVector;
        }

    }
    
    //INTERACTIBLE OBJECT SPECIFIC METHODS
    public void HoldScale(GameObject player)
    {
        this.player = player;

        float distance = CalculateDistance(player);

        this.originalScaleRatio = gameObject.transform.localScale.x / distance;

        this.holdingScale = true;
        SwitchMaterial(this.lockedMaterial); 
        Debug.Log("SCALE HAS BEEN LOCKED!"); 
    }
    
    public void ReleaseScale()
    {
        this.holdingScale = false;
        SwitchMaterial(this.unlockedMaterial); 
        Debug.Log("SCALE HAS BEEN UNLOCKED!"); 
    }

	public void ResetScale()
	{
		ReleaseScale();
		gameObject.transform.localScale = originalScale;
	}

    public float CalculateDistance(GameObject player)
    {
        float distance = Mathf.Sqrt(Mathf.Pow((player.transform.position.x - gameObject.transform.position.x),2) +
                                    Mathf.Pow((player.transform.position.y - gameObject.transform.position.y),2) +
                                    Mathf.Pow((player.transform.position.z - gameObject.transform.position.z),2));

        return distance;
    }

    private void SwitchMaterial(Material material)
    {
        this.renderer.material = material;
    }
}
