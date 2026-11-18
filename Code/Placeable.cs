using Sandbox;

public sealed class Placeable : Component
{
	[Property] public int Cost { get; set; } = 10;
	[Property] public int SizeInCells { get; set; } = 1;
	[Property] public string DisplayName { get; set; } = "Machine";
}
