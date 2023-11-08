using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerData))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private InputAction mouseClickAction; /// Click(+), Add Binding > Path: (Mouse: Left Button)
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private int moveValue = 6; /// moveValue means comparing "move" value per 1 activityPoint. In this case it is 6:1
    private Coroutine moveCoroutine;

    private void Start()
    {
        playerData = GetComponent<PlayerData>();
        agent = GetComponent<NavMeshAgent>();
        mouseClickAction.performed += ClickToMove;
    }

    /// Using OnEnable() and OnDisable() to enable and disable Input Action
    /// to increase the efficiency of receiving input from the mouse
    /// by reducing work instead of using the Update function.
    private void OnEnable()
    {
        mouseClickAction.Enable();
    }

    private void OnDisable()
    {
        mouseClickAction.Disable();
    }

    private void ClickToMove(InputAction.CallbackContext context)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        /// Main Camera detect where the mouse's pressed.
        Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(movePosition, out var hitInfo))
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(agent.transform.position, hitInfo.point, NavMesh.AllAreas, path);

            /// DISTANCE INFO
            /// Convert pathDistance(float) to pathDistanceInt(int)
            /// Debug path distance in console
            float pathDistance = GetPathDistance(path);
            int pathDistanceInt = Mathf.RoundToInt(pathDistance);
            Debug.Log($"Path Distance: {pathDistanceInt}/{moveValue} = {pathDistanceInt / moveValue}");

            /// CHECK IF PLAYER ARE IN BATTLE MODE OR NOT.
            /// If not(false), feel free to move!
            /// If yes(true), movement will reduce ActivityPoint(AP)
            if (!playerData.IsBattle)
            {
                ClickEffect(hitInfo.point + new Vector3(0, 0.1f, 0), 0, false);

                /// Change this delay time as needed
                yield return new WaitForSeconds(1.0f);
            }
            else if (playerData.IsBattle)
            {
                /// CHECK IF AP(activityPoint) IS ENOUGH?
                /// NOT ENOUGH! because (distance/6 > AP)
                if (playerData.ActivityPoint == 0 || (GetPathDistance(path) / moveValue) > playerData.ActivityPoint)
                {
                    Debug.Log("Not enough AP, Please choose a closer position.");
                }
                /// ENOUGH! because (distance/6 <= AP)
                else
                {
                    /// Calculate rounded number of distance by pathDistanceInt / moveValue in double, then convert to int
                    double roundedDistance = Math.Ceiling((double)pathDistanceInt / moveValue);
                    int numberRoundedDistance = Convert.ToInt32(roundedDistance);

                    /// MANAGE AP (activityPoint) & DEBUG
                    playerData.SetActivityPoint(-numberRoundedDistance);
                    Debug.Log($"-{numberRoundedDistance} AP");

                    /// Particle and Floating text effect
                    ClickEffect(hitInfo.point + new Vector3(0, 0.1f, 0), numberRoundedDistance, true);

                    // Change this delay time as needed
                    yield return new WaitForSeconds(1.0f);
                }
            }
        }
    }

    /// Calculate the total distance of the path
    private float GetPathDistance(NavMeshPath path)
    {
        /// Set default totalDistance value to be calculated is 0
        ///
        /// Then check how many corners the path has.
        /// 1  >>> The function will returns a totalDistance value with out calculating.
        /// 2 + >>> The function will loops through all point of corners-1. and calculate the distance between the connected point of corners.
        ///
        /// Retyrb totalDistance when finish the loop*/
        float totalDistance = 0f;

        if (path.corners.Length < 2)
            return totalDistance;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            totalDistance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return totalDistance;
    }

    private void ClickEffect(Vector3 pointPosition, int numberRoundedDistance, bool isBattle)
    {
        // Particle click effect
        GameObject clickEffect = ParticleManager.Instance.data.clickEffectPrefab;
        Instantiate(clickEffect, pointPosition + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);

        if (isBattle)
        {
            // Floating text effect
            GameObject go = Instantiate(floatingTextPrefab, pointPosition, floatingTextPrefab.transform.rotation);
            go.GetComponent<TMP_Text>().text = $"-{numberRoundedDistance} AP";
        }

        // Set destination (NavMesh AI)
        agent.SetDestination(pointPosition);
    }
}