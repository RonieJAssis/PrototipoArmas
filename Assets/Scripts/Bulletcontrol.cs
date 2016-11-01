using UnityEngine;
using System.Collections;

public class Bulletcontrol : MonoBehaviour {

    private Vector2 speed = new Vector2(15, 0);
    private Rigidbody2D rbBullit;

    void Start()
    {
        rbBullit = GetComponent<Rigidbody2D>();
        rbBullit.velocity = speed * this.transform.localScale.x;
        Destroy(gameObject, 2f);

    }


}
