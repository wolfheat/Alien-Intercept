public class StarPickup : Pickup
{
	public override void CollideWithPlayer()
	{
		SoundController.Instance.PlaySFX(SFX.StarPickup);
		PlayerStats.Instance.AddStars(value);
		base.CollideWithPlayer();
	}
}