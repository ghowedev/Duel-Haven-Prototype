using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack1 : MonoBehaviour
{
    public Animator animator;
    public CircleCollider2D hitboxCollider;
    public float delay = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        hitboxCollider = transform.Find("Hitboxes/BasicAttack1").gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.ResetTrigger("BasicAttack1");
            animator.SetTrigger("BasicAttack1");
            // animator.Play("BasicAttack1");
        }
    }

    public void EnableHitbox()
    {
        hitboxCollider.enabled = true;
    }

    public void DisableHitbox()
    {
        hitboxCollider.enabled = false;
    }
}