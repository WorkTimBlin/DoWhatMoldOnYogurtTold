using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldPicture : MonoBehaviour
{
	[SerializeField]
	Sprite calmSprite = null;
	[SerializeField]
	Sprite openMouthSprite1 = null;
	[SerializeField]
	Sprite openMouthSprite2 = null;
	[SerializeField]
	Sprite pattingSprite1 = null;
	[SerializeField]
	Sprite pattingSprite2 = null;
	[SerializeField]
	Sprite sadAngrySprite = null;
	[SerializeField]
	List<Sprite> chewingSprites = new List<Sprite>();

	SpriteRenderer rendererr;

	Coroutine currentAnimation = null;
	public bool IsBusy { get; private set; }

	// Start is called before the first frame update
	void Start()
	{
		if (
			calmSprite == null ||
			openMouthSprite1 == null ||
			openMouthSprite2 == null ||
			pattingSprite1 == null ||
			pattingSprite2 == null ||
			sadAngrySprite == null ||
			chewingSprites.Count == 0 
			) throw new System.Exception("all sprites must be defined");
		rendererr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void OnGetPattedForSeconds(float seconds)
	{
		if (currentAnimation != null)
		{
			StopCoroutine(currentAnimation);
		}
		currentAnimation = StartCoroutine(BeingPattedCoroutine(seconds));
	}

	public void OnAboutToGetFed(float seconds)
	{
		if (currentAnimation != null)
		{
			StopCoroutine(currentAnimation);
		}
		currentAnimation = StartCoroutine(AboutToGetFedAnimationCoroutine(seconds));
	}

	public void StartChewing(Vector3 handPos)
	{
		if(currentAnimation != null)
		{
			StopCoroutine(currentAnimation);
		}
		currentAnimation = StartCoroutine(ChewingAnimationCoroutine(2, 2));
	}
	public void OnGetSadForSeconds(float seconds)
	{
		if (currentAnimation != null)
		{
			StopCoroutine(currentAnimation);
		}
		currentAnimation = StartCoroutine(BeingSadCoroutine(seconds));
	}


	IEnumerator AboutToGetFedAnimationCoroutine(float seconds)
	{
		rendererr.sprite = openMouthSprite1;
		yield return new WaitForSeconds(seconds);
		rendererr.sprite = openMouthSprite2;
	}
	IEnumerator ChewingAnimationCoroutine(float seconds, int times)
	{
		IsBusy = true;
		float secondsToWaitEachTime = seconds / (times * chewingSprites.Count);
		for(int i = 0; i < times; i++)
		{
			foreach(Sprite sprite in chewingSprites)
			{
				rendererr.sprite = sprite;
				yield return new WaitForSeconds(secondsToWaitEachTime);
			}
		}
		rendererr.sprite = calmSprite;
		IsBusy = false;
	}
	IEnumerator BeingSadCoroutine(float seconds)
	{
		rendererr.sprite = sadAngrySprite;
		yield return new WaitForSeconds(seconds);
		rendererr.sprite = calmSprite;
	}

	IEnumerator BeingPattedCoroutine(float seconds)
	{
		IsBusy = true;
		rendererr.sprite = Random.Range(0, 2) == 0 ? pattingSprite1 : pattingSprite2;
		yield return new WaitForSeconds(seconds);
		rendererr.sprite = calmSprite;
		IsBusy = false;
	}
}
