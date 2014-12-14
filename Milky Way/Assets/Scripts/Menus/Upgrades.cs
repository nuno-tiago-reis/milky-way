using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.IO;

public class Upgrades : MonoBehaviour {

    protected string fileName = "upgrades.txt";

    protected Dictionary<string, string> spaceshipUpgrades;

    protected Text[] values;

    protected Slider[] sliders;

    protected int initialPoints;

	// Use this for initialization
	void Start () {

        spaceshipUpgrades = new Dictionary<string, string>();

        values = GetComponentsInChildren<Text>();
        
        foreach(Text value in values)
            Debug.Log("v = " + value.text);

        sliders = GetComponentsInChildren<Slider>();

        if (loadFile() == true) {

            values[1].text = spaceshipUpgrades["Health"];
            values[2].text = spaceshipUpgrades["Power"];
            values[3].text = spaceshipUpgrades["Speed"];
            values[4].text = spaceshipUpgrades["Handling"];
        }

        sliders[0].maxValue = sliders[1].maxValue = sliders[2].maxValue = sliders[3].maxValue = initialPoints = int.Parse(values[5].text);
	}

    public bool loadFile() {

        if (File.Exists(fileName)) {

            try {

                string line;

                StreamReader streamReader = new StreamReader(fileName);

                using (streamReader) {

                    do {

                        line = streamReader.ReadLine();

                        if (line != null) {

                            string[] entries = line.Split(' ');

                            if (entries.Length > 0) {

                                // Upgrade Type, value
                                spaceshipUpgrades.Add(entries[0], entries[1]);

                            }
                        }
                    }
                    while (line != null);

                    streamReader.Close();

                    return true;
                }
            }
            catch (System.Exception e)
            {

                Debug.Log("Exception when reading the file!" + e.ToString());

                return false;
            }
        }
        return false;
    }

    public void writeFile() {

        try {

            string[] contents = { "Health " + values[1].text, "Power " + values[2].text, 
                                  "Speed " + values[3].text, "Handling " + values[4].text };

            if (File.Exists(fileName))
            {
                System.IO.File.WriteAllText(fileName, string.Empty);

                System.IO.File.WriteAllLines(fileName, contents);
            }

            StreamWriter streamWriter = File.CreateText(fileName);

            streamWriter.WriteLine("Health " + values[1].text);
            streamWriter.WriteLine("Power " + values[2].text);
            streamWriter.WriteLine("Speed " + values[3].text);
            streamWriter.WriteLine("Handling " + values[4].text);

            streamWriter.Close();
        }
        catch (System.Exception e) {

            Debug.Log("Exception when reading the file!" + e.ToString());
        }
    }

    public void addHealth() {

        //read slider value
        int sliderValue = (int)sliders[0].value;

        //update health value
        values[1].text = sliderValue.ToString();

        //update points
        values[5].text = (initialPoints - sliderValue).ToString();
    }

    public void addPower()
    {

        //read slider value
        int sliderValue = (int)sliders[1].value;

        //update power value
        values[2].text = sliderValue.ToString();

        //update points
        values[5].text = (initialPoints - sliderValue).ToString();
    }

    public void addSpeed() {

        //read slider value
        int sliderValue = (int)sliders[2].value;

        //update speed value
        values[3].text = sliderValue.ToString();

        //update points
        values[5].text = (initialPoints - sliderValue).ToString();  
    }

    public void addHandling()
    {

        //read slider value
        int sliderValue = (int)sliders[3].value;

        //update handling value
        values[4].text = sliderValue.ToString();

        //update points
        values[5].text = (initialPoints - sliderValue).ToString();
    }
}
