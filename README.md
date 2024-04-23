# 🖥️ 나홀로 성 지키기

+ 제한 시간 동안 몰려오는 적들을 처치하고 성을 지키는 원터치 게임 입니다!
+ 몰려오는 적들을 처치하고 60초 동안 성을 지키세요!
+ 스킬 뽑기를 통해 스킬을 얻으세요!
+ 같은 속성의 스킬을 장착해서 적과 싸우세요!
<br/>

## 📽️ 프로젝트 소개
 - 게임 이름 : 나홀로 성 지키기
 - 플랫폼 : Android / Web
 - 장르 : 2D 하이퍼캐주얼 디펜스 액션 아케이드
 - 개발 기간 : 24.03.18 ~ 24.04.19
 - 프로젝트 유형 : 개인
<br/>

## ⚙️ Environment

- `Unity 2022.3.23f1`
- **IDE** : Visual Studio 2019, MonoDevelop
- **VCS** : Git (GitHub Desktop)
- **Envrionment** : Android / Web
- **Resolution** : 1920 x 1080 `FHD`
<br/>

## ▶️ 게임 스크린샷

<p align="center">
  <img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/3f839016-1a7c-4c2d-ac4e-a139f88fdbee" width="49%"/>
  <img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/2da0e46b-8a1d-449d-badf-f9cfe755f746" width="49%"/>
</p>
<p align="center">
  <img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/512405f5-034a-48bf-b8e9-bcbd533b567f" width="49%"/>
  <img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/6388d81d-0f0e-45b2-9bbd-c1090f8dbf23" width="49%"/>
</p>
<p align="center">
  <img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/43425d4d-311c-4cb3-a646-a9def38e52c1" width="49%"/>
  <img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/1107054e-59a9-4e60-b3d2-9096fe7f9cd5" width="49%"/>
</p>
<br/>

## 🔳 와이어 프레임
![image](https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/e4e6cb5f-0754-496b-8b76-ab81b40c503b)
![image](https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/946f7205-8b3d-4dd3-bf66-185bb4ddce80)


## 🧩 클라이언트 구조

### GameManager
![image](https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/82d943a9-0013-4c2d-a27c-590612208480)

### Enemy
![image](https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/751c8abd-d84f-4597-b9d7-98ec8e6784eb)


## ✏️ 구현 기능

### 1. 상태 패턴 구현
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/76266ccb-1def-47b6-adbb-e9cc4239b6f7" width="50%"/>

#### 구현 이유
- 다양한 상태를 가진 Player와 Enemy 움직임 구현
- 끊임없이 독립적으로 행동해야 함
- 유연한 상태 관리로 필요에 따라 상태를 추가하거나 수정이 가능해야 함

#### 구현 방법
- IState 인터페이스 : 구체적인 상태 클래스로 연결할 수 있도록 설정
```C#
public interface IEnemyState
{
    void Handle(EnemyController controller);
}
``` 
​
- Context 스크립트 : 클라이언트가 객체의 내부 상태를 변경할 수 있도록 요청하는 인터페이스를 정의
```C#
public void Transition()
{
    CurrentState.Handle(_enemyController);
}

public void Transition(IEnemyState state)
{
    CurrentState = state;
    CurrentState.Handle(_enemyController);
}
```
​
- State 스크립트 : 각 State를 정의, State 변경 조건 설정
```C#
// Start문과 동일하게 사용
public void Handle(EnemyController enemyController)
{
    if (!_enemyController)
        _enemyController = enemyController;

    Debug.Log("Idle 상태 시작");   
    _idleTime = 3f;
    _time = 0;

    StartCoroutine(COUpdate());
}

// Update문과 동일하게 사용
IEnumerator COUpdate()
{
  while (true)
  {
  	// 각각의 상태 변환 조건 설정
	if(_enemyController.Ishit)
    	{
		_enemyController.HitStart();
		break;
	}
	if(_enemyController.IsAttack)
	{
		_enemyController.AttackStart();
		break;
	}
    
      yield return null;
  }
}
```
<br/>

### 2. 롱클릭 구현
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/c8ad82a6-fc10-4605-ab7f-51881792969d" width="50%"/>

