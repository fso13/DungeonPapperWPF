# Промпты для агентов: «Бумажные подземелья» → Godot 4 (C#)

Набор из **15 задач** для поочерёдной (или параллельной) работы агентов.

**Перед стартом любой задачи агент должен прочитать:** `docs/GODOT_MIGRATION_SPEC.md`

**Репозиторий-источник ассетов:** текущий WPF-проект (`Resources/`, `code/`)

**Стек:** Godot 4.x, C# (.NET), GDExtension не использовать.

---

## Как использовать

1. Выполнять задачи **по номеру**, если не указано иное.
2. После каждой задачи — **запуск проекта в Godot**, smoke-test.
3. Задачи **3, 8, 14** можно запускать параллельно (разные папки, без конфликтов).
4. Каждый промпт копировать целиком в нового агента.
5. В конце агента проверять **критерии приёмки** из таблицы внизу.

---

## Задача 01 — Инициализация проекта Godot + C#

### Промпт

```
Создай новый Godot 4 проект с поддержкой C# в папке `godot/` в корне репозитория.

Требования:
- Godot 4.x, шаблон .NET
- Структура папок согласно docs/GODOT_MIGRATION_SPEC.md (раздел 5)
- Autoload: GameManager, SceneRouter, NetworkManager, ConfigService
- Пустые сцены-заглушки для всех 12 экранов меню (MainMenu, ModeSelect, MapSelect, SoloSetup, JoinScreen, Lobby, ChallengeScreen, ChallengeStageResult, ChallengeComplete, Results, Settings, GameRoot)
- SceneRouter с методами навигации и классом GameLaunchContext
- Импортируй PNG из ../Resources/ в godot/assets/ (сохрани подпапки: heroes, dice, monsters, ui)
- README в godot/README.md: как открыть и собрать проект

Не реализуй игровую логику. Только каркас.
Язык кода: C#. Комментарии в коде — на русском или английском, единообразно.
```

### Критерии приёмки

- [ ] Проект открывается в Godot без ошибок
- [ ] `dotnet build` проходит
- [ ] Autoload зарегистрированы в project.godot
- [ ] MainMenu — стартовая сцена
- [ ] Ассеты импортированы и видны в FileSystem

---

## Задача 02 — Экраны меню и навигация

### Промпт

```
Реализуй UI и навигацию для всех экранов меню в Godot C# проекте (godot/).

Читай: docs/GODOT_MIGRATION_SPEC.md, разделы 4 и 10.

Экраны:
1. MainMenu — логотип, «Выбрать режим», «Настройки», «Выход»
2. ModeSelect — карточки: Соло, Мультиплеер (подменю Хост/Клиент), Челлендж; показ прогресса челленджа если есть сохранение
3. MapSelect — сетка из 12 слотов-карт (пока заглушки), панель деталей, кнопки Назад/Продолжить
4. SoloSetup — заглушки выбора цели и таланта (2+2 карты), чекбокс миссий, «Начать»
5. JoinScreen — IP, порт, никнейм, кнопка подключения
6. Lobby — список игроков (заглушка), готовность, выбор карты (только хост), старт
7. ChallengeScreen — прогресс 0/12, список сценариев, Продолжить/Начать заново
8. ChallengeStageResult, ChallengeComplete — заглушки с кнопками навигации
9. Results — заглушка итогов
10. Settings — никнейм, громкость, язык (пока без i18n)

SceneRouter должен проводить по всему flow:
MainMenu → ModeSelect → MapSelect → SoloSetup → GameRoot (заглушка «Игра»)
MainMenu → ModeSelect → ChallengeScreen → GameRoot
MainMenu → ModeSelect → JoinScreen/Lobby → GameRoot

Используй Control-узлы, контейнеры (VBox/HBox/Grid), anchors full rect.
Стиль — простой, без финальной полировки.
```

### Критерии приёмки

- [ ] Можно пройти все ветки навигации без ошибок
- [ ] Кнопка «Назад» работает на каждом экране
- [ ] GameLaunchContext заполняется при переходе к GameRoot
- [ ] Нет hardcoded путей вне SceneRouter

### Зависимости

Задача 01

---

