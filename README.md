# 🚀 Hierarchy State Machine

[![GitHub release](https://img.shields.io/badge/version-v1.0.0-blue)](https://github.com/Ohenzy/event-bus-ugl/releases/tag/v1.0.0)
[![Unity](https://img.shields.io/badge/Unity-black.svg?logo=unity)](https://unity.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

---

## 📦 **Installation**  
### **Via Git URL (Unity Package Manager)**  
1. Open **Package Manager** → **Add package from git URL** → Paste:<br>
   https://github.com/Ohenzy/state-machine-ugl.git?path=/Assets/Scripts/StateMachine/#v1.0.0
   
3. Click **Add**.  

## 🛠 **Usage**  
### **1. Add States**  
```csharp
private void Awake()
{
    _stateMachine = IStateMachine.Builder()
        .StartWith<CharacterIdleState>()
        .Add(new CharacterDodgeState())
        .Add(new CharacterState()
            .Add(new CharacterIdleState())
            .Add(new CharacterMovementState())
        )
        .Add(new CharacterCombatState()
            .Add(new CharacterCombatIdleState())
            .Add(new CharacterCombatMovementState())
        )
        .Build();
}
```

### **2. Apply state**  
```csharp
private void OnCharacterMovement()
{
    _stateMachine.Apply<CharacterMovementState>();
}
