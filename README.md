# timestamper
- 打刻(始業、終業)時間をCSVファイルに記録します。

## なぜ作ったか？
- 副業でソフトウェア開発をする際、時間単価での契約をしている。
- ただ、時間報告をするために自身で勤怠アプリを契約するのは、嫌だった。
- 最低限、始業、終業時刻を記録し、最終的に集計できる形式で管理できればよかったので、作成した。

## 使い方
- アプリケーションを起動する。
- ユーザー名を入力する。
  - ここで設定ファイルが作成される。
  - 設定ファイルのデフォルト名は、config.json
- 始業か、終業を入力すれば、それぞれ時間が記録される。
  - 打刻を記録するファイルのデフォルト名は、Dakoku.csv

## 動作環境
- 開発言語：.Net6
- Windowsでは動作確認済み。(Mac, Linuxでは未確認)
  - .Net6はMac, Linuxにも対応しているため、ビルドすれば使えるはず。

## 改善予定
- [ ] 始業、終業時のSlackへの投稿できるようにする。
- [ ] 同じ行へ、始業、終業を記録できるようにする。
- [ ] Windows以外のOSで動作確認する。
