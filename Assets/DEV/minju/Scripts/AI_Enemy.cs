//--------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Linq;
//--------------------------------------------------
//Define possible states for enemy (each state is equivalent to string hash code in camel-case)
public enum AI_ENEMY_STATE {IDLE = 2081823275,
							PATROL=207038023,
							CHASE= 1463555229,
							ATTACK=1080829965,
							SEEKHEALTH=-833380208};
//--------------------------------------------------
public class AI_Enemy : MonoBehaviour, IListener
{
	//**Enemy 현재 상태
	public AI_ENEMY_STATE CurrentState = AI_ENEMY_STATE.IDLE;

	//Distance to destination classified as 'reached'
	public float DistEps = 1f;

	//Viewing angle (in degrees) of enemy for line of sight
	public float FieldOfView = 30f;

	//Layer mask for line of sight detection
	public LayerMask SightMask;

	//Time (in seconds) enemy should chase unseen player before giving up
	public float ChaseTimeOut = 2f;

	//Attack delay (interval in seconds between attacks)
	public float AttackDelay = 1f;

	//Damage to deal on each attack
	public float AttackDamage = 10f;

	//Health of enemy character
	public float Health = 100f;

	//Danger health level to trigger flee and restore behaviour
	public float HealthDangerLevel = 20f;

	//Animator for object
	private Animator ThisAnimator = null;

	//Navigation Agent Component
	private UnityEngine.AI.NavMeshAgent ThisAgent = null;

	//Reference to all waypoints in the scene
	private Transform[] WayPoints = null;

	//Reference to transform component
	private Transform ThisTransform = null;

	//Reference to player in scene
	private Transform PlayerTransform = null;

	//Attached collision volume
	private BoxCollider ThisCollider;
	
