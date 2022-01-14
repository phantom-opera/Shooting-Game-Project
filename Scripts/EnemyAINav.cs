using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyAINav : MonoBehaviour
{

	public Transform[] points;
	public Transform startPosition;
	private int destPoint = 0;
	private NavMeshAgent agent;
	private Animation animate;
	public Transform retreatPoint;
	public float maxHealth;
	public float currentHealth;

	public GameObject playerChar;
	public GameObject self;
	public GameObject detector;
	public AudioSource deathSound;
	public int damageToGive;


	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animate = GetComponent<Animation>();
		playerChar = GameObject.FindWithTag("Player");
		currentHealth = maxHealth;

		// Disabling auto-braking allows for continuous movement
		// between points (ie, the agent doesn't slow down as it
		// approaches a destination point).
		agent.autoBraking = false;

		GotoNextPoint();
	}


	void GotoNextPoint()
	{
		// Returns if no points have been set up
		if (points.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		agent.destination = points[destPoint].position;
		animate.Play("Run");


		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;
	}


	void Update()
	{
		// Choose the next destination point when the agent gets
		// close to the current one.
		if (!agent.pathPending && agent.remainingDistance < 0.5f)
			GotoNextPoint();


		if (currentHealth <= 0)
		{
			ScoringSystem.score++;
			deathSound.Play();
			Destroy(self);
		}

		if (detector.GetComponent<Detection>().playerDetected == true)
		{
			agent.destination = playerChar.transform.position;
			animate.Play("Run");
		}
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			StartCoroutine("Wait");
			other.gameObject.GetComponent<PlayerManager>().DamagePlayer(damageToGive);
			//attackSound.Play();
		}
		animate.Play("Attack2");
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1);
	}
}