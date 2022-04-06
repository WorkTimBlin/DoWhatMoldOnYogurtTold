using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour
{
	[SerializeField]
	Hand handScript;
	[SerializeField]
	Stamina hygieneStamina;
	[SerializeField]
	SpriteRenderer dirt;
	[SerializeField]
	List<Sprite> dirtSpritesFromCleanToDirty = new List<Sprite>();

	GameObject handObject;

	Vector3 previousPos;

	// Start is called before the first frame update
	void Start()
	{
		handObject = handScript.gameObject;
		UpdateDirtSprite(hygieneStamina.Value);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (IsItHandWithTool(collision.gameObject))
		{
			previousPos = handObject.transform.position;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(IsItHandWithTool(collision.gameObject))
		{
			CleanBy((handObject.transform.position - previousPos).magnitude * 0.25f);
			previousPos = handObject.transform.position;
		}
	}

	private bool IsItHandWithTool(GameObject gameObject)
	{
		return gameObject == handObject && !handScript.IsEmpty && handScript.ObjectHolding.tag == "Tool";
	}

	private void CleanBy(float value)
	{
		hygieneStamina.Value += value;
		UpdateDirtSprite(hygieneStamina.Value);
	}

	public void PolluteBy(float value)
	{
		hygieneStamina.Value -= value;
		UpdateDirtSprite(hygieneStamina.Value);
	}

	private void UpdateDirtSprite(float value)
	{
		for(int i = 0; i < dirtSpritesFromCleanToDirty.Count; i++)
		{
			if((100 - value) <= (i + 1) * (100 / dirtSpritesFromCleanToDirty.Count))
			{
				dirt.sprite = dirtSpritesFromCleanToDirty[i];
				return;
			}
		}
	}
}
