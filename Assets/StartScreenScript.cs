using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class StartScreenScript : MonoBehaviour
{
    public GameObject SceneFader;
    public SceneFaderScript SFScript;
    public MusicFadeScript MFScript;
    public AudioSource bgm;
    public Text introText;

    private void Start()
    {
        MFScript = gameObject.GetComponent<MusicFadeScript>();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(SceneFader);
    }
    public void teleportPortal(string MapName)
    {
        StartCoroutine(delayFade(MapName));

    }
    private IEnumerator delayFade(string MapName)
    {

        SceneFader.GetComponent<Canvas>().sortingOrder = 1;
        StartCoroutine(SFScript.Fade(SceneFaderScript.FadeDirection.In));
        StartCoroutine(MFScript.FadeOut(bgm, 0.3f));
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(MapName);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeTextToFullAlpha(0.2f, introText));
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(FadeTextToZeroAlpha(0.2f, introText));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SFScript.Fade(SceneFaderScript.FadeDirection.Out));
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
        Destroy(SceneFader);
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}