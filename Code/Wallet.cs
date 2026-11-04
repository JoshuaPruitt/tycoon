using Sandbox;

public sealed class Wallet : Component
{
	[Property] public int Money { get; set; } // Players money

	public bool TrySpend( int amount ) // Spend / Remove money
	{
		if ( Money >= amount ) // Check that player has enough money to spend
		{
			Money -= amount; // Remove money on spend
			return true;
		}
		return false;
	}

	public void Add( int amount ) // Add money to wallet
	{
		Money += amount;
	}
}
