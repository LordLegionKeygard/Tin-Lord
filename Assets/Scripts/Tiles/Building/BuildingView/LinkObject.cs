using UnityEngine;

public class LinkObject : MonoBehaviour
{
	public bool isLink;

	public bool isLinkSecond;

	public bool isRestart;

	public Transform link;

	public Transform secondLink;

	private Vector3 _startPos;

	private Quaternion _startRot;

	private void Start()
	{
		_startPos = base.transform.position;
		_startRot = base.transform.rotation;
	}

	private void Update()
	{
		if (isRestart)
		{
			base.transform.position = _startPos;
			base.transform.rotation = _startRot;
		}
		else if (isLink)
		{
			base.transform.position = link.position;
			base.transform.rotation = link.rotation;
		}
		else if (isLinkSecond)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, secondLink.position, Time.deltaTime * 2f);
		}
	}
}
