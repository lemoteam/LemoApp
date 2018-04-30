using UnityEngine;

public class SliderController : MonoBehaviour {

    private Transform max; 
    // private float distance;
    public ReaderManager readerManager;

    // Use this for initialization
    void Start () {
        Debug.Log ("START !!!!!!");
        max = GameObject.Find ("Max").transform;
    }
	
    // Update is called once per frame
    void Update () {
        Vector3 distance = max.position - this.transform.position;
        Debug.Log("position max :"+max);
        Debug.Log("position image target:"+this.transform.position);
        //Debug.Log("distance : "+ distance.magnitude);
        // readerManager.UpdateReaderSettings(parameter);
    }
}
