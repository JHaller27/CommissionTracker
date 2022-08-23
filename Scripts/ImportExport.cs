using System;
using CommissionTracker;
using Godot;

public class ImportExport : Control
{
	private TextEdit TextArea => this.GetNode<TextEdit>("%TextEdit");
	private Label StatusLabel => this.GetNode<Label>("%StatusLabel");
	private SceneTreeTween Tween { get; set; }

	public override void _Ready()
	{
		this.Export();
	}

	private void Copy() => OS.Clipboard = this.TextArea.Text;
	private void Paste() => this.TextArea.Text = OS.Clipboard;
	private void Export() => DoWithStatus(() => this.TextArea.Text = SaveUtils.ExportAll(), "Exporting...", "Export data updated");
	private void Import() => DoWithStatus(() => SaveUtils.ImportAll(this.TextArea.Text), "Importing...", "Import complete");

	private void DoWithStatus(Action action, string beforeStatus, string afterStatus)
	{
		this.Tween?.Kill();
		this.Tween = CreateTween();

		this.StatusLabel.Modulate = Colors.White;
		this.StatusLabel.Text = beforeStatus;

		action();

		this.StatusLabel.Text = afterStatus;

		this.Tween.TweenProperty(this.StatusLabel, "modulate:a", 0f, 3);
		this.Tween.TweenCallback(this, nameof(ClearLabel));
	}

	private void ClearLabel() => this.StatusLabel.Text = string.Empty;
}
