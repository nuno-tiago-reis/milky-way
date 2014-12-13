using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.IO;

public class Upgrades : MonoBehaviour {

    protected string fileName = "upgrades.txt";

    protected Dictionary<string, int> spaceshipUpgrades;

    protected Text[] values;

    protected Slider[] sliders;

    protected int initialPoints;

	// Use this for initialization
	void Start () {

        spaceshipUpgrades = new Dictionary<string, int>();

        values = GetComponentsInChildren<Text>();

        sliders = GetComponentsInChildren<Slider>();

        sliders[0].maxValue = sliders[1].maxValue = initialPoints = int.Parse(values[3].text);

        loadFile();
	}

    public bool loadFile() {

         try {

             string line;

             StreamReader streamReader = new StreamReader(fileName);

             using (streamReader) {

                 do {

                     line = streamReader.ReadLine();

                     if (line != null) {

                         string[] entries = line.Split(' ');

                         if (entries.Length > 0) {

                             spaceshipUpgrades.Add(entries[0], int.Parse(entries[1]));

                         }
                     }
                 }
                 while (line != null);

                 streamReader.Close();

                 return true;
             }
         }
         catch (System.Exception e) {

             Debug.Log("Exception when reading the file!" + e.ToString());

             return false;
         }
    }

    public bool writeFile() {

        try {

            string[] contents = { "Health " + values[1], "Speed " + values[2] };

            if (File.Exists(fileName))
            {
                System.IO.File.WriteAllText(fileName, string.Empty);

                System.IO.File.WriteAllLines(fileName, contents);

                return true;
            }

            StreamWriter streamWriter = File.CreateText(fileName);

            streamWriter.WriteLine("Health " + values[1]);
            streamWriter.WriteLine("Speed " + values[2]);

            streamWriter.Close();

            return true;
        }
        catch (System.Exception e) {

            Debug.Log("Exception when reading the file!" + e.ToString());

            return false;
        }
    }

    public void addHealth() {

        //read slider value
        int sliderValue = (int)sliders[0].value;

        //update health value
        values[1].text = sliderValue.ToString();

        //update points
        values[3].text = (initialPoints - sliderValue).ToString();
    }

    public void addSpeed() {

        //read slider value
        int sliderValue = (int)sliders[1].value;

        //update health value
        values[2].text = sliderValue.ToString();

        //update points
        values[3].text = (initialPoints - sliderValue).ToString();  
    }
}
