using Godot;

public partial class RowNumberLabel : Label
{
	public void SetRowNumber(int rowNumber)
	{
		Text = rowNumber.ToString();
	}
}