	//Boolean indicating whether player can be seen 'right now'
	[SerializeField]
	private bool CanSeePlayer = false;
	//--------------------------------------------------
	//Called at object creation
	public void Awake()
	{
		//Find all gameobjects with waypoint
		GameObject[] Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

		//Select all transform components from waypoints using Linq
		WayPoints = (from GameObject GO in Waypoints
		             select GO.transform).ToArray();

		//**애니메이터 얻기
		ThisAnimator = GetComponent<Animator>();

		//**내비게이션 메시 에이전트
		ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		//**Get Transform Component
		ThisTransform = transform;

		//**플레이어 태그를 가진 트랜스폼
		PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

		//**Get Collider
		ThisCollider = GetComponent<BoxCollider>();
	}
	//--------------------------------------------------
	void Start()
	{
		//**Enter first state (Idle 상태로 시작)
		StartCoroutine(State_Idle());
	}
	//--------------------------------------------------
	//Notification function to be invoked on Listeners when events happen
	public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
	{
	}
	//--------------------------------------------------
	//Event called when Idle animation is completed
	public void OnIdleAnimCompleted()
	{
		//Stop active Idle state
		StopAllCoroutines();
		StartCoroutine(State_Patrol());
	}
	//--------------------------------------------------
	//Function to return whether player can be seen right now
	private bool HaveLineSightToPlayer(Transform Player)
	{
		//Get angle between enemy sight and player
		float Angle = Mathf.Abs(Vector3.Angle(ThisTransform.forward, (Player.position-ThisTransform.position).normalized));

		//If angle is greater than field of view, we cannot see player
		if(Angle > FieldOfView) return false;

		//Check with raycast- make sure player is not on other side of wall
		if(Physics.Linecast(ThisTransform.position, Player.position, SightMask)) return false;

		//We can see player
		return true;
	}
	//--------------------------------------------------
	//Function to get nearest health restore to Target in scene
	private HealthRestore GetNearestHealthRestore(Transform Target)
	{
		//Get all health restores
		HealthRestore[] Restores = Object.FindObjectsOfType<HealthRestore>();

		//Nearest
		float DistanceToNearest = Mathf.Infinity;

		//Selected Health Restore
		HealthRestore Nearest = null;

		//Loop through all health restores
		foreach(HealthRestore HR in Restores)
		{
			//Get distance to this health restore
			float CurrentDistance = Vector3.Distance(Target.position, HR.transform.position);

			//Found nearer health restore, so update
			if(CurrentDistance <= DistanceToNearest)
			{
				Nearest = HR;
				DistanceToNearest = CurrentDistance;
			}
		}

		//Return nearest or null
		return Nearest;
	}
	//--------------------------------------------------
	//Update sight check for player
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			ChangeHealth(-30f);
		}

		//Assume we cannot see player
		CanSeePlayer = false;

		//If player not inside bounds then exit
		if(!ThisCollider.bounds.Contains(PlayerTransform.position)) return;

		//Player is inside bounds, update line of sight
		CanSeePlayer = HaveLineSightToPlayer(PlayerTransform);
	}
	//--------------------------------------------------
	//Called when object exits this collider
	void OnTriggerExit(Collider other)
	{
		//if object is not player, then exit
		if(!other.CompareTag("Player")) return;

		//Is player and they have left line of sight
		CanSeePlayer = false;
	}
	//--------------------------------------------------
	//Event called health changed
	public void ChangeHealth(float Amount)
	{
		//Reduce health
		Health += Amount;

		//Should we die?
		if(Health <= 0)
		{
			StopAllCoroutines();
			Destroy(gameObject);
			return;
		}

		//Check health danger level
		if(Health > HealthDangerLevel) return;

		//Health is less than or equal to danger level, now seek health restores, if available
		StopAllCoroutines();
		StartCoroutine(State_SeekHealth());
	}
	//--------------------------------------------------
	#region States
	//--------------------------------------------------
	//**This coroutine runs when object is in idle state
	//**Idle 상태일 때 시작되는 코루틴
	public IEnumerator State_Idle()
	{
		//**Set current state 현재상태 IDLE로 설정
		CurrentState = AI_ENEMY_STATE.IDLE;

		//**Activate idle state with Mecanim
		//**메카님에서 IDLE 상태 활성화
		//**IDLE 해시코드로 메카님에서 Idle 트리거 설정 및 애니메이션 재생
		ThisAnimator.SetTrigger((int) AI_ENEMY_STATE.IDLE);

		//**Stop nav mesh agent movement
		//**내비게이션 이동 멈춤
		ThisAgent.Stop();

		//**Loop forever while in idle state
		//**IDLE 상태에서 무한반복
		while (CurrentState == AI_ENEMY_STATE.IDLE)
		{
			//Check if we can see player
			if(CanSeePlayer)
			{
				//**If we can see player, then chase to reach attack distance
				//**플레이어를 보면 추격
				StartCoroutine(State_Chase());
				yield break;
			}
			
			//Wait for next frame
			yield return null;
		}
	}
	//--------------------------------------------------
	//This coroutine runs when object is in patrol state
	public IEnumerator State_Patrol()
	{
		//Set current state
		CurrentState = AI_ENEMY_STATE.PATROL;
		
		//Set Patrol State
		ThisAnimator.SetTrigger((int) AI_ENEMY_STATE.PATROL);

		//Pick a random waypoint
		Transform RandomDest = WayPoints[Random.Range(0, WayPoints.Length)];

		//Go to destination
		ThisAgent.SetDestination(RandomDest.position);

		//Loop forever while in patrol state
		while(CurrentState == AI_ENEMY_STATE.PATROL)
		{
			//Check if we can see player
			if(CanSeePlayer)
			{
				//If we can see player, then chase to reach attack distance
				StartCoroutine(State_Chase());
				yield break;
			}

			//Check if we have reached destination
			if(Vector3.Distance(ThisTransform.position, RandomDest.position) <= DistEps)
			{
				//We have reached destination. Changed state back to Idle
				StartCoroutine(State_Idle());
				yield break;
			}

			//Wait for next frame
			yield return null;
		}
	}
	//--------------------------------------------------
	//This coroutine runs when object is in chase state
	public IEnumerator State_Chase()
	{
		//Set current state
		CurrentState = AI_ENEMY_STATE.CHASE;

		//Set Chase State
		ThisAnimator.SetTrigger((int) AI_ENEMY_STATE.CHASE);

		//Loop forever while in chase state
		while(CurrentState == AI_ENEMY_STATE.CHASE)
		{
			//Set destination to player
			ThisAgent.SetDestination(PlayerTransform.position);

			//If we lose sight of player, keep chasing for a time-out period
			if(!CanSeePlayer)
			{
				//Begin time out
				float ElapsedTime = 0f;

				//Continue to chase
				while(true)
				{
					//Increment time
					ElapsedTime += Time.deltaTime;

					//Set destination to player
					ThisAgent.SetDestination(PlayerTransform.position);

					//Wait for next frame
					yield return null;

					//Has time out expired?
					if(ElapsedTime >= ChaseTimeOut)
					{
						//If we still cannot see player, then change to idle state
						if(!CanSeePlayer)
						{
							//Change to idle - still cannot see player
							StartCoroutine(State_Idle());
							yield break;
						}
						else
							break; //We can see player again so resume chase
					}
				}
			}

			//If we have reached player then attack
			if(Vector3.Distance(ThisTransform.position, PlayerTransform.position) <= DistEps)
			{
				//We have reached distance, now attack
				StartCoroutine(State_Attack());
				yield break;
			}
			
			//Wait until next
			yield return null;
		}
	}
	//--------------------------------------------------
	//This coroutine runs when object is in attack state
	public IEnumerator State_Attack()
	{
		//Set current state
		CurrentState = AI_ENEMY_STATE.ATTACK;
		
		//Set Chase State
		ThisAnimator.SetTrigger((int) AI_ENEMY_STATE.ATTACK);

		//Stop nav mesh agent movement
		ThisAgent.Stop();

		//Set up timer for attack interval
		float ElapsedTime = 0f;

		//Loop forever while in attack state
		while(CurrentState == AI_ENEMY_STATE.ATTACK)
		{
			//Update timer
			ElapsedTime += Time.deltaTime;

			//Check if player has passed beyond the attack distance or disappeared. If so, begin chase
			if(!CanSeePlayer || Vector3.Distance(ThisTransform.position, PlayerTransform.position) > DistEps)
			{
				//Change to chase
				StartCoroutine(State_Chase());
				yield break;
			}

			//Check attack delay
			if(ElapsedTime >= AttackDelay)
			{
				//Reset counter
				ElapsedTime = 0f;

				//Launch attack
				if(PlayerTransform != null)
					PlayerTransform.SendMessage("ChangeHealth", -AttackDamage, SendMessageOptions.DontRequireReceiver);
			}
				
			//Wait until next frame
			yield return null;
		}
	}
	//--------------------------------------------------
	//This coroutine runs when object is in seek health state
	public IEnumerator State_SeekHealth()
	{
		//Set current state
		CurrentState = AI_ENEMY_STATE.SEEKHEALTH;
		
		//Set Chase State
		ThisAnimator.SetTrigger((int) AI_ENEMY_STATE.SEEKHEALTH);

		//This is the nearest health restore
		HealthRestore HR = null;

		//Loop forever while in seek health state
		while(CurrentState == AI_ENEMY_STATE.SEEKHEALTH)
		{
			//If health restore is not valid, then get nearest
			if(HR == null) HR = GetNearestHealthRestore(ThisTransform);

			//There is an active health restore, so move there
			ThisAgent.SetDestination(HR.transform.position);

			//If HR is null or health is above critical, then there is no more health restore, so go to idle state
			if(HR == null || Health > HealthDangerLevel)
			{
				//Change to idle
				StartCoroutine(State_Idle());
				yield break;
			}

			//Wait until next frame
			yield return null;
		}
	}
	//--------------------------------------------------
	#endregion
	//--------------------------------------------------
}
//--------------------------------------------------