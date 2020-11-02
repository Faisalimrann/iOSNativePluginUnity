#if UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UnitySwift
{
    public static class PostProcessor
    {

        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
        {
            if (buildTarget == BuildTarget.iOS)
            {

                ChangeXcodePlist(buildPath);

                // So PBXProject.GetPBXProjectPath returns wrong path, we need to construct path by ourselves instead
                // var projPath = PBXProject.GetPBXProjectPath(buildPath);
                var projPath = buildPath + "/Unity-iPhone.xcodeproj/project.pbxproj";
                var proj = new PBXProject();
                proj.ReadFromFile(projPath);

                var targetGuid = proj.GetUnityMainTargetGuid();

                UnityEngine.Debug.Log("targetGuid: " + targetGuid);

                //// Configure build settings
                proj.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");

                proj.SetBuildProperty(targetGuid, "SWIFT_OBJC_BRIDGING_HEADER", "Libraries/SwiftPlugin/SwiftPlugin-Bridging-Header.h");
                proj.SetBuildProperty(targetGuid, "SWIFT_OBJC_INTERFACE_HEADER_NAME", "SwiftPlugin-Swift.h");
                proj.AddBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");

                proj.SetBuildProperty(targetGuid, "SWIFT_VERSION", "5.0");






                proj.WriteToFile(projPath);

                ProjectCapabilityManager projCapability = new ProjectCapabilityManager(projPath, "Unity-iPhone/mmk.entitlements", "Unity-iPhone");

                //projCapability.AddHealthKit();
                projCapability.WriteToFile();
            }
        }

        public static void ChangeXcodePlist(string path)
        {
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            PlistElementDict rootDict = plist.root;

            UnityEngine.Debug.Log(">> Automation, plist ... <<");

            // example of changing a value:
            // rootDict.SetString("CFBundleVersion", "6.6.6");

            // example of adding a boolean key...
            // < key > ITSAppUsesNonExemptEncryption </ key > < false />
            rootDict.SetString("NSHealthShareUsageDescription", "Recording of fitness data!!");

            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}
#endif