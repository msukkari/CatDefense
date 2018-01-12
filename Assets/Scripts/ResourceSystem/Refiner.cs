using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refiner : MonoBehaviour
{

    public int m_refineTime;
    public GameObject m_dropPoint;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Refine(GameObject rr)
    {
        GameObject linked = rr.GetComponent<Resource>().linkedResource;
        Destroy(rr);

        StartCoroutine(RefineCoRoutine(linked));
    }

    private IEnumerator RefineCoRoutine(GameObject linkedResource)
    {
        yield return new WaitForSeconds(m_refineTime);

        GameObject fr = Instantiate(linkedResource);
        fr.transform.position = m_dropPoint.transform.position;
    }
}
