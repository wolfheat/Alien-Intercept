public class HealthPickup : Pickup
{
	public override void CollideWithPlayer()
	{
		SoundController.Instance.PlaySFX(SFX.StarPickup);
		PlayerStats.Instance.AddHealth(value);
		base.CollideWithPlayer();
	}
}