## Задача 03 — Resource-классы и ContentLoader

### Промпт

```
Создай Godot Resource-классы для данных игры в godot/scripts/data/.

Читай: docs/GODOT_MIGRATION_SPEC.md, раздел 6.

Классы (C#, [GlobalClass]):
- DungeonData, RoomData, MinionData, TreasureData
- BossData, BossTierData
- MissionData, ObjectiveData, TalentData, ArtifactData
- DieFaceData
- ChallengeConfig, ChallengeProgress

Также:
- Enums.cs: HeroClassType, Outlook, DieSymbol, DieColor, Barrier, TreasureType, GameMode, GamePhase
- ContentLoader.cs — статические методы LoadDungeon, LoadBoss, LoadAllMissions и т.д.

Создай 1 тестовое подземелье resources/dungeons/dungeon_01.tres (6×7, несколько комнат с приспешником, ловушкой, самоцветом).
Создай 3 BossData для dungeon_01.
Создай ChallengeConfig main_campaign.tres с порядком dungeon_01..dungeon_12 (остальные id — заглушки).

Данные для dungeon_01 возьми за основу из code/Quests.cs, сверь с правилами.
```

### Критерии приёмки

- [ ] Все Resource-классы видны в редакторе Godot (New Resource)
- [ ] dungeon_01.tres открывается и содержит 42 комнаты
- [ ] ContentLoader загружает dungeon_01 без ошибок
- [ ] MapSelect может отобразить название и превью dungeon_01 (подключи в задаче 02 или здесь)

### Зависимости

Задача 01. Можно параллельно с задачей 02.

---

## Задача 04 — Runtime-модели и DiceRoller

### Промпт

```
Создай чистые C# модели и DiceRoller БЕЗ зависимости от Godot Node.

Папка: godot/scripts/models/ и godot/scripts/core/

Модели:
- Hero, Party, PotionSlot, ArtifactState
- Die, PlayerState, GameSession, PlayerAction
- GameResults, BossFightResult, CombatResult

DiceRoller:
- RollAll(): 3 белых + 3 чёрных кубика
- RollWithReroll(): переброс если черепов >= 3 ИЛИ клеверов >= 3
- Маппинг number → DieSymbol по правилам (0=сапоги, 1-8 герои, 9=клевер, 10-11=череп)

Создай тестовый проект или Godot test scene с NUnit/xUnit тестами (минимум 8 тестов):
- переброс при 3 черепах
- переброс при 3 клеверах
- корректный маппинг всех 12 граней
- начальное здоровье партии = 4
- MaxHealth = сумма уровней

Читай: docs/GODOT_MIGRATION_SPEC.md, разделы 6.3 и 7.2 (DiceRoller).
```

### Критерии приёмки

- [ ] Модели не ссылаются на Godot API (кроме Vector2I если нужно)
- [ ] Все тесты DiceRoller зелёные
- [ ] `dotnet test` проходит

### Зависимости

Задача 03 (enums)

---

## Задача 05 — RulesEngine: действия и урон

### Промпт

```
Реализуй RulesEngine (часть 1): урон, прокачка, зелья, артефакты.

Папка: godot/scripts/core/

Классы:
- DamageResolver — ранения, зелья, воскрешение (-9 славы флаг)
- ActionResolver — CanLevelUp, ApplyLevelUp, CanCraft, ApplyCraft, CanBrew, ApplyBrew
- GloryCalculator — пока только вспомогательные методы

Правила (из docs/GODOT_MIGRATION_SPEC.md §7):
- Прокачка: символ + цвет = тип личности; маг 4+ игнорирует цвет; клевер = любой класс
- Способности на 4 уровне: воин +1 vs боссы, маг игнор цвета, священник иммунитет к черепам, плут = самоцвет
- Зелья: любой кубик → 2 зелья, макс 12; награды на 4/8/12
- Артефакты: 1 часть за кубик, 8 предметов, первые 4 требуют символ

Unit-тесты (минимум 15 новых):
- прокачка с правильным/неправильным цветом
- маг 4+ с любым цветом
- зелье поглощает ранение раньше шкалы здоровья
- воскрешение при wounds >= maxHealth
- крафт 1 и 2 части артефакта

Публичный фасад RulesEngine с методами из спецификации §7.3.
```

