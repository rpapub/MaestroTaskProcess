# MaestroTaskProcess

## 1. Introduction

**MaestroTaskProcess** is a UiPath Studio template designed for use in **UiPath Maestro™** BPMN workflows. It provides a standardized, API-style input/output interface for RPA tasks triggered via the **“Start and wait for RPA workflow”** action. The template is intended for pro developers building production-grade automations within orchestrated business processes.

## 2. Project Goal

The goal of this repository is to provide a **standardized, reusable UiPath Studio template** for implementing **RPA workflows** that are executed as **tasks** within **UiPath Maestro** BPMN processes.

This template serves as a starting point for developers building production-ready automations that:

* Conform to a well-defined input/output interface
* Can be reliably invoked using the “Start and wait for RPA workflow” action in Maestro
* Support structured data exchange, traceability, and versioning
* Separate business logic from orchestration and integration concerns

The focus is on enabling repeatable, maintainable, and composable automations in complex, multi-agent enterprise workflows orchestrated by Maestro.

## 3. Rationale

Typical UiPath automation projects are built around queue-based execution, where input data is delivered as queue items and processed asynchronously by robots. This works well for transactional, decoupled automation scenarios.

However, when UiPath RPA workflows are used as **task elements** in a **BPMN process orchestrated by UiPath Maestro**, the execution model changes significantly:

* The RPA process is invoked **explicitly and synchronously** from the BPM engine.
* Inputs are passed as **structured arguments**, not queue items.
* The workflow is expected to return **output and status data** directly to the BPMN engine.
* Execution is part of a larger, coordinated business process model, possibly involving human-in-the-loop tasks, decision rules, or AI agents.

This creates a need for a **reliable, standardized project structure** that:

* Accepts structured arguments in a predictable, versioned format.
* Returns outputs in a consistent shape.
* Handles exceptions and status reporting gracefully.
* Operates transparently within the BPMN lifecycle.

The `MaestroTaskProcess` template is designed to meet that need and provide a reusable foundation for RPA processes called via **“Start and wait for RPA workflow”** tasks in Maestro.


## 4. How It Works  
TODO: Summarize how the template integrates with Maestro and UiPath Orchestrator.

## 5. Standardized Input/Output Interface

This template defines a clear interface for communication between UiPath RPA processes and Maestro BPMN tasks.

### 🔹 Input (required)

| Field             | Direction | Type   | Description                      |
| ----------------- | --------- | ------ | -------------------------------- |
| `version`         | In        | String | Interface version identifier     |
| `request.payload` | In        | Object | Main input data for the workflow |

### 🔹 Output (required)

| Field               | Direction | Type    | Description                        |
| ------------------- | --------- | ------- | ---------------------------------- |
| `status.isSuccess`  | Out       | Boolean | Indicates if execution succeeded   |
| `status.timestamp`  | Out       | String  | Completion timestamp (ISO format)  |
| `status.durationMs` | Out       | Integer | Execution time in milliseconds     |

Additional optional fields are available for traceability, diagnostics, and extended context.

📁 *See `./docs/interface/` for sample data and schema definitions (in progress).*

## 6. Design Principles

This template follows a set of clear design principles to ensure consistency, maintainability, and cross-context usability when used within UiPath Maestro BPMN processes.

### 🔹 API-Oriented Thinking

Although terms like *API (Application Program Interface)* are less common in low-code platforms, this project deliberately embraces them. It treats each RPA workflow as an **API-like unit of execution**: one that accepts a structured input, produces structured output, and follows clear, documented interface contracts.

The process interface mimics **REST API conventions**, including:

* Versioning via an explicit `version` argument
* Standardized request/response format using JSON structures
* Clear separation between payload data and execution metadata (`status`)
* Predictable error handling semantics (`isSuccess`, `errorType`, etc.)

This approach enables clean integration with Maestro, improves traceability, and supports future automation patterns like autonomous agent chains or hybrid human-AI loops.

### 🔹 Reusability

The project is designed to be copied and reused across many BPMN task scenarios, with the business-specific logic isolated from the I/O and orchestration layer.

### 🔹 Clarity

Each component (e.g. arguments, status output, payload contracts) is explicitly defined and documented in `./docs/interface`.

### 🔹 Config Separation

While Maestro provides parameters at runtime, environment-specific details or sensitive values can still be externalized using `Config.xlsx` or Orchestrator assets, as needed.

### 🔹 Structured Outputs

All outputs follow defined schemas to support downstream processing, human review, or debugging via Maestro's visual trace features.


## 7. Getting Started  
TODO: Provide steps to clone, open in Studio, and test the template.

## 8. Usage in Maestro BPMN  
TODO: Explain how to invoke the RPA process using “Start and wait for RPA workflow” in a BPMN model.

## 9. Example Use Case  
TODO: Describe a practical scenario where this template is applied in a real workflow.

## 10. Versioning and Payload Contracts  
TODO: Explain how the `version` argument governs schema expectations and backward compatibility.

## 11. Troubleshooting  
TODO: List common errors (e.g., type resolution issues) and fixes.

## 12. Roadmap  
TODO: Outline planned improvements, potential feature additions, and design evolution.

## 13. Release Notes / Changelog  
TODO: Track major template updates and version tags.

## 14. Contribution Guidelines  
Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for details.

## 15. License  
This project is licensed under the [Creative Commons Attribution 4.0 International (CC BY 4.0)](https://creativecommons.org/licenses/by/4.0/) license.  
© 2025 Christian Prior-Mamulyan

## 16. Credits and Contact  
Created by Christian Prior-Mamulyan  
Contact: cprior@gmail.org
