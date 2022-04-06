using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSomethingElse : MonoBehaviour, IUIAction
{
	[SerializeField]
	GameObject objectToIstantiate;

	new public GameObject gameObject { get => base.gameObject; }
	GameObject parent { get => transform.parent.gameObject; }

	public void OnAction()
	{
		Destroy(parent);
		if(objectToIstantiate != null) Instantiate(objectToIstantiate);
	}
}