### Критерии приёмки

- [ ] 15+ тестов, все зелёные
- [ ] Нет Godot Node в RulesEngine
- [ ] Способности 4-го уровня покрыты тестами

### Зависимости

Задача 04

---

## Задача 06 — RulesEngine: движение и комнаты

### Промпт

```
Реализуй RulesEngine (часть 2): движение, комнаты, приспешники.

Классы:
- MovementValidator — BFS, 2/3 шага, стены, реки, тоннели на краях
- CombatResolver — FightMinion, урон = max(0, level_minion - level_hero)
- ActionResolver расширить: ApplyMove, ResolveRoomEntry

Правила входа в комнату (строгий порядок):
1. Приспешник ИЛИ ловушка (+1 ранение)
2. Награда (зелье / часть артефакта / level up / самоцвет)

Повторный вход:
- приспешник и награда НЕ повторяются
- ловушка срабатывает СНОВА

Артефакты для движения:
- №3 Сапоги скорохода — переход через реки
- №4 Кирка — проход сквозь стены

Первый ход — с любого из 6 входов (нижний ряд, y=6).

Unit-тесты (минимум 15):
- BFS на тестовой карте 6×7
- стена блокирует без кирки
- река блокирует без сапог
- тоннель переносит на противоположный край
- бой с приспешником: победа с/без ранений
- ловушка при повторном входе

Используй dungeon_01.tres в тестах.
```

### Критерии приёмки

- [ ] 15+ новых тестов зелёные
- [ ] GetReachableRooms возвращает корректные клетки
- [ ] ResolveRoomEntry соблюдает порядок событий

### Зависимости

Задача 05

---

## Задача 07 — RulesEngine: боссы, самоцветы, финал

### Промпт

```
Реализуй RulesEngine (часть 3): чудовища, самоцветы, миссии, финальный подсчёт.

Классы:
- CombatResolver.FightBoss — боевая мощь, tier, бегство
- GemResolver — кража, AnnounceGems, вычёркивание у соперников
- MissionChecker — проверка миссий в конце раунда
- GloryCalculator.CalculateFinalResults — полная формула славы

Правила босса:
- мощь = сумма уровней + удвоенный уязвимый герой + бонусы (воин 4+, огненный меч +3)
- мощь < минимума → бегство (штраф славы, 0 ранений)
- в мультиплеере: DetermineBossFightWinner по max мощи
- в соло: награды лидеру нет

Самоцветы:
- 8 на карте + 2 вне (доступны всем)
- объявление в конце раунда → unavailable у остальных
- несколько игроков могут украсть один самоцвет в одном раунде

Unit-тесты (минимум 15):
- боевая мощь с уязвимостью
- бегство при низкой мощи
- tier славы/ран по порогам
- announce gems вычёркивает у других
- финальный подсчёт: минимальный уровень героя, штраф ранений, -9 воскрешение

Добейся 50+ тестов суммарно по RulesEngine.
```

### Критерии приёмки

- [ ] 50+ unit-тестов суммарно, все зелёные
- [ ] CalculateFinalResults покрыт тестами
- [ ] RulesEngine — полный фасад из спецификации §7.3

### Зависимости

Задача 06

---

## Задача 08 — GameStateMachine

### Промпт

```
Реализуй GameStateMachine для управления фазами партии.

Читай: docs/GODOT_MIGRATION_SPEC.md, раздел 8.

- Enum GamePhase (14 фаз)
- GameStateMachine : Node — Advance(), CurrentPhase, CurrentRound
- Сигналы: PhaseChanged, RoundStarted, GameOver
- PhaseWaitTracker — ожидание готовности всех игроков (для сети)
- Интеграция с RulesEngine:
  - DiceRoll → RollWithReroll
  - SkullDamage → ApplySkullDamage
  - ActionPick/Resolve × 3
  - GemAnnounce
  - BossFight на раундах 3, 6, 8
  - RoundEnd / GameOver

SoloGameController:
- связывает GameStateMachine + RulesEngine + GameSession
- SubmitAction(PlayerAction) — валидация и применение
- без UI, только логика

Тесты (интеграционные, минимум 5):
- полный проход 1 раунда programmatically
- переход к BossFight на раунде 3
- GameOver после раунда 8
```

