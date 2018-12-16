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
        AddVectorObs(IsGrounded(m_Agent.transform.position) ? 1 : 0);
        AddVectorObs(IsHitWall(m_Agent.transform.position) ? 1 : 0);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (IsGrounded(m_Agent.transform.position))
        {
            playerCharacter.SetHorizontalMovement(5);
        }

        int action = Mathf.FloorToInt(vectorAction[0]);
        if (action == 0 && IsGrounded(m_Agent.transform.position))
        {
            SetReward(1);
        }
        else if (action == 1 && IsGrounded(m_Agent.transform.position))
        {
            playerCharacter.SetMoveVector(new Vector2(15, 15));
            if (IsHitWall(m_Agent.transform.position))
            {
                AddReward(1);
            }
            else
            {
                AddReward(-5);
            }
        }

        if (m_Goal.GetComponent<CheckpointChecker>().enteredCheckpoint)
        {
            Done();
        }
    }

    public override void AgentReset()
    {
        m_Agent.transform.position = initialPosition;
    }

    public override void AgentOnDone()
    {

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
