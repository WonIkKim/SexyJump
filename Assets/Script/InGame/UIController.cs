using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private float IndexGage = 0;

    private Image GageImage;
    //private bool isGageAni = false;

    void Start()
    {
        GageImage = GameObject.Find("Gage").GetComponent<Image>();
    }

   

    public void SetGage(float g)
    {
        IndexGage = g;
        GageImage.fillAmount = IndexGage;
    }

    public float GetGage()
    {
        return IndexGage;
    }

    public void StartGageAnimation()
    {
        StartCoroutine("GageAni");
    }

    public void StopGageAnimation()
    {
        StopCoroutine("GageAni");
    }

    IEnumerator GageAni()
    {
        WaitForSeconds tmps = new WaitForSeconds(.1f);
        for (; ; )
        {
            IndexGage += 0.1f;
            if (IndexGage > 1.1f)
            {
                IndexGage = 0f;
            }

            GageImage.fillAmount = IndexGage;
            yield return tmps;
        }

    }


}
