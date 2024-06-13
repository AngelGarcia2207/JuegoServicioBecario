using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageJugar : MonoBehaviour
{
    public static bool initialized = false;
    public static bool[] levelsCompleted = new bool[5];
    [SerializeField] private Animator transicion;
    private float waitTime = 1f;
    
    void Start()
    {
        if(initialized == false)
        {
            initialized = true;
            for(int i = 0; i < 5; i++)
            {
                levelsCompleted[i] = false;
            }
        }
    }

    public void Play()
    {
        if(levelsCompleted[0] == false)
        {
            transicion.SetTrigger("Fade");
            StartCoroutine(ChangeLevel1());
        }
        else if(levelsCompleted[1] == false)
        {
            transicion.SetTrigger("Fade");
            StartCoroutine(ChangeLevel2());
        }
        else if(levelsCompleted[2] == false)
        {
            transicion.SetTrigger("Fade");
            StartCoroutine(ChangeLevel3());
        }
        else if(levelsCompleted[3] == false)
        {
            transicion.SetTrigger("Fade");
            StartCoroutine(ChangeLevel4());
        }
        else
        {
            transicion.SetTrigger("Fade");
            StartCoroutine(ChangeLevel5());
        }
    }

    IEnumerator ChangeLevel1()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Nivel1", LoadSceneMode.Single);
    }

    IEnumerator ChangeLevel2(){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Nivel2", LoadSceneMode.Single);
    }

    IEnumerator ChangeLevel3(){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Nivel3", LoadSceneMode.Single);
    }

    IEnumerator ChangeLevel4(){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Nivel4", LoadSceneMode.Single);
    }

    IEnumerator ChangeLevel5(){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Nivel5", LoadSceneMode.Single);
    }
}
