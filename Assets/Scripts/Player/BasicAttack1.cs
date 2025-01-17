using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack1 : MonoBehaviour
{
    public CircleCollider2D hitboxCollider;
    public float delay = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        hitboxCollider = transform.Find("Hitboxes/BasicAttack1").gameObject.GetComponent<CircleCollider2D>();

        Debug.Log("Hitbox collider: " + hitboxCollider);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public void Attack()
    {
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        hitboxCollider.enabled = true;
        yield return new WaitForSeconds(delay);
        hitboxCollider.enabled = false;

    }
}
