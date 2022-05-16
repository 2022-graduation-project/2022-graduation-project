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
- 3. 스폰포인트에 몬스터 소환할 때 중복되지 않게 수정하기
- -> 해당 스폰포인트에 이미 소환되었는지 확인하는 배열 생성하여, 중복을 확인한다. 이 때 랜덤 숫자를 전역변수로 관리해야 완벽히 중복을 피할 수 있다.

## 📝 상세 내용
- 이슈 내용 구현 관련 상세 내용 설명

## ✔️ 체크리스트
- [ ] To Do A
- [ ] To Do B
- [ ] To Do C
