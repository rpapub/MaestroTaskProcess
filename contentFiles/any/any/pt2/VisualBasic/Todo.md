# TODO Report

Generated: 2025-06-26 22:07:20

Absolutely — here’s a structured breakdown of **todo items** needed to implement intelligent `request.payload` handling in your template, with clear separation of concerns and extensibility in mind:

---

## ✅ TODOs to Implement Smart `request.payload` Behavior

### 🧱 1. **Structure-Level: Represent the Full Request**

* [ ] Create `Request.cs` model with:

  * Top-level `Version` string
  * Nested `RequestEnvelope` containing:

    * `CorrelationId`, `RequestId`, `Timestamp`, `LogContext`
    * `Payload` as `object` or `Dictionary<string, object>`

> 🧠 `Payload` must remain flexible and loosely typed.

---

### 🧠 2. **Detection Logic: Is Payload a Simple String?**

* [ ] Add utility method in `RequestDeserializer.cs`:

  * Try to cast `request.payload` to `string`
  * If successful, return the string
  * If not, return `null`

```csharp
public static string TryExtractSimpleStringPayload(Request req)
```

* [ ] Optionally, return `bool success` or throw if schema expectations are violated

---

### 🧪 3. **Developer Hook: Payload Extraction Entry Point**

* [ ] Define `ExtractPayload<T>(...)` method stub:

  * Accepts `object` or `Dictionary<string, object>`
  * Throws `NotImplementedException`
  * Document clearly: “Override this in each implementation”

```csharp
public static T ExtractPayload<T>(object rawPayload)
```

* [ ] Allow the developer to plug in:

  * `JObject.ToObject<T>()`
  * Manual mapping
  * Third-party schema mappers (optional)

---

### 📑 4. **Logging and Traceability (Optional but Recommended)**

* [ ] Log a debug message if payload is a simple string:

  * `"Interpreted request.payload as string: '..."`

* [ ] If `request.payload` is not a string, log its `Type.FullName` for traceability

---

### 📂 5. **Scaffolding and Documentation**

* [ ] Document this clearly in `README.md` under “Handling `request.payload`”

  * Explain fallback behavior (string detection)
  * Explain expectation to implement custom deserialization

* [ ] Include a template file:

  * `Framework/Adapters/RequestDeserializer.cs`
  * Stubbed methods as described

* [ ] Optionally provide:

  * `sample-string-payload.json`
  * `sample-object-payload.json`

---

### 🚫 6. **What NOT to Do in the Template**

* [ ] Don’t attempt deep schema validation of `payload`
* [ ] Don’t hardcode assumptions about payload fields
* [ ] Don’t require `payload` to be deserialized unless it’s clearly structured

---

Would you like this list turned into a `tasks/todo.md` for the repo or a Jira-style breakdown with tags and priorities?
