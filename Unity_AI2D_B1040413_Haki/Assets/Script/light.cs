using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public Transform traget;

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, traget.position, 20 * Time.deltaTime);
    }
}
