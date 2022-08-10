using System.Collections.Generic;
using System.Linq;
using CommissionTracker;
using CommissionTracker.SaveModels;
using Godot;

public class JobsContainer : VBoxContainer
{
	private static PackedScene _jobItemScene = ResourceLoader.Load<PackedScene>("res://Scenes/JobItem.tscn");

	[Signal]
	public delegate void Changed();

	public override void _Ready()
	{
		foreach (Node child in this.GetChildren())
		{
			this.RemoveChild(child);
		}
	}

	public JobItem AddJobItem()
	{
		JobItem jobItemNode = _jobItemScene.Instance<JobItem>();
		this.AddChild(jobItemNode);
		jobItemNode.Connect("DeleteMe", this, nameof(DeleteJob), new(jobItemNode));
		jobItemNode.Connect("Changed", this, nameof(ChangeDetected));

		EmitSignal(nameof(Changed));

		return jobItemNode;
	}

	public void DeleteJob(JobItem jobNode)
	{
		this.RemoveChild(jobNode);
		EmitSignal(nameof(Changed));
	}

	public IEnumerable<decimal> GetJobValuesOfType(JobType jobType)
	{
		return this.GetChildren().Cast<JobItem>()
			.Where(j => j.JobType == jobType)
			.Select(j => j.Value);
	}

	public List<JobItemModel> Export()
	{
		return this.GetChildren().Cast<JobItem>()
			.Select(j => j.Export())
			.ToList();
	}

	public void Import(List<JobItemModel> jobs)
	{
		foreach (Node child in this.GetChildren())
		{
			this.RemoveChild(child);
		}

		foreach (JobItemModel model in jobs)
		{
			JobItem jobItem = this.AddJobItem();
			jobItem.Import(model);
		}
	}

	private void ChangeDetected() => EmitSignal(nameof(Changed));
}
