using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLevel : MonoBehaviour
{
    [SerializeField] private Animator transicion;
    private float waitTime = 2f;
    
    public void ToLevel1()
    {
        transicion.SetTrigger("Fade");
        StartCoroutine(ChangeLevel1());
    }

    IEnumerator ChangeLevel1(){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Nivel1", LoadSceneMode.Single);
    }

    public void ToLevel2()
    {
        transicion.SetTrigger("Fade");
        StartCoroutine(ChangeLevel2());
    }

    IEnumerator ChangeLevel2(){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Nivel2", LoadSceneMode.Single);
    }

    public void ToLevel3()
    {
        transicion.SetTrigger("Fade");
        StartCoroutine(ChangeLevel3());
    }

    IEnumerator ChangeLevel3(){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Nivel3", LoadSceneMode.Single);
    }

    public void ToLevel4()
    {
        transicion.SetTrigger("Fade");
        StartCoroutine(ChangeLevel4());
    }

    IEnumerator ChangeLevel4(){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Nivel4", LoadSceneMode.Single);
    }

    public void ToLevel5()
    {
        transicion.SetTrigger("Fade");
        StartCoroutine(ChangeLevel5());
    }

    IEnumerator ChangeLevel5(){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Nivel5", LoadSceneMode.Single);
    }
}
