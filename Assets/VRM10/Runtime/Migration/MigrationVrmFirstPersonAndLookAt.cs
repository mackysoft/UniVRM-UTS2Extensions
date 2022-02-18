using System;
using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UniGLTF.Extensions.VRMC_vrm;
using UniJSON;
using UnityEngine;

namespace UniVRM10
{
    public static class MigrationVrmLookAtAndFirstPerson
    {
        static LookAtRangeMap MigrateLookAtRangeMap(JsonNode vrm0)
        {
            // VRM1
            // curve は廃止されます
            return new LookAtRangeMap
            {
                InputMaxValue = vrm0["xRange"].GetSingle(),
                OutputScale = vrm0["yRange"].GetSingle(),
            };
        }

        static LookAtType MigrateLookAtType(JsonNode vrm0)
        {
            switch (vrm0.GetString().ToLower())
            {
                case "bone": return LookAtType.bone;
                case "blendshape": return LookAtType.expression;

            }
            throw new NotImplementedException();
        }

        static FirstPersonType MigrateFirstPersonType(JsonNode vrm0)
        {
            return (FirstPersonType)Enum.Parse(typeof(FirstPersonType), vrm0.GetString(), true);
        }

        public static (LookAt, FirstPerson) Migrate(glTF gltf, JsonNode vrm0)
        {
            // VRM1
            // firstPerson に同居していた LookAt は独立します
            LookAt lookAt = default;
            lookAt = new LookAt
            {
                RangeMapHorizontalInner = MigrateLookAtRangeMap(vrm0["lookAtHorizontalInner"]),
                RangeMapHorizontalOuter = MigrateLookAtRangeMap(vrm0["lookAtHorizontalOuter"]),
                RangeMapVerticalDown = MigrateLookAtRangeMap(vrm0["lookAtVerticalDown"]),
                RangeMapVerticalUp = MigrateLookAtRangeMap(vrm0["lookAtVerticalUp"]),
                Type = MigrateLookAtType(vrm0["lookAtTypeName"]),
                OffsetFromHeadBone = MigrateVector3.Migrate(vrm0, "firstPersonBoneOffset"),
            };

            var firstPerson = new FirstPerson
            {
                // VRM1
                // firstPersonBoneOffset は廃止されます。LookAt.OffsetFromHeadBone を使ってください。
                // firstPersonBone は廃止されます。Head 固定です。
                MeshAnnotations = new System.Collections.Generic.List<MeshAnnotation>(),
            };
            if (vrm0.TryGet("meshAnnotations", out JsonNode meshAnnotations))
            {
                Func<int, int> meshIndexToRenderNodeIndex = meshIndex =>
                {
                    for (int i = 0; i < gltf.nodes.Count; ++i)
                    {
                        var node = gltf.nodes[i];
                        if (node.mesh == meshIndex)
                        {
                            return i;
                        }
                    }
                    throw new NotImplementedException("mesh is not used");
                };
                foreach (var x in meshAnnotations.ArrayItems())
                {
                    var a = new MeshAnnotation
                    {
                        Node = meshIndexToRenderNodeIndex(x["mesh"].GetInt32()),
                        Type = MigrateFirstPersonType(x["firstPersonFlag"]),
                    };
                    firstPerson.MeshAnnotations.Add(a);
                }
            };

            return (lookAt, firstPerson);
        }
    }
}
