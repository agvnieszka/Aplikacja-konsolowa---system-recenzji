using SystemRecenzji.UI;

ScreenViewer viewer = new();

viewer.AddToStack(new ScreenStart(viewer));

viewer.RunUntilDone();