#### 구현 이유
- 버튼 클릭 시, Player의 방향 전환과 스킬 사용을 위해

#### 구현 방법
- Event Trigger 추가
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/cb78fd39-4f33-44ce-bfe8-83270f0f34e6" width="50%"/>

- Pointer Up, Pointer Down 추가
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/3ea32d49-2703-488b-b375-1f12fed014f2" width="50%"/>

- 스크립트 작성
```C#
public float minClickTime = 1; // 최소 클릭시간
private float _clickTime; // 클릭 중인 시간
private bool _isClick; // 클릭 중인지 판단 

// 버튼 클릭이 시작했을 때
public void ButtonDown()
{
	_isClick = true;
}

// 버튼 클릭이 끝났을 때
public void ButtonUp()
{
	_isClick = false;

	if (_clickTime >= minClickTime)
	{
		Debug.Log("스킬 발동!");
	}
}

private void Update()
{
	if (_isClick)
	{
		_clickTime += Time.deltaTime;
	}
	else
	{
		_clickTime = 0;
	}
}
```

- 버튼 연결
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/e4c67f5b-08eb-4b59-86e6-00d52e57e5c3" width="50%"/>
<br/>
<br/>

### 3. ObjectPool 구현
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/d578aac4-d786-4216-acca-ad1abbc2cfe1" width="50%"/>

#### 구현 이유
- 미리 생성한 프리팹을 파괴하지 않고, 재사용해서 최적화를 위해
- 프리팹의 Instantiate, Destroy 함수 사용을 줄이기 위해
- Enemy, Skill, Item 등 생성과 파괴를 반복하는 프리팹에 적용

#### 구현 방법
- ObjectPoolManager로 ObjectPool들을 관리
- Size만큼 미리 프리팹을 생성하고, 선입선출인 Queue 자료구조로 순차적으로 SetActive(true) 실행
```C#
public GameObject SpawnFromPool(string tag)
{
    if (!PoolDictionary.ContainsKey(tag))
        return null;

    GameObject obj = PoolDictionary[tag].Dequeue();
    PoolDictionary[tag].Enqueue(obj);

    return obj;
}
```
<br/>

### 4. SpawnSystem 구현
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/22d48439-f734-463e-b729-c700c63a21c0" width="50%"/>

#### 구현 이유
- 인스펙터 창에서 Stage 별, 적의 종류와 Spawn 시간을 설정하기 위해
- 각각 Stage 마다, 직접 난이도를 설정하기 위해

#### 구현 방법
- SpawnSystem 생성
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/b14f46a4-933a-4f6d-ac97-cabd09477da4" width="50%"/>

- 인스펙터 창에서 Stage 정보를 입력할 수 있도록, struct를 Serializable로 직렬화
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/f2ff9c46-db91-4570-baa5-34b5674559d2" width="50%"/>

```C#
[System.Serializable]
public struct StageInfo
{
	public int Stage;
	public string[] enemys; // enemy + 생성되는 시간 입력 ex) "Snail 5"
}
public List<StageInfo> Stages;
```
<br/>

- 입력한 Stage 정보를 Split 함수로 문자열을 자르고 적 랜덤 Spawn
```C#
private void Start()
{
	_currentStage = GameManager.I.DataManager.GameData.Stage;
	for (int i = 0; i < Stages[_currentStage - 1].enemys.Length; i++)
	{
		string[] words = Stages[_currentStage - 1].enemys[i].Split(' ');
		_enemy = words[0];
		_spawnTime = int.Parse(words[1]);
		
		StartCoroutine(COSpawnEnemy(_enemy, _spawnTime));
	}
}

IEnumerator COSpawnEnemy(string enemy, int time)
{
	while (true)
	{
		yield return new WaitForSeconds(time);
		
		int random = Random.Range(0, 2);

		if (random == 0) GameManager.I.ObjectPoolManager.ActivePrefab(enemy, _spawnLeft.position);
		else GameManager.I.ObjectPoolManager.ActivePrefab(enemy, _spawnRigth.position);
	}
}
```
<br/>

### 5. Skill 구현
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/f72e2a16-0a57-4236-8bd4-0fbe8b81e356" width="50%"/>

