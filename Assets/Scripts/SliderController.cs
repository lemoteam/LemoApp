using UnityEngine;

public class SliderController : MonoBehaviour {

    private Transform max; 
    // private float distance;
    public ReaderManager readerManager;
    private GameObject dynamicObj;
    private GameObject sliderMarker;
    private float firstDistance;
    private bool isReady = false;
	
    // Update is called once per frame
    void Update () {

        if (!isReady)
        {
            max = GameObject.FindWithTag("sliderRef").transform;
            sliderMarker = GameObject.FindWithTag("sliderMarker");

            if (max && sliderMarker)
            {
                firstDistance =  max.position.x - sliderMarker.transform.position.x;
                isReady = true;
            }
        }
        else
        {
            if (sliderMarker.GetComponent<MeshRenderer>().enabled) {
                var distance = max.position.x - sliderMarker.transform.position.x;
                if (firstDistance != distance)
                {
                    var scaleMultiplier = Map(6.0f,-1.5f,140.0f,370.0f,distance);
                    transform.localScale = new Vector3(transform.localScale.x, scaleMultiplier, transform.localScale.z);
                    if (distance > 370 || distance < 140)
                    {
                        GlobalManager.instance.dynamicHasChanded = true;
                    }
    
                    if (distance > 370)
                    {
                        var prop = GlobalManager.instance.reader.GetType().GetProperty("dynamic");
                        if (prop != null) prop.SetValue(GlobalManager.instance.reader, 1, null);
                    }
    
                    if (distance < 140)
                    {
                        var prop = GlobalManager.instance.reader.GetType().GetProperty("dynamic");
                        if (prop != null) prop.SetValue(GlobalManager.instance.reader, 2, null);  
                    } 
                    // Debug.Log("position max :"+distance);
                    // Debug.Log("position image target:"+this.transform.position);
                    Debug.Log("distance : "+ distance);
                    // readerManager.UpdateReaderSettings(parameter); 
                }
            }
        }
    }
    
    public float Map(float from, float to, float from2, float to2, float value) {
        if(value <= from2) {
            return from;
        } else if(value >= to2) {
            return to;
        } else {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }
}
