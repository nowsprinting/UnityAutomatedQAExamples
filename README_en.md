# Unity Automated QA Examples

[![Meta file check](https://github.com/nowsprinting/UnityAutomatedQAExamples/actions/workflows/metacheck.yml/badge.svg)](https://github.com/nowsprinting/UnityAutomatedQAExamples/actions/workflows/metacheck.yml)
[![Test](https://github.com/nowsprinting/UnityAutomatedQAExamples/actions/workflows/test.yml/badge.svg)](https://github.com/nowsprinting/UnityAutomatedQAExamples/actions/workflows/test.yml)

Click [日本語](./README.md) for the Japanese page if you need.



## About this repository

This repository is a sample project for the "Unity Automated QA Guidebook".

Books can be purchased from the following websites, but they are written in Japanese.

#### BOOTH
[Unity Automated QA攻略ガイド - いか小屋 - BOOTH](https://ikagoya.booth.pm/items/3534629)

#### Tech Book Fest Market
[Unity Automated QA攻略ガイド：いか小屋](https://techbookfest.org/product/5755610421264384)



## Structure of sample project

### Scenes

Demo scenes for recording/ playback and testing

```
Assets
└── Scenes
    ├── Keypad.unity                    // Canvas + Button demo scene
    ├── ObjectDemo.unity                // Not Canvas demo scene
    ├── RecordableInputDemo.unity       // (TBD) 2.6 Input System Support demo scene
    └── Title.unity                     // Transition demo scene
```

### Recordings

Chapter 2 Recorded Playback

```
Assets
└── Recordings
    ├── Keypad1997.json
    ├── Keypad20013.json
    ├── Keypad2013.json
    ├── KeypadCrawler.json      // 2.4 Game Crawler (Recording Mode)
    ├── KeypadToTitle.json
    └── TitleToKeypad.json
```

### GeneratedTests

Chapter 3 Test Generation

#### 3.2.1 Full Tests (Use Simplified Driver Code : on)

```
Assets
└── AutomatedQA
    └── GeneratedTests
        ├── FullTestsWithPageObjects_Keypad2013.cs
        └── PageObjects
            └── Scene_Keypad_PageObject.cs
```

#### 3.2.2 Full Tests (Use Simplified Driver Code : off)

```
Assets
└── AutomatedQA
    └── GeneratedTests
        ├── FullTestsWithSteps_Keypad2013.cs
        └── Steps
            └── Steps_Keypad2013.cs
```

#### 3.3 Simple Tests

```
Assets
└── AutomatedQA
    └── GeneratedTests
        └── SimpleTests_Keypad2013.cs
```

### AutomatedRun

Chapter 5 Automators

```
Assets
└── AutomatedRun
    ├── GameCrawlerExample.asset        // 5.3.2 LoadSceneAutomator, 5.3.3 GameCrawlerAutomator
    └── PlaybackScenarioExample.asset   // 5.3.1 RecordedPlaybackAutomator
```

#### 5.2 Run via API

```
Assets
└── Scripts
    └── Editor
        └── CentralAutomationControllerExample.cs
```

#### 5.4 Custom Automator

```
Assets
├── AutomatedRun
│   └── CustomAutomatorExample.asset
└── Scripts
    └── Runtime
        └── CustomAutomator
            ├── CustomAutomator.asmdef
            └── WaitAutomator.cs
```



## License

MIT License