#### 구현 이유
- Melee, Ranged, Area Skill 구현

#### 구현 방법
- Melee Skill 공격 시, AttackCollider를 SetActive(true)해서 적 데미지 적용
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/904a0dae-134a-4fe4-8e45-1f945244f163" width="50%"/>

```C#
private void OnTriggerEnter2D(Collider2D collision)
{
	if(collision.CompareTag("Enemy"))
	{
	    Vector2 _dir = collision.transform.position - _playerController.transform.position;
	    collision.transform.GetComponent<EnemyController>().Ishit = true;
	
	    if(transform.CompareTag("MeleeCollider"))
	    {
		collision.transform.GetComponent<EnemyController>().Hp -= _playerController.Atk;
	    }
	}
}
```
<br/>

- Ranged Skill, Areak Skill 공격 시, Physics2D.OverlapCircleAll로 주위 범위의 콜라이더를 감지해서 적 데미지 적용

```C#
private void Targetting()
{
	int layerMask = (1 << _layerMask);  // Layer 설정
	_targets = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, 2, 0), 2f, layerMask);
	
	for (int i = 0; i < _targets.Length; i++)
	{
	    _targets[i].gameObject.GetComponent<EnemyController>().Hp -= _player.GetComponent<PlayerController>().Atk;
	}
}
```
<br/>

- Areak Skill 공격 시, Interval 초 간격으로 Count 수 만큼 반복

```C#
IEnumerator COShootAreaSkill(SkillData areaSkillData)
{
	int count = 0;
	while (true)
	{
	    count++;
	    GameManager.I.ObjectPoolManager.ActivePrefab(areaSkillData.Tag, transform.position);
	
	    if (count == areaSkillData.Count) break;
	    yield return new WaitForSeconds(areaSkillData.Interval);
	}
}
```
<br/>

- Areak Skill 공격 시, 주위 범위 내, 랜덤으로 생성하고 아래로 이동하도록 구현

```C#
private void Start()
{
	float random = Random.Range(_player.transform.position.x - _areaSkillData.Range, _player.transform.position.x + _areaSkillData.Range);
	_startPos = new Vector3(random, 10f, 0);
	transform.position = _startPos;
}

private void Update()
{
    transform.position += new Vector3(0, -_areaSkillData.Speed, 0) * Time.deltaTime;
}
```
<br/>

### 6. 화살 포물선 운동 구현
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/4d1cd27c-466f-4028-a8d7-31bf6e131532" width="50%"/>

#### 구현 이유
- 성의 좌우 자동 공격을 위해
- 중력의 영향을 받는 자연스러운 화살 구현을 위해

#### 구현 방법
- 화살에 Rigdbody와 Collider 추가
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/7cc79412-ae01-4f19-8bc6-ce8fc8c4fdc8" width="50%"/>

- 스크립트 작성

```C#
public float _power;

private void Start()
{
	_rigidbody.AddForce(transform.right * _power, ForceMode2D.Impulse);
}

private void Update()
{
	transform.right = _rigidbody.velocity;
}
```
<br/>

### 7. 인벤토리 구현
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/003244f9-06c6-4b2b-8425-c0736d6f2e14" width="50%"/>

#### 구현 이유
- 보유한 Skill의 목록을 확인하기 위해

#### 구현 방법
- Scroll View와 Grid Layout Group을 추가
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/cafe7146-32eb-4a68-85b9-7e8350698158" width="50%"/>

- Inventory 스크립트 작성

```C#
private void UpdateMeleeSKillInventory()
{
	// Inventory 초기화
	for (int i = 0; i < _meleeSlotContent.transform.childCount; i++)
	{
		_skillInventorySlot = _meleeSlotContent.transform.GetChild(i).GetComponent<SkillInventorySlot>();
		_skillInventorySlot.SkillEmpty();
	}

	// Inventory Slot
	for (int i = 0; i < _skills.Count; i++)
	{
		_meleeSlotContent.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(_skills[i].IconPath);
		_skillInventorySlot = _meleeSlotContent.transform.GetChild(i).GetComponent<SkillInventorySlot>();
		_skillInventorySlot.SkillText(_skills[i]);
	}
}
```
<br/>

