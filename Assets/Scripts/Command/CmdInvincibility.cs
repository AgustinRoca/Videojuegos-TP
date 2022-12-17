

public class CmdInvincibility : ICommand
{
    private CharacterLifeController _controller;
    
    public CmdInvincibility(CharacterLifeController controller){
        _controller = controller;
    }
    
    public void Execute() => _controller.ActivateOrDeactivateInvincibility();
}