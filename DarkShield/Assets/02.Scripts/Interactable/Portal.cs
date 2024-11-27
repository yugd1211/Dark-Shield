public class Portal : AInteractableObeject
{
	public override void Interact(Interactor player)
	{
		StageManager.Instance.currStage.MoveNextStage();
	}
	public override bool CanInteract()
	{
		// 현재는 Stage가 구현되지 않았기 때문에 일단 무조건 상호작용 가능하게 설정
		// BattleStage, ShopStage 등 상황에 따라 다르게 설정할 수 있음
		return base.CanInteract();
	}
}