### 8. 뽑기 구현
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/483c5efc-7427-4d66-89db-670cc19b0517" width="50%"/>  

#### 구현 이유
- Skill의 Rank 별, 뽑기 확률 적용을 위해

#### 구현 방법
- Random.Range 함수를 사용해서 1 ~ 100의 자연수 중, 랜덤하게 가지고와서 랭크에 따라 뽑기 확률을 설정
- Random.Range 함수를 사용해서 0 ~ 2 의 자연수 중, 랜덤하게 가지고와서 Melee, Ranged, Area 스킬을 결정
- while문을 사용해서 결정된 Rank가 나올때까지 반복하도록 구현
- S Rank : 10%, A Rank : 25%, B Rank : 65% 적용

```C#
public void SkillIInfoButton()
{
int length = _dataWrapper.SkillData.Length;
int random1 = Random.Range(0, 3); // Skill Type
int random2 = Random.Range(1, 101); // Rank

if(random1 == 0) // Melee
{
    while (true)
    {
	int random3 = Random.Range(0, length);
	if (_dataWrapper.SkillData[random3].Type != SkillData.SkillType.Melee) continue;

	if(_dataWrapper.SkillData[random3].Rank == SkillData.SkillRank.S)
	{
	    if (random2 >= 1 && random2 <= 10)
	    {
		_getSkillData = _dataWrapper.SkillData[random3];
		break;
	    }
	    else continue;
	}
	else if(_dataWrapper.SkillData[random3].Rank == SkillData.SkillRank.A)
	{
	    if (random2 >= 11 && random2 <= 35) // A
	    {
		_getSkillData = _dataWrapper.SkillData[random3];
		break;
	    }
	    else continue;
	}
	else
	{
	    if (random2 >= 36 && random2 <= 100) // B
	    {
		_getSkillData = _dataWrapper.SkillData[random3];
		break;
	    }
	    else continue;
	}
    }
}
```
<br/>

  
### 9. Json 데이터 저장 기능 구현
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/12a93236-5aec-475e-8d3f-9fac1e874fc1" width="50%"/> 

#### 구현 이유
- 게임 데이터를 자동으로 저장하는 기능을 구현하기 위해
- 유니티에서 JSON Utility 클래스를 사용해서 오브젝트 데이터를 쉽게 다룰 수 있기 때문에

#### 구현 방법
- 인스펙터 창에서 데이터를 확인 또는 수정이 가능하도록 데이터 class를 Serializable로 직렬화

```C#
using System.IO;

[System.Serializable]
public class GameData
{
    [Header("GameData")]
    public int Satge = 1;
    public int Coin = 0;
    public int SkillDrawCount = 0;

    [Header("Sound")]
    public float BGMVolume = 0;
    public float SFXVolume = 0;
}
```
<br/>

- 데이터를 저장, 불러오기 하는 함수 작성

```C#
[ContextMenu("To Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
void SaveGameDataToJson()
{
	// Android나 WebGL 플랫폼에서는 persistentDataPath 경로를 사용해야 함
	if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
	{
	    string jsonData = JsonUtility.ToJson(GameData, true);
	    string path = Path.Combine(Application.persistentDataPath, "GameData.json");
	    File.WriteAllText(path, jsonData);
	}
	// 유니티 에디터
	else
	{
	    string jsonData = JsonUtility.ToJson(GameData, true);
	    string path = Path.Combine(Application.dataPath, "GameData.json");
	    File.WriteAllText(path, jsonData);
	}
}

[ContextMenu("From Json Data")]
void LoadGameDataFromJson()
{
	// Android나 WebGL 플랫폼에서는 persistentDataPath 경로를 사용해야 함
	if(Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
	{
	    string path = Path.Combine(Application.persistentDataPath, "GameData.json");
	    string jsonData = File.ReadAllText(path);
	    GameData = JsonUtility.FromJson<GameData>(jsonData);
	}
	// 유니티 에디터
	else
	{
	    string path = Path.Combine(Application.dataPath, "GameData.json");
	    string jsonData = File.ReadAllText(path);
	    GameData = JsonUtility.FromJson<GameData>(jsonData);
	}
}
```
<br/>