### Критерии приёмки

- [ ] State machine проходит 8 раундов в тесте
- [ ] BossFight вызывается на 3/6/8
- [ ] SoloGameController работает без UI

### Зависимости

Задача 07

---

## Задача 09 — UI планшета: сетка подземелья

### Промпт

```
Реализуй визуальную сетку подземелья и RoomCell.

Сцены: godot/scenes/game/RoomCell.tscn, DungeonGrid.tscn

RoomCell (Control 75×75 или масштабируемый):
- фон (map.jpg)
- стены: left/right/top/bottom (текстуры из assets)
- иконки: приспешник, ловушка, сокровище, самоцвет
- подсветка: зелёный (доступно), синий (путь), тёмно-синий (текущая), красный (цель артефакта)
- Line2D для пути
- спрайт party на текущей клетке
- клик → сигнал RoomClicked(Vector2I)

DungeonGrid:
- GridContainer columns=6, 42 RoomCell
- Bind(DungeonData) — расстановка по координатам
- Bind(PlayerState) — путь, текущая позиция, перечёркнутые приспешники
- HighlightReachable(List<Vector2I>)
- ClearHighlights()

Используй ассеты из godot/assets/. Сверься с WPF Field.xaml для слоёв.
```

### Критерии приёмки

- [ ] dungeon_01 отображается корректно
- [ ] Клик по клетке эмитит сигнал с координатами
- [ ] Подсветка и путь визуально различимы

### Зависимости

Задача 03

---

## Задача 10 — UI планшета: панели героя и инвентаря

### Промпт

```
Реализуй боковые панели планшета игрока.

Сцена: godot/scenes/game/PlayerSheet.tscn

Панели:
1. HealthTrack + WoundTrack + ResurrectionMark
2. HeroPanel — 4 героя × 6 уровней + индикатор типа личности (белый/чёрный)
3. ArtifactPanel — 8 предметов × 2 части (текстуры из assets/abbility/)
4. PotionPanel — 12 зелий × 2 сердца
5. GemTrack — 10 кружков
6. MinionTrack
7. RoundIndicator — 8 раундов, метки боссов на 3/6/8
8. GloryLabel
9. DicePoolPanel — 6 кубиков (кликабельные)
10. ActionSlots — 3 слота выбранных кубиков
11. Кнопки: «2 движения», «Повысить уровень», «Сварить зелье», «Ковать предмет», «Выбрать кубик»

PlayerSheetController:
- Bind(PlayerState, GamePhase)
- UpdateFromState() — обновление всех панелей
- Сигналы: DieSelected, ActionRequested, RoomClicked (проброс)
- Enable/disable кнопок по фазе

Пока без диалогов — заглушки GD.Print при выборе действия.
```

### Критерии приёмки

- [ ] Все панели обновляются из PlayerState
- [ ] Кубики кликабельны, выделение видно
- [ ] Кнопки действий disabled вне своей фазы

### Зависимости

Задача 09

---

## Задача 11 — Связка соло: играбельная партия

### Промпт

```
Собери полный соло-режим: UI + RulesEngine + StateMachine.

1. GameRoot.tscn — компоновка PlayerSheet + HUD (раунд, кнопка «Бросить кубики»)
2. SoloGameController — полная связка:
   - старт из GameLaunchContext (dungeon, objective, talent)
   - PhaseChanged → обновление UI
   - клик кубика → выбор → кнопка действия → диалог (простой) → Apply
   - движение: подсветка GetReachableRooms → клик → ApplyMove
   - босс: BossFightDialog с результатом
3. Диалоги (минимальные):
   - LevelUpDialog — выбор героя
   - ArtifactCraftDialog — выбор предмета
   - BossFightDialog — сила, слава, раны
4. SoloSetup — подключи реальные ObjectiveData/TalentData (2 случайные карты)
5. MapSelect — загрузка всех dungeon_*.tres из папки
6. Results — разбивка славы по категориям

Flow: MainMenu → Соло → Карта → Setup → Игра (8 раундов) → Results

Используй dungeon_01 для smoke-test. Цель/талант — любые из resources.
```

