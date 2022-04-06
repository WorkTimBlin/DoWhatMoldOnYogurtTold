using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
	[SerializeField]
	private Transform fillerTransform;
	[SerializeField]
	[Range(0, 100)]
	private float _value;

	const float nulPos = -4.75f;
	public float Value
	{
		get { return _value; }
		set
		{
			_value = Mathf.Clamp(value, 0, 100);
			SetStamina();
		}
	}

	void SetStamina()
	{
		fillerTransform.localPosition = Vector3.right * nulPos * (1 - _value/100);
		fillerTransform.localScale = new Vector3(_value / 100, 1, 1);
	}
}
