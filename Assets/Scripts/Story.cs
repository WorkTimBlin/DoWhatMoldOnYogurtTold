using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    [SerializeField]
    float durationInSeconds = 6;
    [SerializeField]
    List<Sprite> slides = new List<Sprite>();

    int clicksCount = 0;
    SpriteRenderer rendererr;
    Coroutine slideshow;
    // Start is called before the first frame update
    void Start()
    {
        rendererr = GetComponent<SpriteRenderer>();
        slideshow = StartCoroutine(SlideshowCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) clicksCount++;
        if(clicksCount > 1)
		{
            StopCoroutine(slideshow);
            SceneManager.LoadScene("MainScene");
        }
    }

    IEnumerator SlideshowCoroutine()
	{
        foreach(Sprite slide in slides)
		{
            rendererr.sprite = slide;
            yield return new WaitForSeconds(durationInSeconds / slides.Count);
		}
        SceneManager.LoadScene("MainScene");
	}
}
