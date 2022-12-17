using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Actor))]
public class MovementController : MonoBehaviour, IMoveable
{
    #region IMOVEABLE_PROPERTIES


    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSpeed => GetComponent<Actor>().ActorStats.RotationSpeed;

    #endregion

    #region IMOVEABLE_METHODS
    public void Travel(Vector3 direction) => transform.position = transform.position + (direction * Time.deltaTime * Speed);
    public void Rotate(Vector3 direction) => transform.Rotate(direction * Time.deltaTime * RotationSpeed);


 
    #endregion
}
