using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.IO;

public class Upgrades : MonoBehaviour {

    protected Dictionary<string, string> upgradeShip0;
    protected Dictionary<string, string> upgradeShip1;

    protected Text[] values;

    protected Slider[] sliders;

    protected int initialPoints;

	// Use this for initialization
	void Start () {

        upgradeShip0 = new Dictionary<string, string>();
        upgradeShip1 = new Dictionary<string, string>();

        values = GetComponentsInChildren<Text>();

        foreach (Text value in values)
            Debug.Log("value:" + value.text);

        sliders = GetComponentsInChildren<Slider>();

        if (loadFile(0) == true) {

            values[0].text = upgradeShip0["Health"];
            values[1].text = upgradeShip0["Power"];
            values[2].text = upgradeShip0["Speed"];
            values[3].text = upgradeShip0["Handling"];
        }

        if (loadFile(1) == true)
        {
            values[16].text = upgradeShip1["Health"];
            values[17].text = upgradeShip1["Power"];
            values[18].text = upgradeShip1["Speed"];
            values[19].text = upgradeShip1["Handling"];
        }

        //Ship0 Sliders
        sliders[0].maxValue = sliders[1].maxValue = sliders[2].maxValue = sliders[3].maxValue = initialPoints = int.Parse(values[4].text);

        //Ship1 Sliders
        sliders[4].maxValue = sliders[5].maxValue = sliders[6].maxValue = sliders[7].maxValue = initialPoints = int.Parse(values[20].text);
	}

    public bool loadFile(int id) {

        string fileName;

        if(id == 0)
            fileName = "upgradesShip0.txt";
        else
            fileName = "upgradesShip1.txt";

        if (File.Exists(fileName))
        {

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
                                if (id == 0)
                                    upgradeShip0.Add(entries[0], entries[1]);
                                else
                                    upgradeShip1.Add(entries[0], entries[1]);

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

        string ship0File = "upgradesShip0.txt";
        string ship1File = "upgradesShip1.txt";

        try {

            string[] contentsShip0 = { "Health " + values[1].text, "Power " + values[2].text, 
                                  "Speed " + values[3].text, "Handling " + values[4].text };

            string[] contentsShip1 = { "Health " + values[16].text, "Power " + values[17].text, 
                                  "Speed " + values[18].text, "Handling " + values[19].text };

            if (File.Exists(ship0File))
            {
                System.IO.File.WriteAllText(ship0File, string.Empty);

                System.IO.File.WriteAllLines(ship0File, contentsShip0);
            }

            if (File.Exists(ship1File))
            {
                System.IO.File.WriteAllText(ship1File, string.Empty);

                System.IO.File.WriteAllLines(ship1File, contentsShip1);
            }

            //Creatig the file for Ship0
            StreamWriter streamWriter = File.CreateText(ship0File);

            streamWriter.WriteLine("Health " + values[1].text);
            streamWriter.WriteLine("Power " + values[2].text);
            streamWriter.WriteLine("Speed " + values[3].text);
            streamWriter.WriteLine("Handling " + values[4].text);

            streamWriter.Close();

            //Creatig the file for Ship1
            streamWriter = File.CreateText(ship1File);

            streamWriter.WriteLine("Health " + values[16].text);
            streamWriter.WriteLine("Power " + values[17].text);
            streamWriter.WriteLine("Speed " + values[18].text);
            streamWriter.WriteLine("Handling " + values[19].text);

            streamWriter.Close();
        }
        catch (System.Exception e) {

            Debug.Log("Exception when reading the file!" + e.ToString());
        }
    }

    public void addHealth(int id) {

        if (id == 0)
        {
            //read slider value
            int sliderValue = (int)sliders[0].value;

            //update health value
            values[0].text = sliderValue.ToString();

            //update points
            values[4].text = (initialPoints - sliderValue).ToString();
        }
        else if(id == 1)
        {
            //read slider value
            int sliderValue = (int)sliders[4].value;

            //update health value
            values[16].text = sliderValue.ToString();

            //update points
            values[20].text = (initialPoints - sliderValue).ToString();
        }
    }

    public void addPower(int id)
    {
        if (id == 0)
        {
            //read slider value
            int sliderValue = (int)sliders[1].value;

            //update power value
            values[1].text = sliderValue.ToString();

            //update points
            values[4].text = (initialPoints - sliderValue).ToString();
        }
        else if (id == 1)
        {
            //read slider value
            int sliderValue = (int)sliders[5].value;

            //update health value
            values[17].text = sliderValue.ToString();

            //update points
            values[20].text = (initialPoints - sliderValue).ToString();
        }   
    }

    public void addSpeed(int id) {

        if (id == 0)
        {

            //read slider value
            int sliderValue = (int)sliders[2].value;

            //update speed value
            values[2].text = sliderValue.ToString();

            //update points
            values[4].text = (initialPoints - sliderValue).ToString();
        }
        else if (id == 1)
        {
            //read slider value
            int sliderValue = (int)sliders[6].value;

            //update health value
            values[18].text = sliderValue.ToString();

            //update points
            values[20].text = (initialPoints - sliderValue).ToString();
        }
    }

    public void addHandling(int id)
    {
        if (id == 0) {
            //read slider value
            int sliderValue = (int)sliders[3].value;

            //update handling value
            values[3].text = sliderValue.ToString();

            //update points
            values[4].text = (initialPoints - sliderValue).ToString();
        }
        else if (id == 1)
        {
            //read slider value
            int sliderValue = (int)sliders[7].value;

            //update health value
            values[19].text = sliderValue.ToString();

            //update points
            values[20].text = (initialPoints - sliderValue).ToString();
        }
    }


    void Update()
    {

        if (int.Parse(values[4].text) == 0)
        {
            sliders[0].interactable = false;
            sliders[1].interactable = false;
            sliders[2].interactable = false;
            sliders[3].interactable = false;
        }

        if (int.Parse(values[20].text) == 0)
        {
            sliders[4].interactable = false;
            sliders[5].interactable = false;
            sliders[6].interactable = false;
            sliders[7].interactable = false;
        }

    }
}
