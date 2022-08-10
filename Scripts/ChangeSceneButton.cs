using Godot;

public class ChangeSceneButton : Button
{
	[Export] public string ChangeToScene { get; set; }

	public override void _Ready()
	{
		this.Connect("pressed", this, nameof(ChangeScene));
	}

	protected virtual void ChangeScene()
	{
		this.GetTree().ChangeScene(this.ChangeToScene);
	}
}
