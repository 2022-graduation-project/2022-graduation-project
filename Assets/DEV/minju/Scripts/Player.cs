using UnityEngine;
using UnityEditor;
using System.Collections;

public class Player : MonoBehaviour 
{
	//Health of player character
	public float Health = 100f;

	//Attack range
	public float AttackRange = 2f;

	//Amount of damage to inflict on enemy
	public float AttackDamage = 10f;

	//Reference to blood-vision
	public GameObject BloodVision = null;

	//List of enemies in level
	private AI_Enemy[] Enemies = null;
	//--------------------------------------------------
	//Event called on health changed
	public void ChangeHealth(float Amount)
	{
		//Reduce health
		Health += Amount;

		//Show blood vision and then hide
		BloodVision.SetActive(true);
		Invoke ("HideBloodVision",0.5f);

		//Should we die?
		if(Health <= 0)
		{
			//Exit from game, back to editor
			EditorApplication.isPlaying = false;
			return;
		}
	}
	//--------------------------------------------------
	//Hides blood
	void HideBloodVision()
	{
		BloodVision.SetActive(false);
	}
	//--------------------------------------------------
	//Called at level start-up
	void Start()
	{
		//Get all enemies in scene
		Enemies = Object.FindObjectsOfType<AI_Enemy>();
	}
	//--------------------------------------------------
	//Called every frame
	void Update()
	{
		//Should we attack?
		if(Input.GetKeyDown(KeyCode.RightControl))
		{
			//Damage enemies in range
			foreach(AI_Enemy En in Enemies)
			{
				//If within attack distance then attack
				if(Vector3.Distance(transform.position, En.transform.position) < AttackRange)
					En.SendMessage("ChangeHealth", AttackDamage, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	//--------------------------------------------------
}
