using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

public class ProgressBar : MonoBehaviour {

    Image circleImage;

    public float timeToComplete;

    public bool isEnabled = true;

    void Start()
    {
        circleImage = (Image)GetComponent("Image");
        
        circleImage.type = Image.Type.Filled;
        circleImage.fillMethod = Image.FillMethod.Radial360;
        circleImage.fillOrigin = 0;
     
       StartCoroutine(RadialProgress(timeToComplete));
    }

    public IEnumerator RadialProgress(float time)
    {
        float rate = 1 / time;
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * rate;
            if (i > 1)
            {
                circleImage.enabled = false;
                isEnabled = false;
            }

            circleImage.fillAmount = i;
            yield return 0;
        }
    }

}
