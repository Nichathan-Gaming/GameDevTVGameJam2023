using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    Camera cam;

    [SerializeField] float speed = 5;

    [SerializeField] Transform gunTransform;
    int gunZ = 0;

    [SerializeField] Animator anim;

    [SerializeField] Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mPos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(mPos.y, mPos.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        Vector3 moveTo = Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles;

        //if one is 180-360 and the other is 180-0, it will scroll backwards, use this to avoid that.
        if (Vector3.Distance(transform.eulerAngles, moveTo) > 180) moveTo += new Vector3(0, 0, 360 * ((transform.eulerAngles.z > moveTo.z) ? 1 : -1));

        transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, moveTo, speed * Time.deltaTime);

        if(transform.eulerAngles.z < 90 || transform.eulerAngles.z > 270)
        {
            gunTransform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else
        {
            gunTransform.localScale = new Vector3(0.5f, -0.5f, 1);
        }

        gunZ = 0;
        if (transform.eulerAngles.z >= 45 && transform.eulerAngles.z < 135)
        {
            gunZ = 1;
            anim.SetFloat("MoveY", 1);
            anim.SetFloat("MoveX", 0);
        }
        else if (transform.eulerAngles.z >= 135 && transform.eulerAngles.z < 225)
        {
            anim.SetFloat("MoveY", 0);
            anim.SetFloat("MoveX", -1);
        }
        else if (transform.eulerAngles.z >= 225 && transform.eulerAngles.z < 315)
        {
            anim.SetFloat("MoveY", -1);
            anim.SetFloat("MoveX", 0);
        }
        else
        {
            anim.SetFloat("MoveY", 0);
            anim.SetFloat("MoveX", 1);
        }
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y-1, gunZ);
    }
}
