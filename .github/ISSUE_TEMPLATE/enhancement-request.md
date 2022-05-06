---
name: Enhancement request
about: Enhancement
title: "[enh]"
labels: Enhancement
assignees: ''

---

## 📃 이슈 내용
- 이슈 내용 요약 설명
- 1. Monster Scene에서 플레이어와 몬스터가 소환됐을 때 OnTriggerEnter에서 NullException 오류가 뜸
- -> 몬스터와 플레이어를 처음에 멀리 떨어트려 소환해야 한다.
- 2. 몬스터에 근접했다가 멀어졌을 때(IDLE 상태), 몬스터 혼자 스스로 뱅글뱅글 돈다.
- -> Rigidbody의 Constraints 속성 Freeze Rotation 모든 축(Y축까지)을 선택하고, 코드로만 roatation하게 만들어야 한다.

## 📝 상세 내용
- 이슈 내용 구현 관련 상세 내용 설명

## ✔️ 체크리스트
- [ ] To Do A
- [ ] To Do B
- [ ] To Do C
