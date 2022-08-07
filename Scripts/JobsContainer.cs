using System.Collections.Generic;
using System.Linq;
using CommissionTracker;
using Godot;
using Godot.Collections;

public class JobsContainer : VBoxContainer
{
	private static PackedScene _jobItemScene = ResourceLoader.Load<PackedScene>("res://Scenes/JobItem.tscn");

	public override void _Ready()
	{
		foreach (Node child in this.GetChildren())
		{
			this.RemoveChild(child);
		}
	}

	public void AddJobItem()
	{
		Node jobItemNode = _jobItemScene.Instance();
		this.AddChild(jobItemNode);
		jobItemNode.Connect("DeleteMe", this, nameof(DeleteJob), new Array(jobItemNode));
	}

	public void DeleteJob(JobItem jobNode)
	{
		this.RemoveChild(jobNode);
	}

	public IEnumerable<decimal> GetJobValuesOfType(JobType jobType)
	{
		return this.GetChildren().Cast<JobItem>()
			.Where(j => j.JobType == jobType)
			.Select(j => j.Value);
	}
}
