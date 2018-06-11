using UnityEngine;
using Vuforia;

public class StopSound : MonoBehaviour,
	ITrackableEventHandler
{

	#region PRIVATE_MEMBER_VARIABLES

	private TrackableBehaviour mTrackableBehaviour;
	private AudioSource audioSource;

	#endregion // PRIVATE_MEMBER_VARIABLES

	#region UNTIY_MONOBEHAVIOUR_METHODS

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	#endregion // UNTIY_MONOBEHAVIOUR_METHODS

	#region PUBLIC_METHODS

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}

	#endregion // PUBLIC_METHODS

	#region PRIVATE_METHODS
	
	private void OnEnable()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.Play();	
		Debug.Log("Replay");
	}

	private void OnTrackingLost()
	{
		audioSource.Stop();	
		Debug.Log("ciaaaao");
	}
	
	private void OnTrackingFound()
	{
		audioSource.Play();
		
		audioSource.volume = 0.15f;
		Debug.Log("Yeeaah");
	}
			
	#endregion // PRIVATE_METHODS
}    


