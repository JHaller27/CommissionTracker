using CommissionTracker;
using Godot;

public class ImportExport : Control
{
	private TextEdit TextArea => this.GetNode<TextEdit>("%TextEdit");

	public override void _Ready()
	{
		this.Refresh();
	}

	private void Copy() => OS.Clipboard = this.TextArea.Text;
	private void Paste() => this.TextArea.Text = OS.Clipboard;
	private void Refresh() => this.TextArea.Text = SaveUtils.ExportAll();
	private void Import() => SaveUtils.ImportAll(this.TextArea.Text);
}
