using System.Collections;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    private GameObject tutorial;
    private void Start()
    {
        transform.SetAsLastSibling();
    }

    public void OpenTutorial(string name, float remainingTime, float startingTime) // Use This
    {
        StartCoroutine(OpenTutorialByName(name, remainingTime, startingTime));
    }

    private IEnumerator OpenTutorialByName(string name, float time, float startingTime)
    {
        CloseTutorial();
        yield return new WaitForSeconds(startingTime);
        tutorial = transform.Find(name).gameObject;
        tutorial.SetActive(true);
        yield return new WaitForSeconds(time);
        CloseTutorial();
    }

    private void CloseTutorial()
    {
        if (tutorial)
        {
            tutorial.SetActive(false);
            tutorial = null;
        }
    }
}