using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using MoreMountains.NiceVibrations;
using DG.Tweening;

public class PlatformManager : MonoBehaviour
{
    ConstraintSource cs, cs2;
    Transform player;
    Vector3 velocity = Vector3.zero;
    internal float movementTime;
    MainPlatformManager mainPlatformManager;
    PlatformManager platformManager;

    float startingY;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mainPlatformManager = player.GetComponent<MainPlatformManager>();
        startingY = transform.position.y;
    }

    private void Update()
    {
         transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), ref velocity, movementTime);
    }

    public void ParentCons(GameObject platform)
    {
        int index = mainPlatformManager.platformList.IndexOf(transform);

        if (index == -1)
            return;

        platformManager = platform.gameObject.GetComponent<PlatformManager>();
        platformManager.enabled = true;

        cs.sourceTransform = transform;

        if (index == mainPlatformManager.platformList.Count - 1)
        {
            mainPlatformManager.platformList.Add(platform.transform);
        }
        else
        {
            var next = mainPlatformManager.platformList[index + 1];

            mainPlatformManager.platformList.Insert(index + 1, platform.transform);
            cs2.sourceTransform = platform.transform;
            cs2.weight = 1f;

            ParentConstraint nextCons = next.GetComponent<ParentConstraint>();

            if (nextCons.sourceCount > 0)
                nextCons.RemoveSource(0);

            nextCons.AddSource(cs2);
            nextCons.SetTranslationOffset(0, new Vector3(0, 0, 1));
            nextCons.enabled = true;
            nextCons.constraintActive = false;
            nextCons.constraintActive = true;
        }

        Vibrations.Selection();

        platformManager.movementTime = mainPlatformManager.platformList.Count * 0.05f;

        cs.weight = 1f;

        ParentConstraint parentConstraint = platform.GetComponent<ParentConstraint>();

        parentConstraint.AddSource(cs);
        parentConstraint.SetTranslationOffset(0, new Vector3(0, 0, 1));
        parentConstraint.enabled = true;
        parentConstraint.constraintActive = true;

        for (int i = 1; i < mainPlatformManager.platformList.Count; i++)
        {
            mainPlatformManager.platformList[i].GetComponent<PlatformManager>().movementTime = i * 0.05f;
        }

        StartCoroutine(AtmRushScale());
    }

    private IEnumerator AtmRushScale()
    {
        for (int i = 0; i <= mainPlatformManager.platformList.Count - 1; i++)
        {
            Transform obj;

            if (mainPlatformManager.platformList[mainPlatformManager.platformList.Count - 1 - i] == player)
            {
                obj = mainPlatformManager.platformList[mainPlatformManager.platformList.Count - 1 - i];
                obj.parent.transform.DOScale(1.2f, 0.1f);
            }
            else
            {
                obj = mainPlatformManager.platformList[mainPlatformManager.platformList.Count - 1 - i];
                obj.transform.DOScale(1.5f, 0.1f);
            }

            yield return new WaitForSeconds(0.03f);

            if (obj == player)
                obj.parent.transform.DOScale(1f, 0.1f);
            else
            {
                if (obj)
                    obj.transform.DOScale(1f, 0.1f);
            }
        }
    }

    public void ThrowPlatforms()
    {
        int index = mainPlatformManager.platformList.IndexOf(transform);

        for (int i = index; i < mainPlatformManager.platformList.Count; i++)
        {
            mainPlatformManager.platformList[i].GetComponent<PlatformManager>().ThrowThis();
        }
        mainPlatformManager.platformList.RemoveRange(index, mainPlatformManager.platformList.Count - index);
    }

    public void ThrowPlatformsForMoneyDoor()
    {
        int index = mainPlatformManager.platformList.IndexOf(transform);

        for (int i = index + 1; i < mainPlatformManager.platformList.Count; i++)
        {
            mainPlatformManager.platformList[i].GetComponent<PlatformManager>().ThrowThis();
        }
        mainPlatformManager.platformList.RemoveRange(index, mainPlatformManager.platformList.Count - index);

        Destroy(gameObject);
    }

    public void ThrowThis()
    {
        transform.tag = "Platform";
        GetComponent<PlatformManager>().enabled = false;
        GetComponent<Collider>().enabled = false;
        DeactivateParentConstraint();
        Vector3 pos = new Vector3(Random.Range(5, -5), transform.position.y, transform.position.z + Random.Range(7, 15));

        Vibrations.Rigid();

        //if (platformType.type == PlatformType.Type.Good")
        //    platformType.TurnPlatfromToBadWithoutHaptic();
        //else
        //{
        //    DeleteThis();
        //    return;
        //}

        transform.DOJump(pos, 2f, 2, 0.5f).OnComplete(() =>
        {
            //StartCoroutine(ATMRushThrow());
            GetComponent<Collider>().enabled = true;
        });

        //StartCoroutine(ATMRushThrow());
    }

    public void DeleteThis()
    {
        mainPlatformManager.platformList.Remove(transform);
        Vibrations.Warning();
        Destroy(gameObject);
    }

    public void DeactivateParentConstraint()
    {
        GetComponent<ParentConstraint>().constraintActive = false;
        GetComponent<ParentConstraint>().RemoveSource(0);
        GetComponent<ParentConstraint>().enabled = false;
    }

    private IEnumerator ATMRushThrow()
    {
        transform.DOMoveY(startingY + 1f, 0.12f);

        yield return new WaitForSeconds(0.15f);

        transform.DOMoveY(startingY, 0.12f);

        yield return new WaitForSeconds(0.15f);

        transform.DOMoveY(startingY + 0.75f, 0.9f);

        yield return new WaitForSeconds(0.12f);

        transform.DOMoveY(startingY, 0.9f);

        yield return new WaitForSeconds(0.12f);

        transform.DOMoveY(startingY + 0.5f, 0.5f);

        yield return new WaitForSeconds(0.6f);

        transform.DOMoveY(startingY, 0.5f);
    }

    public void DeletePlatform()
    {
        if (mainPlatformManager.platformList.IndexOf(transform) == mainPlatformManager.platformList.Count - 1)
        {
            mainPlatformManager.platformList.Remove(transform);
            Destroy(gameObject);
            return;
        }

        var nextPlatform = mainPlatformManager.platformList[mainPlatformManager.platformList.IndexOf(transform) + 1].gameObject;
        var previousPlatform = mainPlatformManager.platformList[mainPlatformManager.platformList.IndexOf(transform) - 1].gameObject;

        nextPlatform.gameObject.GetComponent<ParentConstraint>().enabled = false;

        nextPlatform.GetComponent<ParentConstraint>().RemoveSource(0);
        nextPlatform.GetComponent<PlatformManager>().cs.sourceTransform = previousPlatform.transform;
        nextPlatform.GetComponent<PlatformManager>().cs.weight = 1f;
        nextPlatform.GetComponent<ParentConstraint>().AddSource(nextPlatform.GetComponent<PlatformManager>().cs);
        nextPlatform.gameObject.GetComponent<ParentConstraint>().SetTranslationOffset(0, new Vector3(0, 0, 1));
        nextPlatform.gameObject.GetComponent<ParentConstraint>().enabled = true;
        nextPlatform.gameObject.GetComponent<ParentConstraint>().constraintActive = true;

        mainPlatformManager.platformList.Remove(transform);

        for (int i = 1; i < mainPlatformManager.platformList.Count; i++)
        {
            mainPlatformManager.platformList[i].GetComponent<PlatformManager>().movementTime = i * 0.05f;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            if (gameObject.tag != "Platform")
            {
                other.tag = "Untagged";
                ParentCons(other.gameObject);
                MoneyManager.Instance.CreateMoney(1, false);
            }
        }
    }
}
