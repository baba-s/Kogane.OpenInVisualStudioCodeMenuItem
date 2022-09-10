using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Kogane.Internal
{
    internal static class OpenInVisualStudioCodeMenuItem
    {
        private const string ITEM_NAME = "Assets/Kogane/Open in Visual Studio Code";

        [MenuItem( ITEM_NAME, true )]
        private static bool CanOpen()
        {
            var assetGUIDs = Selection.assetGUIDs;
            return assetGUIDs is { Length: > 0 };
        }

        [MenuItem( ITEM_NAME, false, 1552434739 )]
        private static void Open()
        {
            var assetGUIDs = Selection.assetGUIDs;

            if ( assetGUIDs is not { Length: > 0 } ) return;

            var assetPaths = assetGUIDs
                    .Select( x => AssetDatabase.GUIDToAssetPath( x ) )
                    .OrderBy( x => x )
                ;

            foreach ( var assetPath in assetPaths )
            {
                var fullPath = Path.GetFullPath( assetPath );

                var startInfo = new ProcessStartInfo( "code", $@"-r ""{fullPath}""" )
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                };

                try
                {
                    Process.Start( startInfo );
                }
                catch ( Win32Exception )
                {
                    Debug.LogError( "Mac でこのコマンドを使用する場合は Visual Studio Code のコマンドパレットで `Shell Command: Install code command in PATH` を実行しておく必要があります" );
                }
            }
        }
    }
}