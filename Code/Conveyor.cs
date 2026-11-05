using Sandbox;

public sealed class Conveyor : Component
{
	[Property] public float Speed { get; set; } = 100f;

	protected override void OnStart()
	{
		var collider = GetComponent<BoxCollider>();
		if ( collider is null )
		{
			Log.Warning( "Conveyor has no box collider!! " );
			return;
		}
		collider.SurfaceVelocity = Vector3.Forward * Speed;
	}
}