using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headBob : MonoBehaviour
{
	Vector3 startingLocation;
	public float Speed = 0.025f;
	public bool IsFullWave = true;
	public Vector3 Amount;
	private float timer = 0.0f;

    private void Start()
    {
		startingLocation = transform.localPosition;
    }
    public void Update()
	{
		var position = transform.localPosition;
		if (!Player.isPlayerMove)
		{
			if (timer == 0 || Mathf.Abs(timer - Mathf.PI) < 0.01)
			{
				return;
			}
		}

		float moveCyclePercent = Mathf.Sin(timer)/4;
		timer += Speed;

		if (timer > Mathf.PI * (IsFullWave ? 2 : 1))
		{
			timer = 0.0f;
		}

		if (Amount.x != 0)
		{
			print(Amount.y);
			position.x = Amount.x * moveCyclePercent;
		}

		if (Amount.y != 0)
		{
            if (position.x > 0)
            {
				position.y = startingLocation.y+(-position.x / 2);
            }
            else
            {
				position.y = startingLocation.y + (position.x / 2);
			}
		}

		if (Amount.z != 0)
		{
			position.z = Amount.z * moveCyclePercent;
		}

		transform.localPosition = position;
	}
}
