# Unity Automated QA Examples

[![Meta file check](https://github.com/nowsprinting/UnityAutomatedQAExamples/actions/workflows/metacheck.yml/badge.svg)](https://github.com/nowsprinting/UnityAutomatedQAExamples/actions/workflows/metacheck.yml)
[![Test](https://github.com/nowsprinting/UnityAutomatedQAExamples/actions/workflows/test.yml/badge.svg)](https://github.com/nowsprinting/UnityAutomatedQAExamples/actions/workflows/test.yml)

Click [English](./README_en.md) for English page if you need.



## このリポジトリについて

このリポジトリは、同人誌『Unity Automated QA攻略ガイド』のサンプルプロジェクトです。

書籍は次のWebサイトから購入できます。

#### BOOTH
[Unity Automated QA攻略ガイド - いか小屋 - BOOTH](https://ikagoya.booth.pm/items/3534629)

#### 技術書典マーケット
[Unity Automated QA攻略ガイド：いか小屋](https://techbookfest.org/product/5755610421264384)



## サンプルプロジェクトの構造

### Scenes

```
Assets
├── Scenes
│   ├── Keypad.unity                    // Canvas + Buttonのデモscene
│   ├── ObjectDemo.unity                // 非Canvasのデモscene
│   ├── RecordableInputDemo.unity
│   └── Title.unity                     // Scene遷移デモ用のタイトル画面
```

### Recordings

第2章 Recorded PlaybackでUI操作を記録したJSONファイル

```
Assets
├── Recordings
│   ├── Keypad1997.json
│   ├── Keypad20013.json
│   ├── Keypad2013.json
│   ├── KeypadCrawler.json      // 2.4 Game Crawler
│   ├── KeypadToTitle.json
│   └── TitleToKeypad.json
```

### GeneratedTests

第3章 Test Generationで`Keypad2013.json`から生成したテストコード

3.2.1 Full Tests + Use Simplified Driver Code : on

```
Assets
├── AutomatedQA
│   ├── GeneratedTests
│   │   ├── FullTestsWithPageObjects_Keypad2013.cs
│   │   ├── PageObjects
│   │   │   └── Scene_Keypad_PageObject.cs
```

3.2.2 Full Tests + Use Simplified Driver Code : off

```
Assets
├── AutomatedQA
│   ├── GeneratedTests
│   │   ├── FullTestsWithSteps_Keypad2013.cs
│   │   ├── Steps
│   │   │   └── Steps_Keypad2013.cs
```

3.3 Simple Tests

```
Assets
├── AutomatedQA
│   ├── GeneratedTests
│   │   ├── SimpleTests_Keypad2013.cs
```

### AutomatedRun

第5章 AutomatorsのAutomated Run設定例

```
Assets
├── AutomatedRun
│   ├── GameCrawlerExample.asset        // 5.3.2 LoadSceneAutomatorも含む
│   └── PlaybackScenarioExample.asset
```

5.2 APIによる実行

```
Assets
├── Scripts
│   ├── Editor
│   │   └── CentralAutomationControllerExample.cs
```

5.4 カスタムAutomator

```
Assets
├── AutomatedRun
│   └── CustomAutomatorExample.asset
├── Scripts
│   ├── Runtime
│   │   ├── CustomAutomator
│   │   │   ├── CustomAutomator.asmdef
│   │   │   └── WaitAutomator.cs
```



## License

MIT License
