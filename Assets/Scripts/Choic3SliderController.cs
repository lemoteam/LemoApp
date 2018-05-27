using System;
using System.Globalization;
using UnityEngine;

public class Choic3SliderController : MonoBehaviour {
    private GameObject indicator;
    private GameObject[] indexes;
    private float previousIndex = -1;	
    // Use this for initialization
    void Start () {
        indicator = GameObject.FindWithTag("indicator");
        indexes = GameObject.FindGameObjectsWithTag("index");
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (indicator.GetComponent<MeshRenderer>().enabled)
        {
            getMinDistance();
        }
    }
    
    private void getMinDistance()
    {
        var min = float.MaxValue;
        for (int i = 1; i < indexes.Length; i++)
        {
            var distance = Vector3.Distance(indexes[i].transform.position, indicator.transform.position);

            if (distance < min)
            {
                min = distance;
                var currentIndex = i;
                if (previousIndex != currentIndex)
                {
                    GlobalManager.instance.dynamicHasChanded = true;
                    Debug.Log(indexes[i].name + " index est : " + i);
                    var dynamicValue = Single.Parse(indexes[i].name) / 10;                    
                    Debug.Log(dynamicValue);
                    transform.localScale = new Vector3(transform.localScale.x, 4.7f * dynamicValue, transform.localScale.z);
                    var prop = GlobalManager.instance.reader.GetType().GetProperty("dynamic");
                    if (prop != null) prop.SetValue(GlobalManager.instance.reader,dynamicValue, null);
                    previousIndex = currentIndex;
                }
            }
        }
    }
}
