using UnityEngine;

public class SliderController : MonoBehaviour {

    private Transform max; 
    // private float distance;
    public ReaderManager readerManager;
    private GameObject dynamicObj;

    // Use this for initialization
    void Start () {
        max = GameObject.Find ("Max").transform;
        dynamicObj = GameObject.Find("dynamic");
    }
	
    // Update is called once per frame
    void Update () {
        Debug.Log("position de lmnt: "+this.transform.position);
        var distance = max.position.x - this.transform.position.x;
        var scaleMultiplier = Map(2.0f,0.5f,170.0f,300.0f,distance);
        dynamicObj.transform.localScale = new Vector3(0, scaleMultiplier, 0);
        if (distance > 300 || distance < 170)
        {
            GlobalManager.instance.dynamicHasChanded = true;
        }

        if (distance > 300)
        {
            var prop = GlobalManager.instance.reader.GetType().GetProperty("dynamic");
            if (prop != null) prop.SetValue(GlobalManager.instance.reader, 1, null);
        }

        if (distance < 170)
        {
            var prop = GlobalManager.instance.reader.GetType().GetProperty("dynamic");
            if (prop != null) prop.SetValue(GlobalManager.instance.reader, 2, null);  
        } 
       // Debug.Log("position max :"+distance);
        // Debug.Log("position image target:"+this.transform.position);
        Debug.Log("distance : "+ distance);
        // readerManager.UpdateReaderSettings(parameter);
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
