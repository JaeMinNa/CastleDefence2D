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
- 버튼 클릭 시, Player의 스킬 사용을 위해

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

### 2. ObjectPool 구현
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/51eaa960-70bc-4614-8236-36bcd36584bd" width="50%"/>

#### 구현 이유
- 미리 생성한 총탄 프리팹을 파괴하지 않고, 재사용해서 최적화를 위해
- 프리팹의 Instantiate, Destroy 함수 사용을 줄이기 위해

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

### 3. GameManger 구현
<p align="center">
  <img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/f89a51ef-2103-45d7-bfb4-977fa98f9f3a" width="49%"/>
  <img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/40fa1552-3838-46b1-9eef-728f75aced92" width="49%"/>
</p>
<br/>

#### 구현 이유
- 각각 Manager들을 통합하여 접근 가능한 Manager가 필요

#### 구현 방법
- 어디서든 쉽게 접근이 가능해야 하므로 싱글톤 사용
- GameManger은 Manager들을 관리하는 하나의 역할만 수행
```C#
public static GameManager I;

[field: SerializeField] public DataManager DataManager { get; private set; }
[field: SerializeField] public SoundManager SoundManager { get; private set; }

private void Awake()
{
    if (I == null) I = this;
    else Destroy(gameObject);
    
    Init();
}

private void Init()
{
    DataManager.Init();
    SoundManager.Init();
}

private void Release()
{
    DataManager.Release();
    SoundManager.Release();
}
```
<br/>

### 4. 로딩 씬 구현
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/dc119f73-2223-4ff2-8744-5346ba599a42" width="50%"/>  

#### 구현 이유
- 씬이 전환 될 때, 다음 씬에서 사용될 리소스들을 읽어와서 게임을 위한 준비 작업 필요
- 로딩 화면이 없다면 가만히 멈춘 화면이나 까만 화면만 보일 수 있음
- 씬이 전환 될 때, 지루한 대기 시간을 이미지나 Tip으로 지루하지 않게 하기 위해

#### 구현 방법
- 씬을 불러오는 도중에 다른 작업이 가능 비동기 방식 씬 전환 구현
```C#
IEnumerator LoadScene()
{
    yield return null;
    AsyncOperation op = SceneManager.LoadSceneAsync(NextScene);
    op.allowSceneActivation = false;
    float timer = 0.0f;
    while (!op.isDone)
    {
        yield return null;
        timer += Time.deltaTime;
        if (op.progress < 0.9f)
        {
            _loadingBar.value = Mathf.Lerp(_loadingBar.value, op.progress, timer);
            if (_loadingBar.value >= op.progress)
            {
                timer = 0f;
            }
        }
        else
        {
            _loadingBar.value = Mathf.Lerp(_loadingBar.value, 1f, timer);
            if (_loadingBar.value == 1.0f)
            {
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
```

- 리소스 로딩이 끝나기 전에 씬 로딩 되는 것을 막기 위해 allowSceneActivation을 false로 설정
- allowSceneActivation을 false로 90% 로드 한 상태로 대기하고, true 변경 시, 남은 부분을 로드하고 씬 이동
<br/>

  
### 5. 3D 사운드 구현

#### 구현 이유
- 유니티에서 제공하는 3D 사운드 기능을 사용하니 거리에 따른 소리 음량 크기 조절이 한번씩 제대로 적용이 안되는 현상 발생
- 사운드가 깨지거나 이상한 소리로 변질되어 재생되는 현상 발생
- 하나의 AudioSource에서 동시에 많은 수의 PlayOneShot 함수가 실행되면 리소스 및 처리 성능에 영향을 줄 수 있음

#### 구현 방법
- 3D 사운드 기능을 사용하지 않고, 거리에 따라 볼륨을 직접 조절하는 방식으로 직접 구현
- 미리 생성한 몇 개의 AudioSource가 순차적으로 재생하도록 구현

```C#
public void StartSFX(string name, Vector3 position)
{
	_index = _index % _etcSFXAudioSource.Length;
	
	float distance = Vector3.Distance(position, GameManager.I.PlayerManager.Player.transform.position);
	float volume = 1f - (distance / _maxDistance);
	if (volume < 0) volume = 0f;
	_etcSFXAudioSource.volume = Mathf.Clamp01(volume);
	_etcSFXAudioSource.PlayOneShot(_sfx[name]);
	
	_index++;
}
```
<br/>

### 6. Player 3단 콤보 공격 구현
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/8ff5ecb8-3c7b-4a1c-8ae3-cf0b76d33911" width="50%"/> 

#### 구현 이유
- Player의 연속 공격 구현

#### 구현 방법
- Sub-State Machine로 각각 애니메이션을 연결
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/f25b1057-7d5d-474d-8013-dd3da44fc75a" width="50%"/>
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/6c4bf20c-2d8e-4a71-a960-65c6864f3149" width="50%"/> 

```C#
public void OnAttackInput(InputAction.CallbackContext context)
{
    if (context.phase == InputActionPhase.Started)
    {
        _animator.SetTrigger("Attack");
    }
}
```
<br/>

### 7. 튜토리얼 시스템 구현
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/c3193176-cf88-4d67-b2c5-60164304ae24" width="50%"/> 

#### 구현 이유
- 유저 피드백에서 게임 진행에 대한 정보가 부족하다는 피드백을 받음

#### 구현 방법
- Tutorial 빈 게임오브젝트에 Canvas 추가, Canvas의 자식으로 튜토리얼 UI 생성
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/64a266c5-3c45-45db-9375-ff17ef0b235d" width="50%"/>

- 튜토리얼을 실행하는 콜라이더 범위 IsTrigger로 설정
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/73f72464-9058-45b6-89d8-ee5d000547eb" width="50%"/>

- 튜토리얼을 관리하는 스크립트와 튜토리얼 범위를 관리하는 스크립트 작성
```C#
public void TutorialActive(int num)
{
	_tutorials[num].SetActive(true);
	Time.timeScale = 0f;
	_isPause = true;
	IsTutorialActive[num] = true;
}
```
<br/>

### 8. 방치형 보상 기능 구현
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/cc22c720-a795-4d9a-811c-e4d0fcb6c781" width="50%"/> 

#### 구현 이유
- 마지막 저장 시간에 따른 보상 지급

#### 구현 방법
- DateTime.Now로 마지막 저장 시간을 구해서 PlayerPrefs로 로컬 저장
- 저장한 시간과 현재 시간을 계산해서 경과 시간을 계산
- 경과 시간에 따라 보상을 지급
```C#
if (PlayerPrefs.HasKey("LastTime"))
{
    _lastTime = DateTime.Parse(PlayerPrefs.GetString("LastTime"));
    _currentTime = DateTime.Now;
    _timeSpan = _currentTime - _lastTime;
    _rewardTime = _timeSpan.TotalSeconds;
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
  
