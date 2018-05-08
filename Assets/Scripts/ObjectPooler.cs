using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	// [System.Serializable]
	/*public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int radius;
		public int size; 
	} */


	public GameObject gem;
	private Vector3 gemPosition;
	private Vector3 currentGemPosition;
	
	private bool isLevitate;
	private bool isCurrentPositionSet;
	
	private float gemAmplitude = 5.0f;
	private float gemOmega = 5.0f;
	private float gemReplaceFraction = 0;
	private float gemReplaceSpeed = 1f;
	private float RotationSpeed = 0f;
	
	//public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;
	

	
	void Start()
	{
		isLevitate = true;
		gemPosition = gem.transform.position;
		poolDictionary = new Dictionary<string, Queue<GameObject>>();
		/*foreach (var pool in pools)
		{
			var objectPool = new Queue<GameObject>();

			for (var i = 0; i < pool.size; i++)
			{
				// Calc size
				var angle = (360 / pool.size) * i;
				var posX = Math.Sin((angle * Math.PI / 180)) * pool.radius;
				var posY = Math.Cos((angle * Math.PI / 180)) * pool.radius;
				
				GameObject obj = Instantiate(pool.prefab);
				// Push obj clones to ObjectPooler :)
				obj.transform.parent = transform;
				obj.SetActive(true);
				obj.transform.position = new Vector3(ToSingle(posX), 0, ToSingle(posY));
				objectPool.Enqueue(obj);
			}

			poolDictionary.Add(pool.tag, objectPool);
		}*/
	}


	private void FixedUpdate()
	{
		transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);

		if (isLevitate)
		{			
			var newGemPosition = gem.transform.position;
			newGemPosition.y += (Mathf.Cos(Time.time) / 10f)* Time.deltaTime;
			gem.transform.position = newGemPosition ;
			
		} else {

			if (!isCurrentPositionSet) {
				currentGemPosition = gem.transform.position;
				isCurrentPositionSet = true;
			}


			if (!(gemReplaceFraction < 1)) return;
			gemReplaceFraction += Time.deltaTime * gemReplaceSpeed;
			gem.transform.position = Vector3.Lerp(currentGemPosition, gemPosition, gemReplaceFraction);
		}
		
	}


	public void Levitate(string tag) {
		if (!poolDictionary.ContainsKey(tag)) return;

		isLevitate = true;
		isCurrentPositionSet = false;
		gemReplaceFraction = 0;
		RotationSpeed = 5f;
		
		/*foreach (var pool in pools) 
		{
			for (var i = 0; i < pool.size; i++)
			{
				var objectToSpawn = poolDictionary[tag].Dequeue();
				var gemElement = objectToSpawn.GetComponent<GemElement>();
				gemElement.instance.LevitationOn(); 
				poolDictionary[tag].Enqueue(objectToSpawn);
			}
		}*/
	}



	public void Gravitate(string tag)
	{
		if (!poolDictionary.ContainsKey(tag)) return;

		isLevitate = false;
		RotationSpeed = 0f;
		
		/*foreach (var pool in pools) 
		{
			for (var i = 0; i < pool.size; i++)
			{
				var objectToSpawn = poolDictionary[tag].Dequeue();
				var gemElement = objectToSpawn.GetComponent<GemElement>();
				gemElement.instance.LevitationOff(); 
				poolDictionary[tag].Enqueue(objectToSpawn);	
			}
		}*/
	}

	
	private static float ToSingle(double value)
	{
		return (float)value;
	}
}
