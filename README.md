# Witchs_House-Unity

마녀의 집을 컨셉으로 제작한 mini project입니다.

### 게임 설명
- W, S, A, D 키를 이용해 이동할 수 있습니다.
- Space 키를 이용해 '공격' / G키를 이용해 '채집'을 할 수 있습니다.
- 공격과 채집을 통해 아이템을 획득, 인벤토리에서 확인할 수 있습니다.
- '제작'에서 획득한 아이템을 이용해 제작을 할 수 있습니다. 제작된 물체는 House 앞에 생성됩니다.
- 도감에서 획득한 아이템을 확인할 수 있습니다.

![witch's house](https://github.com/yerim1004/Witchs_House-Unity/assets/57720521/4a010da4-cd80-4a4a-89ae-715219a45f8e)





### 스크립트 설명

#### GameController
- [게임 시작 전 세팅](https://github.com/yerim1004/Witchs_House-Unity/blob/c3e63068e87b48c2983ed4c46f8ff0807fba8599/Final_project/1971440-KYR/Assets/Script/PlayerScript/GameController.cs)을 위한 코드입니다.

#### 플레이어
- [이동](https://github.com/yerim1004/Witchs_House-Unity/blob/534fe8b923b5effec972befc7a2320a159de9403/Final_project/1971440-KYR/Assets/Script/PlayerMove.cs#L39)
- [채집](https://github.com/yerim1004/Witchs_House-Unity/blob/534fe8b923b5effec972befc7a2320a159de9403/Final_project/1971440-KYR/Assets/Script/PlayerMove.cs#L58)
- [공격 이펙트](https://github.com/yerim1004/Witchs_House-Unity/blob/534fe8b923b5effec972befc7a2320a159de9403/Final_project/1971440-KYR/Assets/Script/Shooting.cs)

#### 동물 AI
- [움직임](https://github.com/yerim1004/Witchs_House-Unity/blob/c3e63068e87b48c2983ed4c46f8ff0807fba8599/Final_project/1971440-KYR/Assets/Script/EnemyScript/EnemyController.cs#L76)
- 동물은 [공격을 받을 경우](https://github.com/yerim1004/Witchs_House-Unity/blob/c3e63068e87b48c2983ed4c46f8ff0807fba8599/Final_project/1971440-KYR/Assets/Script/EnemyScript/EnemyController.cs#L34) HP가 감소되며 3회 이상 공격을 받으면 죽게 됩니다.

#### 채집물
- 플레이어가 채집하고 [5초 후 재생성](https://github.com/yerim1004/Witchs_House-Unity/blob/c3e63068e87b48c2983ed4c46f8ff0807fba8599/Final_project/1971440-KYR/Assets/Script/CollectScript/CollectCreate.cs#L26) 됩니다.
- [재생성 호출](https://github.com/yerim1004/Witchs_House-Unity/blob/0d1436306b0127d2866a06a19167687bd7d12772/Final_project/1971440-KYR/Assets/Script/PlayerScript/PlayerMove.cs#L66)은 채집을 하는 주체에서 합니다

#### 인벤토리
- 아이템을 획득하면 [아이템 저장](https://github.com/yerim1004/Witchs_House-Unity/blob/0d1436306b0127d2866a06a19167687bd7d12772/Final_project/1971440-KYR/Assets/Script/ItemSave.cs#L49) 함수가 호출됩니다.
- 아이템이 0개가 되었을 때 [슬롯을 업데이트](https://github.com/yerim1004/Witchs_House-Unity/blob/0d1436306b0127d2866a06a19167687bd7d12772/Final_project/1971440-KYR/Assets/Script/ItemSave.cs#L77) 합니다.
- 각 [슬롯](https://github.com/yerim1004/Witchs_House-Unity/blob/0d1436306b0127d2866a06a19167687bd7d12772/Final_project/1971440-KYR/Assets/Script/Slot.cs)마다 스크립트로 관리합니다.

#### 도감
- 구글 시트에서 가져온 [데이터를 리스트에 저장](https://github.com/yerim1004/Witchs_House-Unity/blob/0d1436306b0127d2866a06a19167687bd7d12772/Final_project/1971440-KYR/Assets/Script/Dictionary.cs#L70)합니다.
- 저장된 데이터를 바탕으로 [도감 UI를 생성합니다](https://github.com/yerim1004/Witchs_House-Unity/blob/0d1436306b0127d2866a06a19167687bd7d12772/Final_project/1971440-KYR/Assets/Script/DictionaryItem.cs#L28).
- 아이템을 획득하면 UI를 다시 그려줍니다. [[Repaint 스크립트](https://github.com/yerim1004/Witchs_House-Unity/blob/0d1436306b0127d2866a06a19167687bd7d12772/Final_project/1971440-KYR/Assets/Script/DictionaryItem.cs#L109)]

#### 몬스터 제작
- 정해진 제작 순서로 아이템을 삽입하면 제작에 성공합니다. [[제작 스크립트](https://github.com/yerim1004/Witchs_House-Unity/blob/0d1436306b0127d2866a06a19167687bd7d12772/Final_project/1971440-KYR/Assets/Script/MonsterMake.cs#L81)]

