using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
	public AudioSource attackSound;
	private Animation animate;
	public int damageToGive;


	// Start is called before the first frame update
	void Start()
	{
		animate = GetComponent<Animation>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") //On collision with the player attack them and deal damage.
		{
			this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
			StartCoroutine("Wait");
			other.gameObject.GetComponent<PlayerManager>().DamagePlayer(damageToGive);
			//attackSound.Play();
		}
		animate.Play("Attack2");
		StartCoroutine("Wait");
		gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1);
	}
}
