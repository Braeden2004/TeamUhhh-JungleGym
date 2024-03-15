using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelRotationHandler : MonoBehaviour
{
    PlayerController player;
    Roll roll;
    [SerializeField] GameObject playerObj;
    [SerializeField] float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        roll = GetComponent<Roll>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (roll.isRolling && roll.OnSlope())
        {
            Quaternion desiredRot = Quaternion.LookRotation(roll.slopeDir);
            Quaternion additionalRot = Quaternion.Euler(0, 90f, 0); //Take this out when player model is fixed
            Quaternion rot = desiredRot * additionalRot; //Take this out when player model is fixed
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }
        else if (player.moveDir != Vector3.zero)
        {
            Quaternion desiredRot = Quaternion.LookRotation(player.moveDir);
            Quaternion additionalRot = Quaternion.Euler(0, 90f, 0); //Take this out when player model is fixed
            Quaternion rot = desiredRot * additionalRot; //Take this out when player model is fixed
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }
    }
}
