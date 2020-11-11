using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IssueController : MonoBehaviour
{

    public bool dying = false;
    private float h;

    // Start is called before the first frame update
    void Start()
    {
        h = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    public void RemoveIssue()
    {
        dying = true;
        CapsuleCollider2D cc2d = GetComponent("CapsuleCollider2D") as CapsuleCollider2D;
        if (cc2d) {
            cc2d.enabled = false;
        }
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (dying) {
            Vector3 ls = transform.localScale;
            transform.localScale = new Vector2(ls.x + 0.05f, 0.9f * ls.y);
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f * ls.y * h * 0.5f);
        }
    }
}
