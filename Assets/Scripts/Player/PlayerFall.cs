using UnityEngine;
using System.Collections;

public class PlayerFall : MonoBehaviour 
{
	public Vector2 respawnPoint = new Vector2(0, 4);

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag.Equals("Player"))
		{
			other.transform.position = respawnPoint;
			other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}
}
