using System.Collections.Generic;
using UnityEditor;

namespace FoodStoryTAS
{
    public static class Extensions 
    {
#if UNITY_EDITOR
		#region Editor
		public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            List<T> assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null)
                {
                    assets.Add(asset);
                }
            }
            return assets;
        }

        public static T FindAssetByType<T>() where T : UnityEngine.Object
        {
            T asset = null;
            var guids= AssetDatabase.FindAssets (string.Format("t:{0}", typeof(T)));
            string firstGuid = string.Empty;

            if (guids.Length > 0)
            {
                firstGuid = guids[0];

                string assetPath = AssetDatabase.GUIDToAssetPath(firstGuid);
                asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }

            return asset;
        }
		#endregion
#endif
		#region Enum

		/// <summary>
		/// Get next element of enum.
		/// </summary>
		public static T Next<T>(this T src) where T : struct
		{
			if (!typeof(T).IsEnum) throw new System.ArgumentException(System.String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));

			T[] Arr = (T[])System.Enum.GetValues(src.GetType());
			int j = System.Array.IndexOf<T>(Arr, src) + 1;
			return (Arr.Length == j) ? Arr[0] : Arr[j];
		}

		#endregion

		#region Converter
		public static string ConvertCurrency(Currencies currency)
		{
			string convertedValue = string.Empty;

			switch (currency)
			{
				case Currencies.EUR:
					convertedValue = "€";
					break;
				case Currencies.USD:
					convertedValue = "$";
					break;
			}

			return convertedValue;
		}
		#endregion
	}
}