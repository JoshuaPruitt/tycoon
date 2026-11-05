using Sandbox;

public sealed class BlockLifetime : Component
{
	[Property] public float Lifetime { get; set; } = 30f;

	private TimeSince _age;
	protected override void OnStart()
	{
		_age = 0;
	}

	protected override void OnUpdate()
	{
		if ( _age > Lifetime )
		{
			GameObject.Destroy();
		}
	}
}