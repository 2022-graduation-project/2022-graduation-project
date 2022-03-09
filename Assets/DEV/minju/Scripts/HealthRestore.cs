//--------------------------------------------------
using UnityEngine;
using System.Collections;
//--------------------------------------------------
public class HealthRestore : MonoBehaviour
{
	//--------------------------------------------------
	//Health Points for this restore
	public float HealthPoints = 50f;

	//Distance for collection
	public float DistanceCollection = 2f;

	//--------------------------------------------------
	void OnTriggerStay(Collider other)
	{
		//Check if colliding object is near enough to collect this bonus
		if(Vector3.Distance(other.transform.position, transform.position) > DistanceCollection) return;

		other.SendMessage("ChangeHealth", HealthPoints, SendMessageOptions.DontRequireReceiver);

		//Destroy this object
		Destroy(gameObject);
	}
	//--------------------------------------------------
}
//--------------------------------------------------