### Критерии приёмки

- [ ] Можно пройти полную партию 8 раундов на dungeon_01
- [ ] Прокачка, зелья, движение, босс работают
- [ ] Results показывает итоговую славу
- [ ] Нет crash при типичном прохождении

### Зависимости

Задачи 08, 10

---

## Задача 12 — Режим челлендж

### Промпт

```
Реализуй режим челленджа.

Классы:
- ChallengeController (autoload или сервис)
- ChallengeSaveService — user://challenge_{id}.tres

Экраны:
- ChallengeScreen — прогресс, список 12 сценариев, Продолжить/Сброс
- ChallengeStageResult — слава за этап, кнопка «Следующее»
- ChallengeComplete — итог за 12 сценариев

Логика:
- ChallengeConfig main_campaign.tres — порядок dungeon_01..12
- После партии в режиме Challenge: сохранить славу, CurrentIndex++, переход на ChallengeStageResult
- «Следующее подземелье» → новая партия на следующей карте (с нуля, MVP)
- Сброс с подтверждением
- ModeSelect показывает прогресс (N/12)

Интеграция: GameLaunchContext.IsChallenge = true, без MapSelect.
```

### Критерии приёмки

- [ ] Прогресс сохраняется между сессиями
- [ ] Можно пройти 2 сценария подряд
- [ ] Сброс обнуляет прогресс
- [ ] ChallengeComplete на 12-м сценарии

### Зависимости

Задача 11

---

## Задача 13 — Сеть: хост, лобби, синхронизация

### Промпт

```
Реализуй сетевой мультиплеер (хост = сервер).

Читай: docs/GODOT_MIGRATION_SPEC.md, раздел 11.

NetworkManager:
- HostGame(port), JoinGame(ip, port), Disconnect
- обработка peer_connected/disconnected

Лобби:
- RpcLobbyUpdate — список игроков, готовность, выбранная карта
- хост выбирает карту через MapSelect
- каждый выбирает objective/talent до старта
- RpcStartGame → все в GameRoot

Игровая синхронизация (RPC из спецификации §11.2):
- RpcSyncPhase, RpcSyncDicePool
- RpcPickDie, RpcSubmitAction
- RpcSyncPlayerState
- RpcAnnounceGems, RpcSyncUnavailableGems
- RpcBossFightResults, RpcGameOver

Сервер:
- единственный вызывает RulesEngine
- валидирует все действия
- рассылает снимки PlayerState

Клиент:
- только UI + отправка intent
- принимает RpcSync* и обновляет локальное отображение

Тест: 2 экземпляра (или headless), хост + клиент, 1 раунд.
```

### Критерии приёмки

- [ ] Хост создаёт игру, клиент подключается по IP
- [ ] Оба видят один бросок кубиков
- [ ] Действия клиента применяются после валидации сервером
- [ ] Можно завершить 1 полный раунд вдвоём

### Зависимости

Задача 11

---

## Задача 14 — Сеть: самоцветы, боссы, отключения

### Промпт

```
Дополни мультиплеер до полной партии.

1. GemAnnounce фаза:
   - GemAnnounceDialog на клиенте
   - RpcAnnounceGems → сервер собирает → RpcSyncUnavailableGems
   - обновление GemTrack и недоступных самоцветов на карте

2. BossFight:
   - параллельный расчёт на сервере для всех кто дошёл до комнаты босса
   - RpcBossFightResults — слава, раны, победитель награды
   - BossFightDialog на клиентах

3. Миссии:
   - MissionCompleteDialog в конце раунда
   - RpcSyncMissions

4. Отключения:
   - пауза 60 сек при disconnect
   - хост может продолжить без игрока (skip)

5. Results для мультиплеера — таблица всех игроков

Тест: 2 игрока, полная партия 8 раундов по LAN.
```

### Критерии приёмки

- [ ] Самоцветы синхронизируются между игроками
- [ ] Босс на раунде 3 работает для обоих
- [ ] GameOver показывает победителя
- [ ] Disconnect не крашит сервер

