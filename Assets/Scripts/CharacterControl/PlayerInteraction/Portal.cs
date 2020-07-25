using UserInterface;

namespace CharacterControl
{
    public class Portal : InteractionMovement
    {

        protected override void PlayerTriggered()
        {
            UIManager.Instance.LoadWinWindow();
        }
    }

}

