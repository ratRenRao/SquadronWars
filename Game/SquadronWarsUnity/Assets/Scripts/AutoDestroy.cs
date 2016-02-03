using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

    public float animTime;
	// Use this for initialization
	void Start () {
      
	}
    void Update()
    {
        StartCoroutine("DestoryOnAnimationEnd");
    }

    private IEnumerator DestoryOnAnimationEnd()
    {
        yield return new WaitForSeconds(animTime);
        Destroy(gameObject);
    }
}
