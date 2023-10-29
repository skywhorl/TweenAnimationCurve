# TweenAnimationCurve
>### This is Tween Example Using AnimationCurve Component
<br/>
<br/>

## 소개
본 Ropository는 특정 오브젝트의 시간에 따른 변화라 불리는 Tween을 AnimationCurve 컴포넌트로 표현한 예제 및 스크립트입니다.
해당 예제와 AnimationCurve에 대한 내용은 Notion 페이지 [<kbd> <br>Tween-AnimationCurve <br> </kbd>][_NOTIONLINK]를 참고하시면 되겠습니다.
<!---------------------------------------------------------------------------->
[_NOTIONLINK]: https://protected-lhs-workspace.notion.site/Unity-Animation-Curve-Tween-42fd720d7ab4476c8fdf2e3c3dedc69e?pvs=4

<br/>
<br/>  
<br/>

## 기능
* TweenPosition
  * GameObject를 특정 위치까지 이동시킬 수 있습니다.
* TweenRotation
  * GameObject를 특정 각도까지 회전시킬 수 있습니다.
* TweenScale
  * GameObject를 특정 크기까지 크기를 조절할 수 있습니다.
* TweenRGBA
  * (현재까지 지원하는 Component)Text, Image, Sprite, Material를 가진 GameObject의 색상을 바꿀 수 있습니다.
  <br/>
  <br/>
  <br/>
  
## 사용 방법
* Repository에 있는 Asset 폴더는 GitHub 페이지 내에서의 Package 열람용이니 'TweenAnimationCurve' Package 파일을 다운로드 하시면 됩니다.
* Package 파일을 보시면 TweenAnimationCurve 폴더 내에 ExampleScenes와 Scripts 폴더로 각각 예시로 보여주는 Scene과 사용할 수 있는 Script가 존재합니다.
* Scripts 폴더에서 아래 이미지처럼 TweenPosition, TweenRotation, TweenScale, TweenRGBA 의 Script를 가져다가 사용하실 수 있습니다.

* 해당 Script를 사용하는 방법은 다른 Script에서 변수를 생성 후 AnimationCurve 재생과 정지 함수를 호출하면 됩니다.
다음과 같은 TweenPosition Script의 Component 사용 예시를 참고하시면 되겠습니다.
  ``` c#
  [SerializeField]TweenPosition _tweenPosition;   // 변수 생성
  // '[SerializeField]'는 Tween을 적용할 GameObject를 받아와야 하는 예시 중 하나

  _tweenPosition.Play();          // Position을 움직일 AnimationCurve 재생
  _tweenPosition.Stop();          // 재생 중이던 AnimationCurve 재생 중지
  ```
  * 이후 해당 TweenPosition을 적용한 GameObject에 아래 이미지처럼 from/to 값을 지정하고 그래프의 수치를 수정하시면 됩니다.
    그래프 수치 조절에 대한 다양한 효과 예시로는 '[EasingType](https://easings.net/)'을 참고하시면 되겠습니다.
  <br/>
  <br/>
  <br/>

  ## 예시 영상
  
