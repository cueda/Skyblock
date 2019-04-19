using UnityEngine;
using System;
using System.Collections;

public class BalloonEntity : MonoBehaviour 
{
    [SerializeField]
    private float lifetime = 1f;
    [SerializeField]
    private float xMoveSpeed = 1f;
    [SerializeField]
    private AnimationCurve yHeight;

    private RequestType request;


    public enum RequestType
    {
        DIRT,
        KITTEN,
        SAPLING,
        SOMETHINGELSE
    }


    void OnEnable()
    {
        StartCoroutine(FlyThenCreateItem());
    }


    void OnDisable()
    {
        StopAllCoroutines();
    }


    private IEnumerator FlyThenCreateItem()
    {
        for(float timer = 0; timer < lifetime; timer += Time.deltaTime)
        {
            transform.Translate(new Vector2(xMoveSpeed, yHeight.Evaluate(timer)) * Time.deltaTime);
            yield return null;
        }

        switch(request)
        {
            case RequestType.DIRT:
                Spawner.Instance.SpawnGiftWithContents(GiftEntity.Contents.DIRT, false);
                break;
            case RequestType.KITTEN:
                Spawner.Instance.SpawnGiftWithContents(GiftEntity.Contents.KITTEN, false);
                break;
            case RequestType.SAPLING:
                Spawner.Instance.SpawnGiftWithContents(GiftEntity.Contents.SAPLING, false);
                break;
            default:
                break;
        }

        Destroy(this.gameObject);
    }


	public void SetRequest(RequestType newRequest)
	{
        request = newRequest;
	}
}
