using Godot;

public class JobsContainer : VBoxContainer
{
	private static PackedScene _jobItemScene = ResourceLoader.Load<PackedScene>("res://JobItem.tscn");

	public void AddJobItem()
	{
		Node jobItemNode = _jobItemScene.Instance();
		this.AddChild(jobItemNode);
	}

	public override void _Ready()
	{
		foreach (Node child in this.GetChildren())
		{
			this.RemoveChild(child);
		}
	}
}
