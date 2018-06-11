using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealingColor : MonoBehaviour {

	private string mood; 
	private GameObject[] baseHealing;
	private GameObject[] particulesHealing;
	public ReaderManager readerManager;

	public void Start()
	{
		baseHealing = GameObject.FindGameObjectsWithTag("baseHealing");
		particulesHealing = GameObject.FindGameObjectsWithTag("particulesHealing");
		mood = readerManager.GetReaderSetting("mood");
		initColor();
	}
		
	void initColor()
	{
		switch (mood)
		{
			case "1":
				// moodName ="paisible";
				getColor(new Color(0.1411765f,0.2901961f,0.1764706f),new Color(0.6235294f,0.5254902f,0.3333333f) );
				break;
			case "2":
				// moodName = "extraordinaire";
				getColor(new Color(0.7264151f,0.119927f,0.423171f), new Color(1,0.7058824f,0.1058824f));
				break;
			case "3":
				// moodName = "mysterieux";
				getColor(new Color(0.09242612f,0.1019608f,0.2941177f),new Color(0.5490196f,0.09411765f,0.4862745f));
				break;
		}
	}

	void getColor(Color baseColor, Color particuleColor)
	{
		foreach (var baseH in baseHealing)
		{
			ParticleSystem ps = baseH.GetComponent<ParticleSystem>();
			ParticleSystem.MainModule psmain = ps.main;
			psmain.startColor = baseColor;

		}
		
		foreach (var particuleH in particulesHealing)
		{
			ParticleSystem ps = particuleH.GetComponent<ParticleSystem>();
			ParticleSystem.MainModule psmain = ps.main;
			psmain.startColor = particuleColor;
		}
	}
}
