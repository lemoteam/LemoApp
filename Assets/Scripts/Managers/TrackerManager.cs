using UnityEngine;
using Vuforia;
using Image = UnityEngine.UI.Image;


public class TrackerManager : MonoBehaviour, ITrackableEventHandler
{
	protected TrackableBehaviour mTrackableBehaviour;
	private GameObject scan;
	private GameObject text;

	protected virtual void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		scan = GameObject.FindWithTag("scan");
		text = GameObject.FindWithTag("text");
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			Debug.Log("Custoooom Trackable " + mTrackableBehaviour.TrackableName + " found");
			if (!GlobalManager.instance.isLoggin)
			{	
				AuthManager.Instance.OnLogin(mTrackableBehaviour.TrackableName);
				scan.GetComponent<Image>().color = new Color32(0,0,0,100);
				text.SetActive(false); 
			}
		}
		else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
			newStatus == TrackableBehaviour.Status.NOT_FOUND)
		{
			scan.GetComponent<Image>().color = new Color32(255,255,255,100);
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
		}

	}
}
