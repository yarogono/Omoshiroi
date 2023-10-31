using UnityEditor;
using UnityEngine;

public class MultiplayerBuildAndRun
{
    // 유니티 메뉴에 2개의 클라이언트를 실행하는 메뉴 추가
    [MenuItem("Tools/Run Multiplayer/2 Players")]
    static void PerformWin64Build2()
    {
        PerformWin64Build(2);
    }

    // 유니티 메뉴에 3개의 클라이언트를 실행하는 메뉴 추가
    [MenuItem("Tools/Run Multiplayer/3 Players")]
    static void PerformWin64Build3()
    {
        PerformWin64Build(3);
    }

    // 유니티 메뉴에 4개의 클라이언트를 실행하는 메뉴 추가
    [MenuItem("Tools/Run Multiplayer/4 Players")]
    static void PerformWin64Build4()
    {
        PerformWin64Build(4);
    }

    // Win32로 빌드
    // 실행할 클라이언트(플레이어 카운트)를 인자로 받습니다.
    static void PerformWin64Build(int playerCount)
    {
        // 유니티 빌드세팅 API를 사용
        // 빌드 타겟 설정 => 윈도우, 맥, 안드로이드, IOS 중 어떤걸로 설정할 지
        EditorUserBuildSettings.SwitchActiveBuildTarget(
            BuildTargetGroup.Standalone,
            BuildTarget.StandaloneWindows
        );

        // 실행할 클라이언트(플레이어) 갯수 만큼 반복문 실행
        for (int i = 1; i <= playerCount; i++)
        {
            // 씬의 경로를 추가
            // 프로젝트 이름과 플레이어 번호를 사용해서 빌드 경로와 파일 설정
            BuildPipeline.BuildPlayer(
                GetScenePaths(),
                "Builds/Win64/"
                    + GetProjectName()
                    + i.ToString()
                    + "/"
                    + GetProjectName()
                    + i.ToString()
                    + ".app",
                // 빌드 후 자동 실행하도록 설정
                // BuildTarget.StandaloneWindows64, BuildOptions.AutoRunPlayer);
                BuildTarget.StandaloneOSX,
                BuildOptions.AutoRunPlayer
            );
        }
    }

    // 프로젝트이름 가져오는 함수
    static string GetProjectName()
    {
        string[] s = Application.dataPath.Split('/');
        return s[s.Length - 2];
    }

    // 모든 씬(Scene)에 대한 경로 가져오는 함수
    static string[] GetScenePaths()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];

        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }

        return scenes;
    }
}
