using Sandbox;

public sealed class Collector : Component, Component.ITriggerListener
{
	[Property] public Wallet Wallet { get; set; }

	public void OnTriggerEnter( Collider other )
	{
		if ( other.GameObject.Tags.Has( "block" ) ) //Check that object is of tag block
		{
			var blockValue = other.GameObject.GetComponent<BlockValue>(); // Get blocks value
			if ( blockValue is null )
			{
				other.GameObject.Destroy();
				Log.Warning( $"{other.GameObject.Name} has no BlockValue!!" );
				return; // If value is null then return
			}

			Wallet.Add( blockValue.Value ); // Add to wallet
			other.GameObject.Destroy(); // Destroy block
		}
	}
}