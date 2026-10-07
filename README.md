# ⚔️ Final Survival Project R

> **레벨 성장 · 보스 패턴 · 인벤토리 · NPC · 저장 기능을 포함한 2D Survival / Action 프로젝트**

<p>
  <img src="https://img.shields.io/badge/Unity-2022.3.7f1-000000?logo=unity&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-512BD4?logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/Genre-2D%20Action%20%2F%20Survival-d73a49" />
</p>

## 🎮 About

플레이어 성장과 전투를 중심으로 여러 시스템을 결합한 **2D 액션/서바이벌 게임 프로젝트**입니다.  
단순 이동·공격을 넘어 레벨업, 스탯, 보스 패턴, 인벤토리, NPC 대화, JSON 저장 등 게임의 전체 흐름을 구성하는 기능을 직접 구현했습니다.

## ✨ Core Features

- 🧬 레벨 / 경험치 / 스탯 포인트 성장 시스템
- ❤️ HP · 공격력 · 치명타 · 이동속도 · 점프력 등 플레이어 스탯
- 👾 일반 적 / 보스 전투
- 💥 회전탄 · 미사일 · 돌진 등 보스 패턴
- 🎒 인벤토리 및 아이템 관리
- 💬 NPC / 대화 / 타이핑 효과
- 💾 `JsonUtility` 기반 게임 데이터 Save / Load
- 🔊 사운드 및 설정 관리
- 🎥 카메라 추적
- ⚠️ Trap / Event 시스템
- 🖥 게임 상태 UI 및 알림 메시지

## 🛠 Tech Stack

| Category | Technology |
| --- | --- |
| Engine | Unity 2022.3.7f1 |
| Language | C# |
| Data | JSON / JsonUtility |
| UI | TextMeshPro / Unity UI |
| Game Type | 2D |

## 🧩 Architecture

```text
Assets/Script/
├─ GameManager.cs          # 전역 게임 데이터 / Save & Load / UI
├─ PlayerController.cs     # 플레이어 제어
├─ EnemySystem.cs          # 일반 적
├─ BossSystem.cs           # 보스 AI / 패턴 / HP
├─ BossTurretSystem.cs     # 보스 터렛
├─ BulletSystem.cs         # 투사체
├─ InventorySystem.cs      # 인벤토리
├─ ItemManager.cs          # 아이템
├─ NPCSystem.cs            # NPC
├─ TalkManager.cs          # 대화
├─ EventManager.cs         # 이벤트
├─ Trap.cs                 # 함정
├─ SoundManager.cs         # 사운드
└─ SettingsManager.cs      # 설정
```

## 💾 Data Example

플레이어의 레벨, 경험치, 골드, HP, 공격력, 치명타, 이동 속도 등의 데이터를 `GameData`로 관리하고 JSON 형태로 저장/불러오도록 구성했습니다.

## 🚀 Run

1. Unity Hub에서 프로젝트를 추가합니다.
2. **Unity 2022.3.7f1** 환경에서 엽니다.
3. 게임 Scene을 열고 Play 버튼으로 실행합니다.

---

### 👨‍💻 Developer

**JanMatny327**  
여러 독립 시스템을 하나의 게임 루프로 연결하고, 전투/성장/데이터 구조를 직접 설계한 프로젝트입니다.
