using System;
using System.Collections.Generic;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Extensions;
using Stride.Graphics;
using Stride.Graphics.GeometricPrimitives;
using Stride.Rendering;
using Stride.Rendering.Materials;
using Stride.Rendering.Materials.ComputeColors;

namespace Demo
{
    /// <summary>
    /// Loads every texture from the StridePlaceholderTextures pack and lays them out on a grid of
    /// textured quads that slowly rotate around their Y axis.
    /// </summary>
    public class TextureGridShowcase : SyncScript
    {
        // Number of quads per row.
        public int Columns = 5;

        // World-space distance between quad centers (both axes).
        public float Spacing = 1.4f;

        // Sway speed around Y, in radians per second.
        public float RotationSpeed = 0.8f;

        // Max sway angle around the facing direction, in radians (~28 deg) — the quads
        // gently rock back and forth so the texture always stays readable.
        public float SwayAmplitude = 0.5f;

        private float _time;

        // =====================================================================================
        // Discovered texture Content URLs (STEP 1).
        //
        // The library package "StridePlaceholderTextures.sdpkg" declares its asset-folder root as
        // "Assets" (AssetFolders: !dir Assets). Every .sdtex lives directly under that Assets/
        // folder, so each texture's Content URL is simply its file name WITHOUT extension.
        //
        // Adjust this array if you add / remove / rename textures in the pack.
        // =====================================================================================
        private static readonly string[] TextureUrls =
        {
            "Black",
            "Checker",
            "Error",
            "Grey",
            "Grid",
            "Missing",
            "Normal",
            "Undefined",
            "UVGrid",
            "White",
        };

        private readonly List<Entity> quads = new List<Entity>();

        public override void Start()
        {
            int cols = Math.Max(1, Columns);
            int total = TextureUrls.Length;
            int rows = (total + cols - 1) / cols;

            int index = 0;
            foreach (var url in TextureUrls)
            {
                Texture tex;
                try
                {
                    tex = Content.Load<Texture>(url);
                }
                catch (Exception e)
                {
                    Log.Warning($"TextureGridShowcase: could not load texture '{url}': {e.Message}");
                    continue;
                }

                // Unlit-ish diffuse material that shows the texture directly.
                var material = Material.New(GraphicsDevice, new MaterialDescriptor
                {
                    Attributes =
                    {
                        Diffuse = new MaterialDiffuseMapFeature(new ComputeTextureColor { Texture = tex }),
                        DiffuseModel = new MaterialDiffuseLambertModelFeature(),
                    }
                });

                // Quad = a unit plane primitive (lies in the XY plane, normal +Z by default).
                var primitive = GeometricPrimitive.Plane.New(GraphicsDevice);
                var model = new Model
                {
                    material,
                    new Mesh { Draw = primitive.ToMeshDraw() }
                };

                int col = index % cols;
                int row = index / cols;

                // Center the grid on the parent entity's origin.
                float x = (col - (cols - 1) * 0.5f) * Spacing;
                float y = ((rows - 1) * 0.5f - row) * Spacing;

                var quad = new Entity($"Quad_{url}")
                {
                    new ModelComponent { Model = model }
                };
                quad.Transform.Position = new Vector3(x, y, 0f);

                Entity.AddChild(quad);
                quads.Add(quad);
                index++;
            }

            if (quads.Count == 0)
            {
                Log.Warning("TextureGridShowcase: no textures were loaded. " +
                            "Make sure the StridePlaceholderTextures package is referenced and compiled.");
            }
        }

        public override void Update()
        {
            _time += (float)Game.UpdateTime.Elapsed.TotalSeconds;
            var rot = Quaternion.RotationY(SwayAmplitude * MathF.Sin(_time * RotationSpeed));
            foreach (var quad in quads)
            {
                quad.Transform.Rotation = rot;
            }
        }
    }
}
