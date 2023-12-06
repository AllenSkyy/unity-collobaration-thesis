using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class grabOjects : MonoBehaviour
{
	[SerializeField]
	private Transform grabPoint;
	[SerializeField]
	private Transform rayPoint;
	[SerializeField]
	private float rayDistance;
	
	private GameObject grabbedObject;
	private int layerIndex;
	
	private void Start(){
		layerIndex = LayerMask.NameToLayer("Objects");
	}
	
	void Update(){
		RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);
		
		if (hitInfo.collider!=null && hitInfo.collider.gameObject.layer == layerIndex){
			
			if (Keyboard.current.spaceKey.wasPressedThisFrame && grabbedObject == null){
				grabbedObject = hitInfo.collider.gameObject;
 				grabbedObject.GetComponent<Rigidbody2D>().isKinematic=true;
				grabbedObject.transform.position = grabPoint.position;
				grabbedObject.transform.SetParent(transform);
			}
			else if (Keyboard.current.spaceKey.wasPressedThisFrame){
				grabbedObject.GetComponent<Rigidbody2D>().isKinematic=false;
				grabbedObject.transform.SetParent(null);
				grabbedObject = null;
				
			}
			
			
		}
	}
	
}
