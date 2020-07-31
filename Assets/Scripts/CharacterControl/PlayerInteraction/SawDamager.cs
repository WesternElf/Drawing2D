
using UserInterface;

namespace  CharacterControl
{
    public class SawDamager : InteractionMovement
    {
        protected override void PlayerTriggered()
        {
            UIManager.Instance.LoadLoseWindow();
        }
    }
}

