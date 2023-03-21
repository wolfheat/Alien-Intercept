public class StarPickup : Pickup
{
	public override void CollidingWithPlayer()
	{
		SoundController.Instance.PlaySFX(SFX.StarPickup);
		PlayerStats.Instance.AddStars();
		base.CollidingWithPlayer();
	}
}