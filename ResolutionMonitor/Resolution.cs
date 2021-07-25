namespace ResolutionMonitor
{
	public class Resolution
	{
		public int width { get; private set; }
		public int Height { get; private set; }

		public Resolution(int width, int Height)
		{
			this.width = width;
			this.Height = Height;
		}
	}
}