### Зависимости

Задача 13

---

## Задача 15 — Контент, локализация, полировка

### Промпт

```
Финальный контент и релизная подготовка.

1. Создай resources для:
   - 12 подземелий (dungeon_01..12) — данные из code/Quests.cs, сверка с rulebook
   - 12 боссов
   - 16 миссий, 16 целей, 16 талантов
   - 8 артефактов (если ещё не созданы)

2. Локализация:
   - ru.po, en.po
   - обёртка Tr() для UI строк

3. Звуки (placeholder или из assets):
   - бросок кубиков, урон, level up, победа

4. Экспорт:
   - preset Windows Desktop, Linux
   - godot/README.md — инструкция сборки

5. Финальный проход по чеклисту docs/GODOT_MIGRATION_SPEC.md §15

Не выдумывай данные карт — бери из code/Quests.cs и PDF правил. Если данных не хватает — TODO с комментарием.
```

### Критерии приёмки

- [ ] Все 12 карт выбираются в MapSelect
- [ ] Челлендж проходит по всем 12
- [ ] Экспорт Windows запускается
- [ ] Чеклист §15 — минимум 90% пунктов

### Зависимости

Задачи 12, 14

---

## Параллельный запуск

| Можно параллельно | Задачи | Условие |
|-------------------|--------|---------|
| Меню + Данные | 02 + 03 | разные папки |
| Модели + UI сетка | 04 + 09 | после 03 |
| RulesEngine части | 05, 06, 07 | строго по порядку |
| Контент карт | 15 (частично) | после 03, независимо от сети |

---

## Шаблон промпта для багфикса (после плейтеста)

```
Контекст: Godot 4 C# проект «Бумажные подземелья» в папке godot/.
Спецификация: docs/GODOT_MIGRATION_SPEC.md

Баг: [описание]
Шаги воспроизведения: [1, 2, 3]
Ожидание по правилам: [цитата из rulebook]
Фактическое поведение: [что происходит]
Скриншот/лог: [если есть]

Исправь минимальным diff. Добавь unit-тест если применимо.
Не трогай несвязанный код.
```

---

## Шаблон промпта для одной карты подземелья

```
Создай resources/dungeons/dungeon_XX.tres и 3 связанных BossData.

Источник данных: code/Quests.cs (квест XX), сверь с rulebook PDF.
Спецификация формата: docs/GODOT_MIGRATION_SPEC.md §6.2.

Включи: все 42 комнаты, стены, приспешников, ловушки, самоцветы, позиции 3 боссов.
OrderIndex = XX для челленджа.
PreviewImage — map.jpg или уникальная текстура если есть.

Проверь загрузку через ContentLoader.LoadDungeon("dungeon_XX").
```

---

## Сводка: порядок и сроки (ориентир с агентом + ревью)

| # | Задача | Ориентир | Кто проверяет |
|---|--------|----------|---------------|
| 01 | Каркас проекта | 0.5–1 дн | запуск Godot |
| 02 | Меню | 1–2 дн | навигация |
| 03 | Resources | 1–2 дн | dungeon_01 в редакторе |
| 04 | Модели + кубики | 1 дн | dotnet test |
| 05 | Rules: действия | 2–3 дн | тесты |
| 06 | Rules: движение | 2–3 дн | тесты |
| 07 | Rules: боссы | 2–3 дн | 50+ тестов |
| 08 | State machine | 1–2 дн | интеграционный тест |
| 09 | UI сетка | 1–2 дн | визуал |
| 10 | UI панели | 2–3 дн | визуал |
| 11 | Соло E2E | 2–3 дн | полная партия |
| 12 | Челлендж | 1–2 дн | 2 сценария подряд |
| 13 | Сеть база | 2–3 дн | 2 клиента, 1 раунд |
| 14 | Сеть полная | 2–3 дн | 8 раундов вдвоём |
| 15 | Контент + релиз | 3–5 дн | экспорт |

**Итого с агентами и вашим ревью:** ~3–6 недель до MVP, ~2–3 месяца до полной версии.

---

*Версия 1.0. Связанный документ: `docs/GODOT_MIGRATION_SPEC.md`*
