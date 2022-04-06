using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mold : MonoBehaviour
{
	[SerializeField]
	GameObject handObject;
	[SerializeField]
	GameObject pictureObject;
	[SerializeField]
	Stamina hungerStamina;
	//[SerializeField]
	//Stamina hygieneStamina;
	[SerializeField]
	Stamina loveStamina;
	[SerializeField]
	Refrigerator refrigerator;
	[SerializeField]
	ParticleSystem particles;


	Hand handScript;
	MoldPicture pictureScript;

	GameObject foodToEat;
	bool IsBusy { get => pictureScript.IsBusy; }
	Vector3 lastPattingPos;
	float pattingStage;

	// Start is called before the first frame update
	void Start()
	{
		handScript = handObject.GetComponent<Hand>();
		pictureScript = pictureObject.GetComponent<MoldPicture>();

		hungerStamina.Value = 100;
		//hygieneStamina.Value = 100;
		loveStamina.Value = 100;
		StartCoroutine(HungerBuildingUpCoroutine(3f));
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void OnGetPatted()
	{
		pictureScript.OnGetPattedForSeconds(1);
		particles.Clear();
		particles.Play();
		StartCoroutine(PolluteLaterByValue(0.5f, 15));
	}
	void OnAboutToGetFed(GameObject food)
	{
		pictureScript.OnAboutToGetFed(0.3f);
		foodToEat = food;
	}
	void OnEat(GameObject food)
	{
		pictureScript.StartChewing(handObject.transform.position);
		Destroy(food);
		hungerStamina.Value = hungerStamina.Value + 34;
	}
	void GetSadForSeconds()
	{
		pictureScript.OnGetSadForSeconds(1);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject == handObject)
		{
			if (!IsBusy)
			{
				if (handScript.IsEmpty)
				{
					
				}
				else
				{
					if(handScript.ObjectHolding.tag == "Food")
					{
						OnAboutToGetFed(handScript.ObjectHolding);
					}
				}
			}
		}
		else if (collision.tag == "Food" && collision.gameObject != foodToEat)
		{
			StartCoroutine(EatingFreeFoodCoroutine(collision.gameObject));
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject == handObject)
		{
			if (!IsBusy)
			{
				if (handScript.IsEmpty)
				{
					if (WasAboutToGetFed())
					{
						OnEat(foodToEat);
					}
				}
				else
				{
					if(handScript.ObjectHolding.tag == "PattingHand")
					{
						pattingStage += (handObject.transform.position - lastPattingPos).magnitude;
						if(pattingStage > 7)
						{
							OnGetPatted();
							pattingStage = 0;
						}
						lastPattingPos = handObject.transform.position;
					}
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject == handObject)
		{
			if (WasAboutToGetFed())
			{
				GetSadForSeconds();
				foodToEat = null;
			}
		}
	}

	IEnumerator EatingFreeFoodCoroutine(GameObject food)
	{
		pictureScript.OnAboutToGetFed(0.1f);
		yield return new WaitForSeconds(0.2f);
		OnEat(food);
	}

	IEnumerator HungerBuildingUpCoroutine(float percentAddingPeriodInSeconds)
	{
		while (true)
		{
			yield return new WaitForSeconds(percentAddingPeriodInSeconds);
			hungerStamina.Value--;
		}
	}
	IEnumerator PolluteLaterByValue(float seconds, float value)
	{
		yield return new WaitForSeconds(seconds);
		refrigerator.PolluteBy(value);
	}
	bool WasAboutToGetFed() => foodToEat != null;
}
