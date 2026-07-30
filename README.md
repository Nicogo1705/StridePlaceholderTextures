# Placeholder Textures
[![Available on the Community Stride Asset Store](https://img.shields.io/badge/Community_Stride_Asset_Store-install-5b8def)](https://nicogo1705.github.io/AssetStore/a/com.nicogo.placeholder-textures)

**Fallback / debug textures for prototyping** in [Stride](https://www.stride3d.net/) — one `Texture`
asset per pattern (512×512). Drop them in whenever an asset is missing, broken, or not made yet, so
your scene never renders "nothing".

## What's in the box

| Texture | Use |
| --- | --- |
| `Missing` | Iconic magenta/black checker — "no texture assigned" |
| `Error` | Loud red — something failed to load |
| `Undefined` | Neutral grey — not decided yet |
| `Checker` | Black/white checker — scale & UV sanity |
| `UVGrid` | Grid + accent — UV layout / tiling check |
| `Normal` | Flat tangent-space normal (128,128,255) |
| `White` / `Black` / `Grey` | Solid fills for masks, tints, defaults |
| `Grid` | Light reference grid |

Every texture is a package **root asset**, so it is always compiled and loadable by URL from any
project that references the pack.

## Quick start

Reference the pack, then drop a texture onto a sprite, UI image or material in Game Studio — or load
one in code by its name:

```csharp
var missing = Content.Load<Texture>("Missing");   // Checker, Error, UVGrid, Normal, White…
```

## Demo

Open `StridePlaceholderTextures.sln`, set **Demo.Windows** as the startup project and run — every
pattern is shown on a grid of textured quads.

## License

MIT. See [LICENSE.md](LICENSE.md).
