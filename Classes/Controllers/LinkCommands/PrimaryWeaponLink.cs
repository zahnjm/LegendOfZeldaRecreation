namespace CSE3902_Game_Sprint0.Classes.Controllers.LinkCommands
{
    public class PrimaryWeaponLink : ICommand
    {
        private LinkStateMachine linkState { get; set; }

        public PrimaryWeaponLink(LinkStateMachine linkState)
        {
            this.linkState = linkState;
        }
        public void Execute()
        {
            linkState.useSword = true;
        }
    }
}
