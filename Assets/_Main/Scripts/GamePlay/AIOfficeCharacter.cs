using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI.ProceduralImage;

public class AIOfficeCharacter : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform workTarget;
    Animator anim;
    Transform lookAt;
    bool officer = true;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        lookAt = GameObject.Find("Reception").transform.Find("LookAt").transform;
        workTarget = GameObject.Find("Reception").transform.Find("Worker").transform;
        anim = GetComponent<Animator>();

        Work();
    }

    private void Update()
    {
        anim.SetFloat("speed", navMeshAgent.velocity.magnitude);

        if (anim.GetBool("isOffice") == true && anim.GetFloat("speed") < 0.01f)
            transform.LookAt(lookAt);

        StartCoroutine(BoxColliderSetFalseAndTrue());

    }
    private void Work()
    {
        navMeshAgent.SetDestination(workTarget.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case Tags.OfficeArea:
                if(anim != null)
                    anim.SetBool("isOffice", true);
                break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case Tags.OfficeArea:
                if (anim != null)
                    anim.SetBool("isOffice", true);
                Color cColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                cColor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = cColor;
                other.GetComponent<OfficeArea>().SetNewCustomer();
                break;
        }
    }

    private IEnumerator BoxColliderSetFalseAndTrue()
    {
        if (officer)
        {
            GetComponent<BoxCollider>().enabled = true;
            
            yield return new WaitForSeconds(7);
            officer = false;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(15f);
            officer = true;
        }
    }
}