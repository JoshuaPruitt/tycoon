using System;
using Sandbox;

public sealed class GridPlacer : Component
{
	[Property] public float CellSize { get; set; } = 50f;
	[Property] public float PlaceDistance { get; set; } = 400f;
	[Property] public GameObject SelectedPrefab { get; set; }
	[Property] public bool NoCost { get; set; } = false;

	private Wallet _wallet;
	private PlayerController _controller;


	protected override void OnStart()
	{
		_wallet = GetComponent<Wallet>();
		_controller = GetComponent<PlayerController>();
	}

	protected override void OnUpdate()
	{
		//Ray casted from camera through cursor
		var ray = _controller.EyeTransform.ForwardRay;

		var tr = Scene.Trace.Ray( in ray, PlaceDistance )
			.IgnoreGameObjectHierarchy( GameObject ) // Do not hit player
			.Run();

		//if nothing hit, or hit wall
		if ( !tr.Hit || tr.Normal.z < 0.7f )
		{
			return;
		}

		var snapped = SnapToCell( tr.EndPosition );
		DebugOverlay.Sphere( new Sphere( snapped, 8f ), Color.Green );

		if ( Input.Pressed( "attack2" ) )
		{
			TryPlace( snapped );
		}
	}

	private void TryPlace( Vector3 position )
	{
		//Ensure a prefab is selected
		if ( SelectedPrefab is null )
		{
			Log.Warning( "GridPlacer has no selected Prefab!" );
			return;
		}

		//Ensure it is a placeable item
		var placeable = SelectedPrefab.GetComponent<Placeable>();
		if ( placeable is null )
		{
			Log.Warning( $"{SelectedPrefab.Name} has no Placeable component! " );
			return;
		}

		//Ensure can afford
		if ( !NoCost && !_wallet.TrySpend( placeable.Cost ) )
		{
			return; // Cannot afford
		}

		var placed = SelectedPrefab.Clone( position );

		// Placed collectors need to know which wallet to place money into
		var collector = placed.GetComponent<Collector>();
		if ( collector is not null )
		{
			collector.Wallet = _wallet;
		}
	}

	//snap placeable objects to a cell
	private Vector3 SnapToCell( Vector3 pos )
	{
		float x = MathF.Floor( pos.x / CellSize ) * CellSize + CellSize / 2;
		float y = MathF.Floor( pos.y / CellSize ) * CellSize + CellSize / 2;

		return new Vector3( x, y, pos.z );
	}
}
