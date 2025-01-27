using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Body;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private float m_ImpulseJump;

    private bool m_IsJump = false;

    private static readonly int s_JumpGroundId = Animator.StringToHash("jump_ground");
    private static readonly int s_JumpId = Animator.StringToHash("jump");


    private void Update()
    {
        OnKeyDown();
        UpdatePhysics();
    }

    private void OnKeyDown()
    {
        if (!m_IsJump && Input.GetKeyDown(KeyCode.Space))
        {
            m_Animator.SetBool(s_JumpGroundId, true);
        }
    }

    private void UpdatePhysics()
    {
        if (m_Body.linearVelocityY != 0 || m_IsJump)
        {
            m_IsJump = true;
            float velocity = Mathf.Sign(m_Body.linearVelocityY);
            m_Animator.SetFloat(s_JumpId, velocity);
            if (m_Body.linearVelocityY == 0)
            {
                m_Animator.SetBool(s_JumpGroundId, false);
                m_IsJump = false;
            }
        }
    }

    public void Jump()
    {
        m_Body.AddForce(new Vector2(0, m_ImpulseJump));
    }
}
