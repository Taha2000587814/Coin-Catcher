using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject TubesContainer;
    public float TubesDelay = 0.8f;
    private List<Tube> Tubes = new List<Tube>();

    private void Start()
    {
        Tube[] tubes = GameObject.FindObjectsByType<Tube>(FindObjectsSortMode.InstanceID);
        foreach (Tube tube in tubes)
        {
            Tubes.Add(tube);
            tube.enabled = false;
        }
    }

    public void StartGame()
    {
        GameObject jarObject = (GameObject)Resources.Load("Jar");
        GameObject jar = Instantiate(jarObject, SpawnPoint.position, Quaternion.identity);

        GameObject menuObj = GameObject.Find("Menu_UI");
        menuObj.SetActive(false);

        Animation anim = TubesContainer.GetComponent<Animation>();
        anim.Play("tubedraw");

        Invoke("ActiveTubes", TubesDelay);
    }

    private void ActiveTubes()
    {
        foreach (Tube tube in Tubes)
        {
            tube.enabled = true;
        }
    }

}
