using UnityEngine;

public class sliderController : MonoBehaviour {
	private GameObject indicator;
	private GameObject indicatorS;
	private GameObject[] indexes;
	private float posIndex;
	public PlayerManager playerManager;
	private float previousIndex = -1;
	
	// Use this for initialization
	void Start () {
		indicator = GameObject.FindWithTag("indicatorS");
		indexes = GameObject.FindGameObjectsWithTag("index");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (indicator && indicator.GetComponent<MeshRenderer>().enabled )
		{
			var minimum = float.MaxValue;
			for (int i = 0; i < indexes.Length; i++)
			{
				var num = Vector3.Distance(indexes[i].transform.position, indicator.transform.position);
				if (num < minimum)
				{
					minimum = num;	
					// Debug.Log("<color=black> min distance : "+ minimum +"</color>");
					var currentIndex = i;
					if (previousIndex != currentIndex)
					{
						playerManager.goToCheckpoint(currentIndex);
						previousIndex = currentIndex;
					}
				}
			}
			/*
			Debug.Log("<color=white> distance indic 1 : "+ Vector3.Distance(indexes[0].transform.position,indicator.transform.position) +"</color>");
			Debug.Log("<color=red> distance indic 2 : "+ Vector3.Distance(indexes[1].transform.position,indicator.transform.position) +"</color>");
			Debug.Log("<color=yellow> distance indic 3 : "+ Vector3.Distance(indexes[2].transform.position,indicator.transform.position) +"</color>");
			*/
		}
	}
}
