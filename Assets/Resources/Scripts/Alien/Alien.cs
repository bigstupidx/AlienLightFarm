using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Alien : MonoBehaviour {

    public enum AlienLiveState
    {
        Born,
        Alive,
        Die
    }

    public enum AlienMovementState
    {
        MoveToPoint,
        MoveToJump,
        Wait,
        Charged,
        Jump,
        Expulsion,
        Free
    }

    public enum JumpDirection
    {
        LeftUp,
        RightUp,
        LeftDown,
        RightDown,
        None
    }

    Clickable ignoreWallClickable;

    Vector3 lastPosition;
    // public const float DeltaHeight = 14.5f;

    float bornTime = 0;


    int currentFloor;

    //  Clickable finalTarget = null;
    //  Clickable lastTarget = null;
    // public Animator animator;

    public GameObject fChild;
    public GameObject childAnim;
    public AlienParts alienParts;

    AlienLiveState currentLiveState = AlienLiveState.Born;
    AlienMovementState currentMovementState = AlienMovementState.Free;
    Color32 startColor;

    float liveTime = GameplayConstants.AlienFullLiveTime;

    Clickable currentClickable;

    Library library;

    Coroutine currentMovableCoroutine;

    Clickable currentFountainTarget;

    Coroutine currentWaitCoroutine;

    const float FullFontainTimer = 10;
    float fontainTimer;

    bool readyToJump;

    float speed = GameplayConstants.AlienNormalSpeed;

    Queue<Clickable> wayQueue = new Queue<Clickable>();

    bool landWasChanged;

    bool pauseInCupol;
    bool pauseInBlackHole;

    int iterator = 0;

    void Awake() {

        library = GameObject.FindObjectOfType<Library>();

        startColor = new Color(0, Random.Range(0.8f, 1f), 1);

    }

    void Update()
    {

        GetPositionInClickable();

        if (!pauseInCupol && !pauseInBlackHole)
        {
            if (liveTime == 0)
            {
                currentLiveState = AlienLiveState.Die;
                return;
            }

            if (landWasChanged && currentMovementState != AlienMovementState.Charged && currentMovementState != AlienMovementState.Expulsion && currentMovementState != AlienMovementState.Jump)
            {
                landWasChanged = false;
                //UpdateIgnoreWall();
                FindFountain();
            }

            if (currentLiveState.Equals(AlienLiveState.Alive))
                liveTime -= Time.deltaTime;
            liveTime = Mathf.Max(liveTime, 0);

            if (currentMovementState == AlienMovementState.MoveToPoint && IsVeryHungry() && currentClickable.IsFountain())
            {
                SetMovementState(AlienMovementState.Charged);
                currentFountainTarget = null;
                StopAllCoroutines();
            }

            /*
            if (AlienInWall() && !currentMovementState.Equals(AlienMovementState.Jump) && !currentLiveState.Equals(AlienLiveState.Born))
            {
                if (currentMovementState.Equals(AlienMovementState.Expulsion))
                {
                    if (currentClickable != null && currentClickable.IsWall() && currentClickableExpulsion != currentClickable)
                    {
                        currentLiveState = AlienLiveState.Die;
                        return;
                    }
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(ExpulsionAlien());
                }
            }*/


            if (currentClickable != null && currentClickable.IsFountain() && currentMovementState.Equals(AlienMovementState.Charged))
            {
                liveTime += Time.deltaTime * GameplayConstants.AlienCoefRateFountain;
                liveTime = Mathf.Min(liveTime, GameplayConstants.AlienFullLiveTime);
                currentClickable.UseFountain();
            }


            if (currentMovementState.Equals(AlienMovementState.Charged) && (liveTime == GameplayConstants.AlienFullLiveTime || !currentClickable.FountainExist()))
            {
                SetMovementState(AlienMovementState.Free);
                currentFountainTarget = null;
            }
        }
    }

    public void UpdateVal()
    {
        iterator = (iterator + 1) % 11;

        UpdateMovementDirection();


        UpdateSpeed();

        SafeCupolInspection();

        if (iterator == 10)
            UpdateColor();

    }

    public void Healing()
    {
        liveTime = GameplayConstants.AlienFullLiveTime;
    }

    void UpdateMovementDirection()
    {
        if (transform.position.x > lastPosition.x)
            alienParts.SetRight();
        else if (transform.position.x < lastPosition.x)
            alienParts.SetLeft();


        lastPosition = transform.position;
    }

    void UpdateSpeed()
    {
        if (IsHungry())
            speed = GameplayConstants.AlienHungrySpeed;
        else
            speed = GameplayConstants.AlienNormalSpeed;

        alienParts.UpdateSpeed(speed);
    }

    void SafeCupolInspection()
    {
        if (AlienInSafeCupol())
        {
            pauseInCupol = true;
            alienParts.PauseOn();
        }
        else
        {
            pauseInCupol = false;
            alienParts.PauseOff();

        }
    }

    void GetPositionInClickable()
    {
        Clickable tempClickable = library.map.GetCurrentClickable(transform.GetComponent<RectTransform>().anchoredPosition);

        if (pauseInBlackHole || pauseInCupol || currentMovementState.Equals(AlienMovementState.Jump))
            return;

        if (tempClickable != null)
        {
            currentClickable = tempClickable;
        }
    }

    void UpdateColor()
    {
        Color32 targetColor;
        Color32 tempStartColor;
        float tempLiveTime = 0;
        if (liveTime > GameplayConstants.AlienFullLiveTime *GameplayConstants.AlienHungryCoef)
        {
            targetColor = new Color(1, 1, 0,1);
            tempLiveTime = liveTime - GameplayConstants.AlienFullLiveTime * GameplayConstants.AlienHungryCoef;
            tempStartColor = startColor;
        }
        else
        {
            tempLiveTime = liveTime;
            targetColor = new Color(1, 0, 0,1);
            tempStartColor = new Color(1, 1, 0,1);
        }


        foreach (Image img in alienParts.images)
        {
            img.color = Color.Lerp(tempStartColor, targetColor, 1 - (tempLiveTime / (GameplayConstants.AlienFullLiveTime * GameplayConstants.AlienHungryCoef))); ;
        }

        //childAnim.GetComponent<Image>().color = Color.Lerp(tempStartColor, targetColor, 1 - (tempLiveTime / (GameplayConstants.AlienFullLiveTime / 2f)));
    }

    public void StartBorn(Clickable clickable)
    {
        currentClickable = clickable;
        currentFloor = clickable.GetFloor();
        StartCoroutine(BornAlienCoroutine(clickable));


        //BornAlienCoroutine(clickable);
      //  fChild.transform.localScale = new Vector3(1, 1, 1);

   //     currentLiveState = AlienLiveState.Alive;

      //  clickable.AlienWasBorning();
    }

    IEnumerator BornAlienCoroutine(Clickable clickable)
    {
        
        while (currentLiveState.Equals(AlienLiveState.Born))
        {
            while (pauseInCupol || pauseInBlackHole)
                yield return null;

            bornTime += Time.deltaTime;

            float currentScale = bornTime / GameplayConstants.AlienBornTime;
            
        fChild.transform.localScale = new Vector3(currentScale, currentScale, currentScale);



        if (bornTime > GameplayConstants.AlienBornTime)
            {
                bornTime = GameplayConstants.AlienBornTime;

                fChild.transform.localScale = new Vector3(1, 1, 1);

                currentLiveState = AlienLiveState.Alive;

                clickable.AlienWasBorning();
                break;
            }

            yield return null;
        }

        //  childAnim.GetComponent<Image>().color = startColor;


        library.aliens.GetComponent<AlienController>().AddAlien(this);

        // SetLastTarget(clickable);
        //  finalTarget = clickable;
    }

    public AlienMovementState GetMovementState()
    {
        return currentMovementState;
    }

    public AlienLiveState GetLiveState()
    {
        return currentLiveState;
    }

    /*
    public void SetTarget(Clickable clickable)
    {
        //  finalTarget = clickable;
        MoveToTarget();

    }*/
    /*
    [System.Obsolete("use SetFountainWayQueue")]
    public void SetFountainTarget(Clickable clickable)
    {
      

        // Debug.Log("SetFountainTarget "+ clickable+" "+ currentFountainTarget);
        if (clickable != currentFountainTarget &&
            (!currentMovementState.Equals(AlienMovementState.Jump) && !currentMovementState.Equals(AlienMovementState.Charged)))
        {
            ///ТУТ НЕ ПРОПУСКАЕТ, КОГДА СТОПОРИТСЯ
            //       Debug.Log("SetFountainTargetInIF");

            //            isFoundFountain = true;


            StopCoroutine(currentMovableCoroutine);

            if (currentWaitCoroutine != null)
                StopCoroutine(currentWaitCoroutine);

            // lastTarget = currentClickable;
            currentFountainTarget = clickable;
            SetTarget(clickable);
        }
    }*/

    public void SetFountainWayQueue(Queue<Clickable> tempQueue)
    {

        Clickable lastElem = tempQueue.ToArray()[tempQueue.Count - 1];

        if (lastElem != currentFountainTarget &&
            (!currentMovementState.Equals(AlienMovementState.Jump) && !currentMovementState.Equals(AlienMovementState.Charged)))
        {

            if(currentMovableCoroutine != null)
            StopCoroutine(currentMovableCoroutine);

            if (currentWaitCoroutine != null)
                StopCoroutine(currentWaitCoroutine);

            // lastTarget = currentClickable;
            currentFountainTarget = lastElem;
            SetWayQueue(tempQueue);
        }
    }

    public void MoveToTarget()
    {
        if (wayQueue.Count == 0)
        {
            if (currentFountainTarget != null)
            {
                
                currentFountainTarget = null;
                SetMovementState(AlienMovementState.Charged);
            }
            else
            {
                SetMovementState(AlienMovementState.Wait);
                currentWaitCoroutine = StartCoroutine(WaitCoroutine());
            }
            return;
        }

        if (currentMovementState.Equals(AlienMovementState.MoveToJump) && currentClickable != null && library.map.IsJumping(currentClickable) && readyToJump)
        {
            StartCoroutine(JumpCoroutine(library.map.GetJumpDirection(currentClickable), library.map.GetClickableAfterJump(currentClickable)));
        }
        else
        {
            if(currentMovableCoroutine != null)
                StopCoroutine(currentMovableCoroutine);

            currentMovableCoroutine = StartCoroutine(MoveToCoroutine(wayQueue.Dequeue()));
        }

        /*
        //Debug.Log("LastTarget "+lastTarget.num +" ")
        if (lastTarget == finalTarget)
        {
        //    Debug.Log(currentFountainTarget);
            if (currentFountainTarget != null)
            {
               // Debug.Log(lastTarget.num);
                currentFountainTarget = null;//isFoundFountain = false;
                currentMovementState = AlienMovementState.Charged;
            }
            else
            {
                currentMovementState = AlienMovementState.Wait; 
                currentWaitCoroutine =StartCoroutine(WaitCoroutine());
            }
            return;
        }



        if (finalTarget.GetFloor().Equals(currentFloor))
        {
            currentMovableCoroutine = StartCoroutine(MoveToCoroutine(finalTarget));
        }
        else
        {
            if(currentMovementState.Equals(AlienMovementState.MoveToJump) && readyToJump && currentClickable != null)
            {
                StartCoroutine(JumpCoroutine(library.map.GetJumpDirection(currentClickable), library.map.GetClickableAfterJump(currentClickable)));
            }
            else
            {
                Clickable ctnf = library.map.GetClickableToNextFloor(currentClickable.num,currentFloor, finalTarget.GetFloor());
                currentMovableCoroutine = StartCoroutine(MoveToCoroutine(ctnf));
            }
        }*/
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(GameplayConstants.AlienMinWaitTime, GameplayConstants.AlienMaxWaitTime));

        while (pauseInCupol || pauseInBlackHole)
            yield return null;

        currentWaitCoroutine = null;
        SetMovementState( AlienMovementState.Free);
    }

    IEnumerator JumpCoroutine(JumpDirection direction, Clickable clickable)
    {
        
        SetMovementState(AlienMovementState.Jump, direction);
        readyToJump = false;

        library.audioController.Jump();

        switch (direction)
        {
            case JumpDirection.RightUp: yield return childAnim.GetComponent<Animator>().PlayAnimation(1); break;
            case JumpDirection.LeftUp: yield return childAnim.GetComponent<Animator>().PlayAnimation(2); break;
            case JumpDirection.RightDown: yield return childAnim.GetComponent<Animator>().PlayAnimation(3); break;
            case JumpDirection.LeftDown: yield return childAnim.GetComponent<Animator>().PlayAnimation(4); break;
        }

        transform.position = childAnim.transform.position;
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x, transform.GetComponent<RectTransform>().anchoredPosition.y - fChild.GetComponent<RectTransform>().anchoredPosition.y);

        //      SetLastTarget(clickable);

        currentFloor = clickable.GetFloor();

        MoveToTarget();
    }

    /*
    IEnumerator ExpulsionAlien()
    {
        SetMovementState(AlienMovementState.Expulsion);
        currentClickableExpulsion = currentClickable;
        RectTransform rt = GetComponent<RectTransform>();
        Vector2 targetPosition = library.map.GetExpulsionTargetPosition(this, currentClickable);
        SetIgnoreWallClickable(currentClickable);

        while (Vector2.Distance(rt.position, targetPosition) > 1f)
        {
            while (pauseInCupol || pauseInBlackHole)
                yield return null;

            rt.position = Vector2.Lerp(rt.position, targetPosition, Time.deltaTime * 5);

            yield return null;
        }
        currentClickableExpulsion = null;
        SetMovementState(AlienMovementState.Free);
    }*/

        /*
    IEnumerator PusherExpulsionAlienCoroutine()
    {
        SetMovementState(AlienMovementState.Expulsion);
        currentClickableExpulsion = currentClickable;
        currentFountainTarget = null;

        RectTransform rt = GetComponent<RectTransform>();
        Vector2 targetPosition = library.map.GetPusherExpulsionTargetPosition(this, currentClickable);

        while (Vector2.Distance(rt.position, targetPosition) > 3f)
        {
            while (pauseInCupol || pauseInBlackHole)
                yield return null;

            rt.position = Vector2.Lerp(rt.position, targetPosition, Time.deltaTime * GameplayConstants.ExpulsionAlienSpeed);

            yield return null;
        }
        currentClickableExpulsion = null;
        SetMovementState(AlienMovementState.Free);
    }*/

        /*
    public void PusherExpulsionAlien()
    {
        if (currentMovableCoroutine != null)
            StopCoroutine(currentMovableCoroutine);

        if (currentWaitCoroutine != null)
            StopCoroutine(currentWaitCoroutine);

        StartCoroutine(PusherExpulsionAlienCoroutine());
    }
    */

    IEnumerator MoveToCoroutine(Clickable clickable)
    {

        RectTransform rt = GetComponent<RectTransform>();
        //  RectTransform targetRt = null; 

        //   targetRt = clickable.GetComponent<RectTransform>();

        bool isFinalTarget = false;
        if (wayQueue.Count == 0)
        {
            isFinalTarget = true;
            SetMovementState(AlienMovementState.MoveToPoint);

        }
        else
        {
            SetMovementState(AlienMovementState.MoveToJump);
        }
        Vector2 targetPosition = clickable.GetLocalPosition(isFinalTarget) /*- library.canvas.scaleFactor * new Vector3(0, DeltaHeight, 0)*/;


        while (Vector2.Distance(rt.anchoredPosition, targetPosition) > 1f)
        {
            while (pauseInCupol || pauseInBlackHole)
                yield return null;

            Vector2 tempPosition = Vector2.MoveTowards(rt.anchoredPosition, targetPosition, Time.deltaTime * speed);
            /*
            if (currentClickable.IsWall() && AlienInWall(tempPosition))
            {

                ////После столкновения со стеной на пару секунд надо забыть о фонтане.
             //   SetLastTarget(currentClickable);
                SetIgnoreFountainClickable(currentFountainTarget);
                currentFountainTarget = null;
               // isFoundFountain = false;
                currentMovementState = AlienMovementState.Free;
                StopAllCoroutines();
            }
            else*/
            rt.anchoredPosition = tempPosition;



            yield return null;
        }
        //  rt.position = targetPosition;

        // SetLastTarget(clickable);

        if (currentMovementState.Equals(AlienMovementState.MoveToJump))
            readyToJump = true;

        MoveToTarget();

    }

    /*
    public Clickable GetLastTarget()
    {
        return lastTarget;
    }*/

    /*
    public Clickable GetFinalTarget()
    {
        return finalTarget;
    }*/

    public bool IsHungry()
    {
        if (liveTime < GameplayConstants.AlienFullLiveTime * GameplayConstants.AlienHungryCoef)
            return true;
        else
            return false;
    }

    public Clickable GetCurrentClickable()
    {
        return currentClickable;
    }

    public int GetCurrentFloor()
    {
        return currentFloor;
    }
    /*
    void SetLastTarget(Clickable clickable)
    {
        lastTarget = clickable;
    }*/

        /*
    public bool AlienInWall()
    {
        return currentClickable != null && currentClickable.IsWall() && AlienIn(library.buildings.wall);
    }*/


    public bool AlienInSafeCupol()
    {
        return currentClickable != null && currentClickable.IsSafeCupol() && !currentMovementState.Equals(AlienMovementState.Jump/* && AlienIn(library.buildings.safeCupol*/);
    }
    /*
    public bool AlienIn(RectTransform rt)
    {
        Vector2 alienPos = GetComponent<RectTransform>().position;
        return ((alienPos.x + childAnim.GetComponent<RectTransform>().rect.width / 2f * library.canvas.scaleFactor > currentClickable.transform.position.x - rt.rect.width / 2f * library.canvas.scaleFactor)
                && (alienPos.x - childAnim.GetComponent<RectTransform>().rect.width / 2f * library.canvas.scaleFactor < currentClickable.transform.position.x + rt.rect.width / 2f * library.canvas.scaleFactor)
                && (alienPos.y - fChild.GetComponent<RectTransform>().anchoredPosition.y * library.canvas.scaleFactor + childAnim.GetComponent<RectTransform>().rect.height / 2f * library.canvas.scaleFactor > currentClickable.transform.position.y - rt.rect.height / 2f * library.canvas.scaleFactor)
                && (alienPos.y - fChild.GetComponent<RectTransform>().anchoredPosition.y * library.canvas.scaleFactor - childAnim.GetComponent<RectTransform>().rect.height / 2f * library.canvas.scaleFactor < currentClickable.transform.position.y + rt.rect.height / 2f * library.canvas.scaleFactor));
    }*/

    public void MoveAlienByWall()
    {/*
        if (AlienInWall(transform.position))
        {
            if (transform.position.x > currentClickable.transform.position.x)
            {
                transform.position = new Vector2(currentClickable.transform.position.x + (library.buildings.wall.rect.width / 2f+ childAnim.GetComponent<RectTransform>().rect.width / 2f) * library.canvas.scaleFactor, transform.position.y); 
            }
            else
            {
                transform.position = new Vector2(currentClickable.transform.position.x -(library.buildings.wall.rect.width / 2f + childAnim.GetComponent<RectTransform>().rect.width / 2f) * library.canvas.scaleFactor, transform.position.y);

            }
        }*/
    }

    public void SetBlackHole()
    {
        pauseInBlackHole = true;
        alienParts.PauseOn();

    }

    public void SetUnBlackHole()
    {
        pauseInBlackHole = false;
        alienParts.PauseOff();
        currentFountainTarget = null;

        SetMovementState(AlienMovementState.Free);
    }

    public bool IsFountainTimeOut()
    {
        if (fontainTimer == 0)
            return false;
        return true;
    }

    public void SetIgnoreWallClickable(Clickable clickable)
    {
        ignoreWallClickable = clickable;
        //fontainTimer = FullFontainTimer;

    }

    public Clickable GetIgnoreWallClickable()
    {
        return ignoreWallClickable;
    }

    public void SetWayQueue(Queue<Clickable> tempQueue)
    {
        wayQueue = tempQueue;
        MoveToTarget();

    }

    public void LandWasChanged()
    {
        landWasChanged = true;
    }

    void FindFountain()
    {
        if (IsHungry())
            library.aliens.GetComponent<AlienController>().AlienFindFountain(this);
    }

    public Clickable GetCurrentFountainTarget()
    {
        return currentFountainTarget;
    }

    void UpdateIgnoreWall()
    {
        if (ignoreWallClickable != null && !ignoreWallClickable.IsWall())
        {
            ignoreWallClickable = null;
        }
    }


    public void SetMovementState(AlienMovementState state)
    {

        if (state == AlienMovementState.MoveToJump || state == AlienMovementState.MoveToPoint)
        {
            alienParts.SetMove();

        }


        if (state == AlienMovementState.Free || state == AlienMovementState.Charged || state == AlienMovementState.Expulsion  || state == AlienMovementState.Wait)
            alienParts.SetStay();

        currentMovementState = state;
    }

    public void SetMovementState(AlienMovementState state, JumpDirection direction)
    {
        if (state == AlienMovementState.Jump)
        {
            switch(direction)
            {
                case JumpDirection.LeftDown: alienParts.SetLeft(); alienParts.SetJumpDown(); break;
                case JumpDirection.RightDown: alienParts.SetRight(); alienParts.SetJumpDown(); break;
                case JumpDirection.LeftUp: alienParts.SetLeft(); alienParts.SetJumpUp(); break;
                case JumpDirection.RightUp: alienParts.SetRight(); alienParts.SetJumpUp(); break;

            }

        }
        SetMovementState(state);
    }

    bool IsVeryHungry()
    {
        if (liveTime < GameplayConstants.AlienFullLiveTime * GameplayConstants.AlienVeryHungryCoef)
            return true;
        else
            return false;
    }
}
