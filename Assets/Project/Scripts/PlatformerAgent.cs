using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using Gamekit2D;

public class PlatformerAgent : Agent
{
    public GameObject m_Agent;
    public GameObject m_Goal;
    private PlayerCharacter playerCharacter;
    private Vector2 initialPosition;

    public override void InitializeAgent()
    {
        playerCharacter = m_Agent.GetComponent<PlayerCharacter>();
        initialPosition = m_Agent.transform.position;
    }

    public override void CollectObservations()
    {
        base.CollectObservations();
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        base.AgentAction(vectorAction, textAction);
    }

    public override void AgentReset()
    {
        base.AgentReset();
    }

    public override void AgentOnDone()
    {
        base.AgentOnDone();
    }

    public bool IsGrounded(Vector2 position)
    {
        Vector2 adjustedPosition = new Vector2(position.x, position.y + 0.5f);
        Vector3 direction = -Vector3.up;
        LayerMask layerMask = 1 << 31;
        RaycastHit2D raycast = Physics2D.Raycast(position, direction, 1, layerMask);
        return raycast;
    }

    public bool IsHitWall(Vector2 position)
    {
        Vector2 centerPosition = new Vector2(position.x, position.y + 0.5f);
        Vector3 direction = Vector3.forward;
        LayerMask layerMask = 1 << 31;
        RaycastHit2D raycast = Physics2D.Raycast(position, direction, 1, layerMask);

        if (raycast && playerCharacter.CheckForGrounded())
        {
            return true;
        }
        return false;
    }
}
