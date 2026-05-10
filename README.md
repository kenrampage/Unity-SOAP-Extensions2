# Unity SOAP Extensions

Unity SOAP Extensions is an addon package for the Obvious SOAP package [available from the Unity Asset Store](https://assetstore.unity.com/packages/tools/utilities/soap-scriptableobject-architecture-pattern-232107?srsltid=AfmBOoq50efPZ-9k1mTLjuLdR9qWUaXgY4C4KpRLr5_UqcQZREA6R1go)

This package adds listener components that expand SOAP (ScriptableObject Architecture Pattern) workflows with additional value driven behavior and inspector friendly components.

## Features

- Bindings
  - `BindTransform` — Reads or writes transform position, rotation, or scale to/from a Vector3Variable. Supports world and local space, per-axis constraints, and auto-resolves to this GameObject's transform if no target is assigned.
  - `BindGameObjectActiveState` — Binds a BoolVariable to a GameObject's active state. Supports an invert toggle to flip the relationship. Auto-falls back to this GameObject if no target is assigned.

- Variable Listeners
  - Listen to SOAP ScriptableVariables and invoke UnityEvents on value changes.

- Variable Value Listeners
  - Listen to a single ScriptableVariable and invoke events only when the incoming value matches configured entries.
  - If multiple entries match, all matching entries are invoked in array order.

- Event Value Listeners
  - Listen to SOAP ScriptableEvents and invoke events only when the raised event value matches configured entries.
  - If multiple entries match, all matching entries are invoked in array order.

## Supported Types

The listener sets cover the following SOAP types:

- `bool`
- `float`
- `int`
- `string`
- `Vector2`
- `Vector3`
- `Vector2Int`
- `Color`
- `Quaternion`
- `Component`
- `GameObject`
