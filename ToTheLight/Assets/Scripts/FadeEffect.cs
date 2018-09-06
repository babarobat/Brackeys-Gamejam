using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeEffect : MonoBehaviour {

    private Image _fadePannel;
    public float fadeInTime = 0.5f;
    public float fadeOutTime = .5f;
    
    private void Start()
    {
        Debug.Log("x");
        _fadePannel = GetComponentInChildren<Image>();
       StartCoroutine( FadeInColor( Color.black, fadeInTime));
    }
    public IEnumerator FadeInColor( Color color, float fadeTime)
    {
        
        float t = 0f;
        _fadePannel.color = color;
        _fadePannel.gameObject.SetActive(true);
        var currentColor = color;
        while (t <= fadeTime)
        {
            _fadePannel.color = Color.Lerp(currentColor, new Color(0, 0, 0, 0), t / fadeTime);
            t += Time.deltaTime;
            yield return null;
        }
        _fadePannel.color = new Color(0, 0, 0, 0);

        _fadePannel.gameObject.SetActive(false);
        


    }
    IEnumerator FadeOutColor( Color color, float fadeTime, string sceneName )
    {
        

        float t = 0f;
        _fadePannel.color = new Color(0, 0, 0, 0);
        var currentColor = new Color(0, 0, 0, 0);
        _fadePannel.gameObject.SetActive(true);
        while (t <= fadeTime)
        {
            _fadePannel.color = Color.Lerp(currentColor, color, t / fadeTime);
            t += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        
       


    }
    public void FadeAndLoadScene(string sceneName, Color color, float fadeTime)
    {
        StartCoroutine(FadeOutColor(color, fadeTime, sceneName));

    }
    

}
