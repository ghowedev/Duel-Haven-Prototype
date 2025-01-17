using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacks : MonoBehaviour
{
    public Text debugText;
    public Animator animator;
    private PlayerMovement playerMovement;
    [SerializeField] private Transform pfFireball;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.05f;
    [SerializeField] private float dashCooldown = 0.2f;
    private float dashCooldownTimestamp = 0f;
    [SerializeField] private float dashEndLag = 0.2f;
    public bool abilitiesEnabled = true;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = ((Vector2)mousePosition - playerMovement.rb.position).normalized;

        debugText.text = $"Mouse Position: {mousePosition}, Direction: {direction}";


        CheckKeyBinds();
    }
    void FixedUpdate()
    {

    }

    void CheckKeyBinds()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > dashCooldownTimestamp)
        {
            StartCoroutine(CoroutineBasicAttack1());
        }
    }

    void HandleAttackingAnimation(Vector2 direction)
    {
        float attackAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        string attackDirection = HelperUtilities.GetNearestDirectionFromAngle(attackAngle);

        animator.SetBool("Attacking", true);
        animator.SetBool("Moving", false);
        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetBool("Up Left", false);
        animator.SetBool("Up Right", false);
        animator.SetBool("Down Left", false);
        animator.SetBool("Down Right", false);
        animator.SetBool(attackDirection, true);
    }

    IEnumerator CoroutineBasicAttack1()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = ((Vector2)mousePosition - playerMovement.rb.position).normalized;

        playerMovement.movementEnabled = false;

        float startTime = Time.time;
        float elapsedTime = 0f;
        dashCooldownTimestamp = Time.time + dashCooldown;

        HandleAttackingAnimation(direction);

        while (elapsedTime < dashDuration)
        {
            playerMovement.MovePlayer(direction, dashSpeed);
            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        playerMovement.rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashEndLag);
        playerMovement.movementEnabled = true;
    }

    void CoroutineFireball()
    {
        // Calculate the direction from the player to the mouse cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 fireballDirection = (mousePosition - firePoint.position).normalized;

        // Calculate the rotation to make the fireball face the desired direction
        float angle = Mathf.Atan2(fireballDirection.y, fireballDirection.x) * Mathf.Rad2Deg;
        Quaternion fireballRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        // Instantiate the fireball
        Transform fireball = Instantiate(pfFireball, firePoint.position, fireballRotation);
    }


}