using Godot;

public class JobItem : VBoxContainer
{
	[Signal]
	public delegate void DeleteMe();

	public override void _Ready()
	{

	}

	private void _on_DeleteButton_pressed()
	{
		EmitSignal(nameof(DeleteMe));
	}
}
