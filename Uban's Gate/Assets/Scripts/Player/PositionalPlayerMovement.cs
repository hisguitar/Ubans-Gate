using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerData))]
[RequireComponent(typeof(NavMeshAgent))]
public class PositionalPlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private InputAction mouseClickAction; // Click(+), Add Binding > Path: (Mouse: Left Button)
    private Coroutine moveCoroutine;

    private void Start()
    {
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
        // Main Camera detect where the mouse's pressed.
        Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(movePosition, out var hitInfo))
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(agent.transform.position, hitInfo.point, NavMesh.AllAreas, path);

            ClickEffect(hitInfo.point + new Vector3(0, 0.1f, 0));

            // Change this delay time as needed
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void ClickEffect(Vector3 pointPosition)
    {
        // Particle click effect
        GameObject clickEffect = ParticleManager.Instance.data.clickEffectPrefab;
        Instantiate(clickEffect, pointPosition + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);

        // Set destination (NavMesh AI)
        agent.SetDestination(pointPosition);
    }
}