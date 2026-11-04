using System;
using Sandbox;

public sealed class Dropper : Component
{
    [Property] public GameObject BlockPrefab { get; set; }
    [Property] public GameObject SpawnPoint { get; set; }
    [Property] public float Interval { get; set; } = 2f;

    TimeSince _lastDrop;

    protected override void OnFixedUpdate()
    {
        if ( BlockPrefab is null || SpawnPoint is null ) return;

        if ( _lastDrop > Interval )
        {
            BlockPrefab.Clone( SpawnPoint.WorldPosition );
            _lastDrop = 0;
        }
    }
}