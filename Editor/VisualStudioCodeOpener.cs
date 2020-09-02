using System.Diagnostics;
using System.IO;
using UnityEditor;

namespace Kogane.Internal
{
	internal static class VisualStudioCodeOpener
	{
		private const string ITEM_NAME = "Assets/Open in Visual Studio Code";

		[MenuItem( ITEM_NAME )]
		private static void Open()
		{
			var asset     = Selection.activeObject;
			var assetPath = AssetDatabase.GetAssetPath( asset );
			var fullPath  = Path.GetFullPath( assetPath );

			var startInfo = new ProcessStartInfo( "code", $"-r {fullPath}" )
			{
				WindowStyle = ProcessWindowStyle.Hidden,
			};

			Process.Start( startInfo );
		}

		[MenuItem( ITEM_NAME, true )]
		private static bool CanOpen()
		{
			var asset = Selection.activeObject;
			var path  = AssetDatabase.GetAssetPath( asset );

			if ( string.IsNullOrWhiteSpace( path ) ) return false;
			if ( AssetDatabase.IsValidFolder( path ) ) return false;

			return true;
		}
	}
}