- 인스펙터 창에서 수정된 데이터 저장, 불러오기
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/3cb23f65-b22e-428b-af06-df8b4e58907d" width="50%"/> 
<br/>

### 10. Admob 보상형 광고 구현
<img src="https://github.com/JaeMinNa/CastleDefence2D/assets/149379194/05fd840c-4f81-4f16-910b-cda374eb84e0" width="50%"/> 

#### 구현 이유
- 유저가 돈을 지불하지 않아도 광고를 시청하면 Coin을 얻거나, 게임을 더 플레이 할 수 있는 기회를 얻게하기 위해
- 유저들이 광고를 시청함으로써, 게임의 수익화를 실현하기 위해

#### 구현 방법
- Google Admob에서 보상형 광고 생성
- Unity plugin을 설치 후, 프로젝트에 Import
- 테스트 ID와 광고 ID를 적용해서 스크립트 작성

```C#
private void start()
{
	#if UNITY_ANDROID
		if (IsTestMode) _adUnitId = "ca-app-pub-3940256099942544/5224354917"; // 테스트용 ID
		else _adUnitId = ""; // 광고 ID
	#elif UNITY_IPHONE
		_adUnitId = "ca-app-pub-3940256099942544~1458002511"; // 테스트용 ID
	#else
		_adUnitId = "unused";
	#endif

MobileAds.Initialize((InitializationStatus initStatus) => { });

public void LoadRewardedAd()
{
	if (_rewardedAd != null)
	{
	    _rewardedAd.Destroy();
	    _rewardedAd = null;
	}

	var adRequest = new AdRequest();

	RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
	{
		if (error != null || ad == null)
		{
		    Debug.LogError("Rewarded ad failed to load an ad " +
				   "with error : " + error);
		    return;
		}

		Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

		_rewardedAd = ad;
		RegisterEventHandlers(_rewardedAd);
		ShowRewardedAd();
	});
}

public void ShowRewardedAd()
{
	if (_rewardedAd != null && _rewardedAd.CanShowAd())
	{
	    _rewardedAd.Show((Reward reward) =>
	    {
		// 광고 보상 입력
	    });
	}
}

private void RegisterEventHandlers(RewardedAd ad)
{
	ad.OnAdPaid += (AdValue adValue) => { };
	ad.OnAdImpressionRecorded += () => { };
	ad.OnAdClicked += () => { };
	ad.OnAdFullScreenContentOpened += () => { };
	ad.OnAdFullScreenContentClosed += () => { };
	// 광고 불러오기를 실패했을 때
	ad.OnAdFullScreenContentFailed += (AdError error) =>
	{
	    LoadRewardedAd();
	};
}
```
<br/>

## 💥 트러블 슈팅

### 1. Input System을 이용한 Player 이동 개선
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/401b8466-c112-43e6-ab26-1a410670b324" width="50%"/>

#### Input 클래스로 Player 이동 구현
- 간편하고 직관적으로 구현 가능
- Update 문에서 매 프레임 실행하기 때문에 성능에 영향
```C#
private void FixedUpdate()
{
	float moveHorizontal = Input.GetAxis("Horizontal");
	float moveVertical = Input.GetAxis("Vertical");
	
	Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
	_rigidbody.AddForce(movement * speed);
}
```

#### Input System으로 개선
- 입력 이벤트에 대한 바인딩 및 처리를 쉽게 구성
- Update문에서 매 프레임 실행할 필요가 없음
- 다양한 입력 장치를 지원
```C#
public void OnMoveInput(InputAction.CallbackContext context)
{
	if (context.phase == InputActionPhase.Performed)
	{
	    _curMovementInput = context.ReadValue<Vector2>();
	}
	else if (context.phase == InputActionPhase.Canceled)
	{
	    _curMovementInput = Vector2.zero;
	}
}

private void Move()
{
	Vector3 dir = transform.forward * _curMovementInput.y + transform.right * _curMovementInput.x;
	dir *= MoveSpeed;
	dir.y = _rigidbody.velocity.y;
	
	_rigidbody.velocity = dir;
}
```

