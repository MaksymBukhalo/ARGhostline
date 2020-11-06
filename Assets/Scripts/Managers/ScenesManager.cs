using UnityEngine.SceneManagement;

namespace FoodStoryTAS
{
	public static class ScenesManager
	{
		/// <summary>
		/// Load first scene with Splash.
		/// </summary>
		public static void LoadSplashScene()
		{
			SceneManager.LoadScene(0);
		}

		/// <summary>
		/// Load second scene with interaction(AR Foundation)
		/// </summary>
		public static void LoadFoodOverviewScene()
		{
			SceneManager.LoadScene(1);
		}
	}
}
