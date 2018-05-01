using UnityEngine;

public class SliderController : MonoBehaviour {

    private Transform max; 
    // private float distance;
    public ReaderManager readerManager;

    // Use this for initialization
    void Start () {
        Debug.Log ("START !!!!");
        max = GameObject.Find ("Max").transform;
    }
	
    // Update is called once per frame
    void Update () {
        Debug.Log("position de lmnt: "+this.transform.position);
        var distance = max.position.x - this.transform.position.x;
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
}
