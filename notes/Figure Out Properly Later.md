# How to mix enums (type safety) and strings (flexibility)

You said you want enum safety + string flexibility. Here are recommended patterns (pick one):

1. **Enum + Decoration Object (recommended)**
	- Keep a strict enum for domain values, but attach a `Decoration` object for free-text context.
	- `Fact.Value = EnumVal` and `Fact.Decoration = {note:string, claimSource:string, rawText:string}`
	- Pros: type safety for logic; flexible human context.
	- Cons: need to map messy input strings -> enum; conversion logic required.
	- Fix: provide robust extractors / annotators and fallback `Enum.Val.Undefined` with high-confidence conversion attempts.
2. **Sealed Class / Discriminated Union (DUs)**
	- Use a sealed class per domain where each case has structured data: `RapeStatus.GirlRaped(details)` or `RapeStatus.Allegedly(details)`.
	- Pros: expressive, type-safe, no separate decoration.
	- Cons: more complex serialization and DB storage.
	- Fix: provide serialization helper/DTO layer.
3. **Enum as Key + String Tag**
	- Store value as enum key plus free `string tag` that can override textual representation.
	- Pros: simplest to persist.
	- Cons: tag may be used incorrectly and cause semantic drift.
	- Fix: validation rules and standardization pipeline.
- ---
- # Performance & scale tips

- Use **incremental evaluation**: only reevaluate rules that reference changed facts.
- Keep a **fact index** mapping predicate → rules (fast rule invalidation).
- Use **batching** and asynchronous worker pool for heavy inference — but keep tracing synchronous for each applied rule (so trace ordering is correct).
- For possible-worlds: apply constraint propagation early to prune infeasible branches.
- Persist world snapshots lazily (store diffs).
---
# Quick text diagrams

**Fact flow with tracing**
```rust
UserInput -> FactNormalizer -> Fact{EnumVal,Decoration,Confidence} -> KB
KB -> AgendaBuilder -> RuleEvalFacade -> (ShortCircuit? Trace) -> ApplyConsequences -> KB update -> TraceAppend
```

**Conflict resolution decision tree**
```pgsql
Conflict detected
 ├─ if automaticResolutionAllowed?
 │    ├─ compare CardinalRank -> pick winner -> record rationale
 │    └─ if tie -> use SourceReputation -> pick or escalate
 └─ else -> Flag & Hold -> create Analyst Kanban card
```
---
# (project-level) + solutions


1. **Traceability** — each inference step is recorded; defensible in court.
2. **Modular** — domain modules (justice/health/military/counselling) can be swapped in/out.
3. **Expressive uncertainty** — unknowns are first-class, so you won’t accidentally drop nuance.
4. **Complex rule management** — large rulebases become hard to maintain.
	- *Fix*: rule metadata, versioning, authoring UI, automated tests & rule linter.
5. **Combinatorial explosion of scenarios/possible worlds**
	- *Fix*: early constraint propagation, scoring/pruning heuristics, and cap on branching (with analyst escalation).
6. **Risk of misuse/automation errors in high-stakes contexts**
	- *Fix*: default to human-in-the-loop for high-impact conclusions; implement hard thresholds and safety gates; produce explainability artifacts for any automated decision.
- ---
- # Implementation roadmap (practical next steps)

1. **Minimal viable kernel**
	- Fact model with enum + decoration.
	- Rule structure with antecedents/consequents, priority.
	- Simple forward chainer, function facades, trace logging.
2. **Add provenance store**
	- Append-only trace ledger, kanban templating integration.
3. **Add conflict handling**
	- Flagging + basic cardinal resolution + analyst UI hooks.
4. **Add modules**
	- Start with one domain (law enforcement ruleset) and iterate.
5. **Hardening**
	- Tests, IRB/legal review, privacy & encryption, RBAC.
6. **Scale**
	- Add incremental evaluation, worker pools, possible-worlds analyzer.
- ---
- - Treat `unknown` as a *distinct semantic state* — don’t collapse silently; always keep provenance on why it’s unknown.
- Make rule priority explicit and auditable — store human-readable reason for each priority level.
- Keep a “why not triggered” log for rules that didn’t fire — useful in debugging.
- Version every rule change and tie it to trace entries so older traces can be replayed with the original rule definitions.
- Build an “explanation template library” per stakeholder (analyst/court/field officer) to present the same chain with different verbosity.
- Use small deterministic utilities for mapping free input to enum values; log every ambiguous mapping as a candidate for human review.
---
# High-level constraints I’ll assume

- Truth model for this conversation: **ternary** (`False`, `Unknown`, `True`) — `Unknown` counts as *possible/triggering* in inference but stays distinct in provenance.
- All logical operations use **function facades** implemented as commands (so every evaluation is an object that can be traced, replayed, stored).
- Facts use **enums for domain values** (type-safety). If human text needed, use a `Decoration` object attached to each fact.
- Forward-chaining core; parallels/worlds spawn when `Unknown` branches; no hard limit on parallels (your requirement).
- Conflict handling defaults to **flag & continue** (produce conflicts, continue inference per-branch), with conflict queue for analyst review.
---
Pros

1. **Forensic traceability** — every decision is fully reconstructable; auditability is strong.
2. **Domain-safe typing** — enums keep inference stable and easier to validate.
3. **Extensible branching** — parallel worlds let you explore uncertainty without losing nuance.

Cons + fixes

1. **Branch explosion** — parallel worlds can grow exponentially.
	- *Fixes*: aggressive constraint propagation, structural merge of equivalent worlds, score-based pruning, adjustable branching policies (per rule).
2. **Rule-authoring complexity for non-programmers** — domain experts may struggle with precise rule logic.
	- *Fixes*: build GUI rule-authoring, LL(k) DSL with clear templates, and a rule linter that suggests problems and examples.
3. **Data-model friction (enums vs. free text)** — mapping messy inputs into strict enums can be a bottleneck.
	- *Fixes*: implement an annotator pipeline that proposes enum mappings with confidence; log unmapped items into a review queue and provide quick-edit UI for mapping.
- ---
- 