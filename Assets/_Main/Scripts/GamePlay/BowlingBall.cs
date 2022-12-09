using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.BallTrigger))
        {
            LabutAnimationPlay(other);  
            if(other.transform.parent.Find("Pins").GetComponent<Animator>().GetInteger("PinIndex") == 0)
            {
                other.transform.root.Find("BowlingArea").Find("DubaMaker").GetComponent<PinMaker>().isTrigger = true;
                other.transform.root.Find("BowlingArea2").Find("DubaMaker").GetComponent<PinMaker>().isTrigger = true;
                other.transform.root.Find("BowlingArea3").Find("DubaMaker").GetComponent<PinMaker>().isTrigger = true;
                other.transform.root.Find("BowlingArea4").Find("DubaMaker").GetComponent<PinMaker>().isTrigger = true;
                Destroy(gameObject, 0.5f);
            }
            if (other.transform.parent.Find("Pins").GetComponent<Animator>().GetInteger("PinIndex") == 1)
            {
                other.transform.root.Find("BowlingArea").Find("DubaMaker").GetComponent<PinMaker>().isTrigger2 = true;
                other.transform.root.Find("BowlingArea2").Find("DubaMaker").GetComponent<PinMaker>().isTrigger2 = true;
                other.transform.root.Find("BowlingArea3").Find("DubaMaker").GetComponent<PinMaker>().isTrigger2 = true;
                other.transform.root.Find("BowlingArea4").Find("DubaMaker").GetComponent<PinMaker>().isTrigger2 = true;
                Destroy(gameObject, 0.5f);
            }
        }

        if (other.CompareTag("VipTrigger"))
        {
            LabutAnimationPlay(other);
            if (other.transform.parent.Find("Pins").GetComponent<Animator>().GetInteger("PinIndex") == 0)
            {
                other.transform.parent.Find("BowlingArea4").Find("DubaMaker").GetComponent<PinMaker>().isTrigger = true;
                Destroy(gameObject, 0.5f);
            }
            if (other.transform.parent.Find("Pins").GetComponent<Animator>().GetInteger("PinIndex") == 1)
            {
                other.transform.parent.Find("BowlingArea4").Find("DubaMaker").GetComponent<PinMaker>().isTrigger2 = true;
                Destroy(gameObject, 0.5f);
            }
        }
    }

    private void LabutAnimationPlay(Collider other)
    {
        other.transform.parent.Find("Pins").GetComponent<Animator>().SetInteger("PinIndex", Random.Range(0, 2));
        other.transform.parent.Find("Pins").GetComponent<Animator>().SetTrigger("Pin");
    }
}