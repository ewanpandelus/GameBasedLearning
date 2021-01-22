public interface IPlayerMovement
{
  
    float GetHorizontalVelocity();
    bool GetIsJumping();
    bool GetIsClimbing();
    float GetVerticalVelocity();
    void SetIsClimbing(bool _isClimbing);
    void SetIsJumping(bool _isJumping);
}