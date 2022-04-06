using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Returner : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Tool" || collision.tag == "PattingHand")
		{
			collision.transform.localPosition = new Vector3(0.699999988f, 5.86999989f, 0);
			collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		}
		if(collision.tag == "PattingHand")
		{
			collision.transform.localPosition = new Vector3(11.3699999f, 5.76000023f, 0);
			collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		}
		if(collision.tag == "Food")
		{
			Destroy(collision.gameObject);
		}
	}
}
