using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBorder : MonoBehaviour
{
    [SerializeField] float force = 5;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<Rigidbody2D>().AddForce((transform.position - collision.transform.position) * force);
        }
    }
}
