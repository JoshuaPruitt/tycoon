using Sandbox;

public sealed class Conveyor : Component, Component.ExecuteInEditor
{
	[Property] public float Speed { get; set; } = 100f;
	[Property] public float Length { get; set; } = 32;
	[Property] public GameObject Visual { get; set; }
	[Property] public GameObject Arrow { get; set; }

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

	protected override void OnUpdate()
	{
		UpdateSize(); // Update object size before continuing
	}

	public void UpdateSize()
	{
		var collider = GetComponent<BoxCollider>();
		var renderer = Visual.GetComponent<ModelRenderer>();


		if ( collider is null || renderer is null )
		{
			Log.Warning( "No collider or Mesh present on Conveyor!!" );
			return;
		}

		if ( Arrow is not null )
		{
			var arrowLPos = Arrow.LocalPosition;
			arrowLPos.x = (Length / 2) + 8;
			Arrow.LocalPosition = arrowLPos;
		}

		var size = collider.Scale;
		var center = collider.Center;
		size.x = Length;
		collider.Scale = size;

		var modelSize = renderer.Model.Bounds.Size;
		Visual.LocalScale = new Vector3( size.x / modelSize.x, size.y / modelSize.y, size.z / modelSize.z );

		center.x = Length / 2; // Update center position of conveyor
		collider.Center = center;

		var visualPos = Visual.LocalPosition; // Update model to reflect current position of collider
		visualPos.x = Length / 2;
		Visual.LocalPosition = visualPos;

	}
}