# Unity SOAP Extensions

Unity SOAP Extensions is an add-on package for the Obvious SOAP asset.

This package adds listener components that expand SOAP workflows with additional value-driven behavior and inspector-friendly setup.

## Features

- Variable Listeners
  - Listen to SOAP ScriptableVariables and invoke UnityEvents on value changes.
  - Includes binding controls (`UNTIL_DESTROY` and `UNTIL_DISABLE`) and optional invoke-on-subscribe behavior.

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

## Runtime Folder Layout

- `Runtime/Variable Listeners`
  - Variable listeners
  - Variable value listeners
  - Shared variable listener base classes

- `Runtime/Event Listeners`
  - Event value listeners
  - Shared event value listener base class

## Notes

- These extensions are designed to follow SOAP patterns while adding value-filtered listener workflows.
- Inspector fields include summaries and tooltips to make setup and maintenance easier.
