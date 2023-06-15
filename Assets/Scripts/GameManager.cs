using UnityEngine;
public class GameManager : MonoBehaviour
{
	//1
	//public RogueSkillController character;
	public bool cameraFollows = true;
	public CameraBounds cameraBounds;
	public Transform player;

	//2
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	void Update()
	{
		if (cameraFollows)
		{
			cameraBounds.SetXPosition(player.position.x);
			Debug.Log("Player Position" + player.position);	
		}
	}
}
