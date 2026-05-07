# Changelog

## [1.2.0] - 2026-05-07
Changed
- Removed typed `UnityEvent` wrapper classes (e.g. `BoolUnityEvent`, `IntUnityEvent`) from all variable listeners, variable value listeners, and event value listeners. Replaced with inline `UnityEvent<T>` fields, which Unity is able to serialize in modern versions.

## [1.1.0] - 2026-04-09
Added
- `BindTransform` — Reads or writes transform position, rotation, or scale to/from a Vector3Variable. Supports world and local space, per-axis constraints, and auto-resolves to this GameObject's transform if no target is assigned.
- `BindGameObjectActiveState` — Binds a BoolVariable to a GameObject's active state, with an optional invert toggle and auto-fallback to this GameObject.

## [1.0.0] - 2026-04-09
Added
- Variable listeners for all SOAP variable types with configurable binding modes.
- Variable value listeners for all SOAP variable types.
- Event value listeners for all SOAP event types.