#### 결과
- 복잡한 입력 시스템이나 다중 입력 조합을 유연하게 처리
<br/>

### 2. Physics.Raycast를 이용한 총기 구현 개선
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/d736e5a7-8aca-4f6b-b4af-56039f537bb6" width="50%"/>

#### 총알 프리팹을 생성해서 총기 구현
- 실제와 같은 총알 속도, 탄도학 등 적용 가능
- 실제와 유사하게 적용하는 것이 어려움
- 적절한 메모리 관리 방법 필요
```C#
private void Fire()
{
	Instantiate(bullet, transform.position, Quaternion.identity);
}
```

#### Physics.Raycast로 개선
- 총알 프리팹을 생성할 필요가 없음
- 즉각적으로 대상의 정보를 읽어 올 수 있음
- 별도의 메모리 관리 방법이 필요 없음
```C#
if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _hitInfo, 50f))
{
	Debug.Log(_hitInfo.transform.name);
}
```

#### 결과
- 초당 프레임 개선 (63 FPS → 73 FPS)
<p align="center">
  <img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/5b4e21fc-eaef-4272-986f-ec634f077708" width="49%"/>
  <img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/dee9851e-ed68-4e00-80ca-6c9db30fc122" width="49%"/>
</p>
<br/>

### 3. ObjectPool을 이용한 총기 탄피 구현 개선

#### 프리팹 생성, 파괴로 총기 탄피 구현
- 간단하고 직관적으로 구현 가능
- 반복적인 프리팹 생성, 삭제로 성능 저하 초래
- 적절한 메모리 관리 방법 필요
```C#
private void Fire()
{
	Instantiate(_bulletEffectObj, transform.position, Quaternion.identity);
}
```

#### ObjectPool로 개선
- 프리팹 생성, 파괴를 하지 않음
- 객체를 미리 생성해서 재사용 → 메모리 최적화 가능

##### ObjectPoolManager
```C#
public void GunEffect(string poolName ,Vector3 startPosition, Quaternion rotation)
{
	_bulletEffectObj = ObjectPool.SpawnFromPool(poolName);
	
	_bulletEffectObj.transform.position = startPosition;
	_bulletEffectObj.transform.rotation = rotation;
	//RangedAttackController attackController = obj.GetComponent<RangedAttackController>();
	//attackController.InitializeAttack(direction, attackData, this);
	
	_bulletEffectObj.SetActive(true);
	StartCoroutine(COGunEffectInactive());
}

IEnumerator COGunEffectInactive()
{
	GameObject obj = _bulletEffectObj;
	
	yield return new WaitForSeconds(0.5f);
	obj.SetActive(false);
}
```

##### ObjectPool
```C#
public GameObject SpawnFromPool(string tag)
{
	if (!PoolDictionary.ContainsKey(tag))
	    return null;
	
	GameObject obj = PoolDictionary[tag].Dequeue();
	PoolDictionary[tag].Enqueue(obj);
	
	return obj;
}
```
![image](https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/558554b0-f1c7-4bd5-b0d0-334c68ce8041)

#### 결과
- 초당 프레임 개선 (50 FPS → 76 FPS)
<p align="center">
  <img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/e02299d0-c341-4ce3-9006-d945f44c5431" width="49%"/>
  <img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/3b3c0f06-0d57-4f9f-ad4a-7f0070b47a9e" width="49%"/>
</p>
<br/>

### 4. 상태 패턴을 이용한 적과 동료 구현
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/86cd872d-3d7e-4dba-94c8-5e29f8b92a86" width="50%"/>

#### 문제 상황
- 적과 동료의 독립적인 움직임을 구현하기 위한 방법이 필요

#### 해결 방안
##### 조건문과 스위치문 사용
- 간단하고 직관적으로 구현 가능
- 행동이 많다면 코드가 복잡해짐
##### 상태 패턴
- 새로운 상태 추가가 쉬움
- 확장성이 용이
  
#### 의견 결정
##### 상태 패턴으로 구현
- 특정 조건에 따라 각각 다른 행동을 할 수 있음
- 특정 행동을 추가해도 유지 관리가 용이
<br/>

