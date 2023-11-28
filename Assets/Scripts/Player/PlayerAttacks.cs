using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private Transform pfFireball;
    [SerializeField] private Transform firePoint;
    
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); 
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeyBinds();
    }
    
    void CheckKeyBinds()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastSwordSwing();
        }
    }
    
    void CastFireball()
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
    
    void CastSwordSwing()
    {
        playerMovement.movementEnabled = false;
    }
}