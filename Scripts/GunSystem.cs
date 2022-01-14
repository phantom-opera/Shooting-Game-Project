using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSystem : MonoBehaviour
{
	//Gun stats
	public int damage;
	public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
	public int magazineSize, bulletsPerTap;
	public bool allowButtonHold;
	public int bulletsLeft;
	public AudioSource gunSound;
	public AudioSource gunReload;
	int bulletsShot;

	//bools 
	bool shooting, readyToShoot, reloading;

	//Reference
	public Camera fpsCam;
	public Transform attackPoint;
	public RaycastHit rayHit;
	public LayerMask whatIsEnemy;

	//Graphics
	public GameObject muzzleFlash, bulletHoleGraphic;
	public float camShakeMagnitude, camShakeDuration;
	public Text text;

	private void Awake()
	{
		bulletsLeft = magazineSize;
		readyToShoot = true;
	}
	private void Update()
	{
		MyInput();

		//SetText

		text.text = bulletsLeft + " / " + magazineSize;
	}
	private void MyInput()
	{
		if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
		else shooting = Input.GetKeyDown(KeyCode.Mouse0);

		if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload(); //If the player presses the R button then ammo is replenished

		//Shoot
		if (readyToShoot && shooting && !reloading && bulletsLeft > 0) //If the gun is ready to shoot and the player is not reloading/gun has no bullets then it will shoot ammo when the shooting button is pressed.
		{
			bulletsShot = bulletsPerTap;
			Shoot();
		}
	}
	private void Shoot()
	{
		readyToShoot = false;

		//Spread
		float x = Random.Range(-spread, spread);
		float y = Random.Range(-spread, spread);

		//Calculate Direction with Spread
		Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

		//RayCast
		if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
		{
			Debug.Log(rayHit.collider.name);

			if (rayHit.collider.CompareTag("Enemy")) //shoots out a 'ray'. if the ray connects with an object with the enemy tag then the enemy takes damage.
				rayHit.collider.GetComponent<EnemyAINav>().TakeDamage(damage);
		}

		gunSound.Play();

		//Graphics

		if (!rayHit.collider.CompareTag("Enemy")) //If the ray does not connect with an enemy a bullet hole graphic is displayed instead.
		{
			Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.FromToRotation(Vector3.forward, rayHit.normal));
		}

		StartCoroutine("MuzzleFlash");


		bulletsLeft--;
		bulletsShot--;

		Invoke("ResetShot", timeBetweenShooting);

		if (bulletsShot > 0 && bulletsLeft > 0)
			Invoke("Shoot", timeBetweenShots);
	}
	private void ResetShot()
	{
		readyToShoot = true;
	}
	private void Reload()
	{
		reloading = true;
		Invoke("ReloadFinished", reloadTime);
		gunReload.Play();
	}
	private void ReloadFinished()
	{
		bulletsLeft = magazineSize;
		reloading = false;
	}

	IEnumerator MuzzleFlash()
	{
		muzzleFlash.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		muzzleFlash.SetActive(false);
	}
}