### 5. Physics.OverlapSphere를 이용한 Targetting 구현
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/dbfc5b0a-af38-477d-a89c-63363e19549d" width="50%"/>

#### 문제 상황
- 동료와 적의 단체 전투 요소를 위해 Targetting 방법이 필요

#### 해결 방안
##### BoxCollider로 IsTrigger 범위 설정
- 간단하게 구현 가능
##### Physics.OverlapSphere를 사용
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/3187c4ed-2000-44df-b310-79ad0154658f" width="50%"/>

- 특정 범위 내의 적이나 동료 판별 가능
- 코루틴 함수로 일정 시간 반복해서 사용해야 함

```C#
private void Targetting()
{
	int layerMask = (1 << _layerMask);	// Layer 설정
	_targets = Physics.OverlapSphere(transform.position, 50f, layerMask);
}
```
 
#### 의견 결정
##### Physics.OverlapSphere로 구현
- BoxCollider 사용 시, 총기 구현에서 사용한 Physics.Raycast가 BoxCollider를 먼저 인식해서 적을 인식할 수 없음
- 범위 내에서 가장 가까운 적이나 동료를 지정 가능
<br/>

### 6. PlayerPrefs를 이용한 데이터 저장 기능 구현
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/a1088497-fab3-4f63-87fd-7a9184a5a1b2" width="50%"/>

#### 문제 상황
- Player의 정보와 배의 위치를 저장할 수 있는 간단한 저장 방법 필요

#### 해결 방안
##### PlayerPrefs 사용
- 유니티에서 제공하는 기능으로 직관적으로 간단하게 사용 가능

```C#
public void DataSave()
{
	// Player 정보 저장
	PlayerPrefs.SetFloat("SaveHp", _playerConditions.Health.CurValue);
	PlayerPrefs.SetInt("SaveCurrentBullet", _playerController.GunController.CurrentGun.CurrentBulletCount);
	PlayerPrefs.SetInt("SaveCoin", _playerController.CurrentCoin);
}

public void DataLoad()
{
	// Player 정보 불러오기
	_playerConditions.Health.CurValue = PlayerPrefs.GetFloat("SaveHp");
	_playerController.GunController.CurrentGun.CurrentBulletCount = PlayerPrefs.GetInt("SaveCurrentBullet");
	_playerController.CurrentCoin = PlayerPrefs.GetInt("SaveCoin") ;
}
```

##### 직렬화 및 파일 저장 사용
- 안전하고 속도가 매우 빠름
##### 데이터베이스 사용
- 대규모 데이터를 저장하고 관리에 적합

#### 의견 결정
##### PlayerPrefs로 구현
- 간단한 정보만 저장하면 되기 때문에 로컬 저장이 맞다고 판단
- 간단하게 사용할 수 있기 때문에 단순한 게임 진행도는 PlayerPrefs로 충분히 구현 가능
<br/>


## 👩‍👦‍👦 유저 테스트
 - 유저 테스트 기간 : 24.02.21 ~ 24.02.28
<br/>

<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/e23a933a-2f65-4024-881a-20619a0d3ac5" width="50%"/>
<br/>
<br/>



## 📋 프로젝트 회고
### 잘한 점
 - 유저 테스트를 통해, 피드백을 받고 수정 작업 진행
 - ObjectPool을 통해, 최적화 진행
 - 상태 패턴 구현
<br/>

### 한계
- 전체적인 최적화 진행 부족
- 배, 파도 유료 에셋의 사용법을 제대로 숙지 못함
- 방치형 보상 기능의 로컬 시간 저장
- 게임의 목적성, 컨텐츠 부족으로 흥미가 떨어짐
- PlayerPrefs를 이용한 저장의 보완이 필요
<br/>

### 소감
유료 에셋 사용, 최적화 진행, 유저 피드백 경험 등 처음으로 시도한 것들이 많아서 의미가 깊은 프로젝트였습니다. 최적화 진행에서는 완벽하다고 볼 수는 없지만, 다음 프로젝트에서 더욱 잘할 수 있을 거라는 자신감을 가질 수 있었습니다. 다른 조들에 비해서 인원 수가 많이 부족했지만, 그만큼 정말 많은 것들을 배울 수 있었습니다.
  
