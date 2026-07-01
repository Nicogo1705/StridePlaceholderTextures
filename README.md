# Placeholder Textures

**Fallback / debug textures for prototyping** in [Stride](https://stride3d.net) — one `Texture`
asset per pattern (512×512). Drop them in whenever an asset is missing, broken, or not made yet, so
your scene never renders "nothing".

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

## Usage

Open the project in Game Studio — the textures appear under the `Assets` folder, ready to drop onto
sprites, UI images or materials. Each `Assets/<Name>.sdtex` sources `Resources/<Name>.png`.

## License

MIT.
